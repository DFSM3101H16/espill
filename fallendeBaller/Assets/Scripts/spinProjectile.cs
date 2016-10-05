using UnityEngine;
using System.Collections;
using System;

public class SpinProjectile : WindProjectile
{
	private float rx;     //  spin axis vector component
	private float ry;     //  spin axis vector component
	private float rz;     //  spin axis vector component
	private float omega;  //  angular velocity, m/s
	private float radius; //  sphere radius, m

	public SpinProjectile(float x0, float y0, float z0, 
		float vx0, float vy0, float vz0, float time,
		float mass, float area, float density, float Cd,
		float windVx, float windVy, float rx, float ry, 
		float rz, float omega, float radius) :
	base (x0, y0, z0, vx0, vy0, vz0, time, mass, 
		area, density, Cd, windVx, windVy) {

		//  Initialize variables declared in the SpinProjectile class.
		this.rx = rx;
		this.ry = ry;
		this.rz = rz;
		this.omega = omega;
		this.radius = radius;
	}

	//  These properties are used to access the fields declared
	//  in the class.
	public float Rx {
		get {
			return rx;
		}
	}

	public float Ry {
		get {
			return ry;
		}
	}

	public float Rz {
		get {
			return rz;
		}
	}

	public float Omega {
		get {
			return omega;
		}
	}

	public float Radius {
		get {
			return radius;
		}
	}

	//  The GetRightHandSide() method returns the right-hand
	//  sides of the six first-order projectile ODEs
	//  q[0] = vx = dxdt
	//  q[1] = x
	//  q[2] = vy = dydt
	//  q[3] = y
	//  q[4] = vz = dzdt
	//  q[5] = z
	public override float[] GetRightHandSide(float s, float[] q, 
		float[] deltaQ, float ds,
		float qScale) {
		float[] dQ = new float[6];
		float[] newQ = new float[6];

		//  Compute the intermediate values of the 
		//  dependent variables.
		for(int i=0; i<6; ++i) {
			newQ[i] = q[i] + qScale*deltaQ[i];
		}

		//  Declare some convenience variables representing
		//  the intermediate values of velocity.
		float vx = newQ[0];
		float vy = newQ[2];
		float vz = newQ[4];

		//  Compute the apparent velocities by subtracting
		//  the wind velocity components from the projectile
		//  velocity components.
		float vax = vx - this.WindVx;
		float vay = vy - this.WindVy;
		float vaz = vz;

		//  Compute the apparent velocity magnitude. The 1.0e-8 term
		//  ensures there won't be a divide by zero later on
		//  if all of the velocity components are zero.
		float va = Mathf.Sqrt(vax*vax + vay*vay + vaz*vaz) + 1.0e-8f;

		//  Compute the total drag force and the dirctional
		//  drag components.
		float density = this.Density;
		float area = this.Area;
		float cd = this.Cd;
		float mass = this.Mass;
		float Fd = 0.5f*density*area*cd*va*va;
		float Fdx = -Fd*vax/va;
		float Fdy = -Fd*vay/va;
		float Fdz = -Fd*vaz/va;

		//  Compute the velocity magnitude
		float v = Mathf.Sqrt(vx*vx + vy*vy + vz*vz) + 1.0e-8f;

		//  Evaluate the Magnus force terms.
		float Cl = radius*omega/v;
		float Fm = 0.5f*density*area*Cl*v*v;
		float Fmx =  (vy*rz - ry*vz)*Fm/v;
		float Fmy = -(vx*rz - rx*vz)*Fm/v;
		float Fmz =  (vx*ry - rx*vy)*Fm/v;

		//  Compute the right-hand sides of the six ODEs
		dQ[0] = ds*(Fdx + Fmx)/mass;
		dQ[1] = ds*vx;
		dQ[2] = ds*(Fdy + Fmy)/mass;
		dQ[3] = ds*vy;
		dQ[4] = ds*(G + (Fdz + Fmz)/mass);
		dQ[5] = ds*vz;

		return dQ;
	}
}

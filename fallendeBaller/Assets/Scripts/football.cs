using UnityEngine;
using System.Collections;
using System;

public class SoccerBall : SpinProjectile
{
	private float temperature;

	public SoccerBall(float x0, float y0, float z0, 
		float vx0, float vy0, float vz0, float time,
		float mass, float area, float density, float Cd,
		float windVx, float windVy, float rx, float ry, 
		float rz, float omega, float radius, 
		float temperature) :
	base( x0, y0, z0, vx0, vy0, vz0, time, mass, area, 
		density, Cd, windVx, windVy, rx, ry, rz, 
		omega, radius) {

		this.temperature = temperature;
	}

	//  This properties are used to access the temperature field.
	public float Temperature {
		get {
			return temperature;
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

		//  Compute the velocity magnitude
		float v = Mathf.Sqrt(vx*vx + vy*vy + vz*vz) + 1.0e-8f;

		//  Compute the drag coefficient, which depends on
		//  the Reynolds number.
		float radius = this.Radius;
		float density = this.Density;
		float viscosity = 1.458e-6f*Mathf.Pow(temperature,1.5f)/
			(temperature + 110.4f);
		float Re = density*v*2.0f*radius/viscosity;
		float cd;
		if ( Re < 1.0e+5f ) { 
			cd = 0.47f;
		}
		else if ( Re > 1.35e+5f ) {
			cd = 0.22f;
		}
		else {
			cd = 0.47f - 0.25f*(Re - 1.0e+5f)/35000.0f;
		}

		//  Compute the total drag force and the dirctional
		//  drag components.
		float area = this.Area;
		float mass = this.Mass;
		float Fd = 0.5f*density*area*cd*va*va;
		float Fdx = -Fd*vax/va;
		float Fdy = -Fd*vay/va;
		float Fdz = -Fd*vaz/va;

		//  Evaluate the Magnus force terms.
		float omega = this.Omega;
		float rx = this.Rx;
		float ry = this.Ry;
		float rz = this.Rz;
		float rotSpinRatio = Mathf.Abs(radius*omega/v);
		float Cl = 0.385f*Mathf.Pow(rotSpinRatio, 0.25f);

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

using UnityEngine;
using System.Collections;
using System;

public class WindProjectile : DragProjectile 
{
	private float windVx;
	private float windVy;

	public WindProjectile(float x0, float y0, float z0, 
		float vx0, float vy0, float vz0, float time,
		float mass, float area, float density, float Cd,
		float windVx, float windVy) :
	base(x0, y0, z0, vx0, vy0, vz0, time, mass, 
		area, density, Cd) {

		//  Initialize variables declared in the WindProjectile class.
		this.windVx = windVx;
		this.windVy = windVy;
	}

	//  These properties are used to access the fields declared
	//  in the class.
	public float WindVx {
		get {
			return windVx;
		}
	}

	public float WindVy {
		get {
			return windVy;
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
		float vax = vx - windVx;
		float vay = vy - windVy;
		float vaz = vz;

		//  Compute the apparent velocity magnitude. The 1.0e-8 term
		//  ensures there won't be a divide by zero later on
		//  if all of the velocity components are zero.
		float va = Mathf.Sqrt(vax*vax + vay*vay + vaz*vaz) + 1.0e-8f;

		//  Compute the total drag force.
		float density = this.Density;
		float area = this.Area;
		float cd = this.Cd;
		float mass = this.Mass;
		float Fd = 0.5f*density*area*cd*va*va;

		//  Compute the right-hand sides of the six ODEs
		dQ[0] = -ds*Fd*vax/(mass*va);
		dQ[1] = ds*vx;
		dQ[2] = -ds*Fd*vay/(mass*va);
		dQ[3] = ds*vy;
		dQ[4] = ds*(G - Fd*vaz/(mass*va));
		dQ[5] = ds*vz;

		return dQ;
	}
}

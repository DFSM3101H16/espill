using UnityEngine;
using System.Collections;
using System;

public class SimpleProjectile : ODE
{
	//  gravitational acceleration.
	public float G = -9.81f; // In unity, y is the vertical direction

	public SimpleProjectile(float x0, float y0, float z0, 
		float vx0, float vy0, float vz0,
		float time) : base(6) {

		//  Load the initial position, velocity, and time 
		//  values into the s field and q array from the
		//  ODE class.
		this.S = time;
		SetQ(vx0,0);
		SetQ(x0, 1);
		SetQ(vy0,2);
		SetQ(y0, 3);
		SetQ(vz0,4);
		SetQ(z0, 5);
		//SetG (g);
	}

	//  These methods return the location, velocity, 
	//  and time values
	public float GetVx() {
		return GetQ(0);
	}

	public float GetVy() {
		return GetQ(2);
	}

	public float GetVz() {
		return GetQ(4);
	}

	public float GetX() {
		return GetQ(1);
	}

	public float GetY() {
		return GetQ(3);
	}

	public float GetZ() {
		return GetQ(5);
	}

	public float GetTime() {
		return this.S;
	}

	public void SetG(float g){
		G = g;
	}


	//  This method updates the velocity and position
	//  of the projectile according to the gravity-only model.
	public virtual void UpdateLocationAndVelocity(float dt) {
		//  Get current location, velocity, and time values
		//  From the values stored in the ODE class.
		float time = this.S;
		float vx0 = GetQ(0);
		float x0 = GetQ(1);
		float vy0 = GetQ(2);
		float y0 = GetQ(3);
		float vz0 = GetQ(4);
		float z0 = GetQ(5);

		//  Update the xyz locations and the z-component
		//  of velocity. The x- and y-velocities don't change.
		float x = x0 + vx0*dt;
		float z = z0 + vz0*dt;
		float vy = vy0 + G*dt;
		float y = y0 + vy0*dt + 0.5f*G*dt*dt;

		//  Update time;
		time = time + dt;

		//  Load new values into ODE arrays and fields.
		this.S = time;
		SetQ(x, 1);
		SetQ(y, 3);
		SetQ(vy,2);
		SetQ(z, 5);
	}

	//  Because SimpleProjectile extends the ODE class,
	//  it must implement the getRightHandSide method.
	//  In this case, the method returns a dummy array.
	public override float[] GetRightHandSide(float s, float[] q, 
		float[] deltaQ, float ds, float qScale) {
		return new float[1];
	}
}

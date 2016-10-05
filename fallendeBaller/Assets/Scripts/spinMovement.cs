using UnityEngine;
using System.Collections;

public class spinMovement : MonoBehaviour {

	public SpinProjectile ball1;
	public float gravitationalAcceleration;
	public Vector3 deltaMov;

	public float x;
	public float y;
	public float z;
	public float vx;
	public float vy;
	public float vz;
	public float time;
	public float windX;
	public float windZ;
	public float rx, ry, rz;
	public float omega, radius;

	public float mass, area, density, cd;



	// Use this for initialization
	void Start () {
		//gravitationalAcceleration = ball.getGravity();
		gravitationalAcceleration = -0.0981f;

		x = transform.position.x;
		y = transform.position.y;
		z = transform.position.z;
		vx = 0.0f;
		vy = 0.0f;
		vz = 0.0f;

		windX = 0.0f;
		windZ = 0.0f;

		rx = 1f;
		ry = 0f;
		rz = 0f;

		omega = 100000;
		radius = 0.011f;

		mass = 0.43f;
		area = Mathf.PI*radius*radius;
		density = 1.225f;
		cd = 0.25f;

		ball1 = new SpinProjectile (x, z, y, vx, vz, vy, Time.time, mass, area, 
			density, cd, windX, windZ, rx, rz, ry, omega, radius);
		ball1.SetG (gravitationalAcceleration);
		//deltaMov = new Vector3 (0.0f, 0.001f, 0.0f);
	}

	// Update is called once per frame
	void FixedUpdate () {
		//		transform.position -= Vector3.down *0.5f *gravitationalAcceleration * Time.time*Time.time;
		if (transform.position.y > 0.0f) {

			ball1.UpdateLocationAndVelocity (Time.deltaTime);

			x = ball1.GetX ();
			y = ball1.GetZ ();
			z = ball1.GetY ();
			time = Time.time;

			deltaMov = new Vector3 (x, y, z);

			transform.position = new Vector3 (x, y, z);
		}

		if (transform.position.y <= 0.0f) {
			gravitationalAcceleration = 0.0f;
		}	
	}
} // PlayerMovement



using UnityEngine;
using System.Collections;

public class dragMovement : MonoBehaviour {

	public DragProjectile ball1;
	public float gravitationalAcceleration;
	public Vector3 deltaMov;

	public float x;
	public float y;
	public float z;
	public float vx;
	public float vy;
	public float vz;
	public float time;

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

		mass = 0.43f;
		area = Mathf.PI*0.011f*0.011f;
		density = 1.225f;
		cd = 0.25f;

		ball1 = new DragProjectile (x, z, y, vx, vz, vy, Time.time, mass, area, density, cd);
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



using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public SimpleProjectile ball1;
	public float gravitationalAcceleration;
	public Vector3 deltaMov;

	public float x;
	public float y;
	public float z;
	public float vx;
	public float vy;
	public float vz;



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

		ball1 = new SimpleProjectile (x, y, z, vx, vy, vz, Time.time);
		ball1.SetG (gravitationalAcceleration);
		//deltaMov = new Vector3 (0.0f, 0.001f, 0.0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
//		transform.position -= Vector3.down *0.5f *gravitationalAcceleration * Time.time*Time.time;
		if (transform.position.y > 0.0f) {

			ball1.UpdateLocationAndVelocity (Time.deltaTime);

			x = ball1.GetX ();
			y = ball1.GetY ();
			z = ball1.GetZ ();

			deltaMov = new Vector3 (x, y, z);

			transform.position = new Vector3 (x, y, z);
		}

		if (transform.position.y <= 0.0f) {
			gravitationalAcceleration = 0.0f;
		}	
	}
} // PlayerMovement

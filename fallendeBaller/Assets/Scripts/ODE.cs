using UnityEngine;
using System.Collections;
using System;

public abstract class ODE
{
	//  Declare fields used by the class
	private int numEqns;  //  number of equations to solve
	private float[] q;   //  array of dependent variables
	private float s;     //  independent variable

	//  Constructor
	public ODE(int numEqns) {
		this.numEqns = numEqns;
		q = new float[numEqns];
	}

	//  These properties access the value of the numEqns
	//  and s fields.
	public int NumEqns {
		get {
			return numEqns;
		}
	}

	public float S {
		get {
			return s;
		}
		set {
			s = value;
		}
	}

	//  This method returns the entire q[] array.
	public float GetQ(int index) {
		return q[index];
	}

	public void SetQ(float value, int index) {
		q[index] = value;
		return;
	}

	public float[] GetAllQ() {
		return q;
	}

	//  This method returns the right-hand side of the 
	//  ODEs. It is declared abstract to force subclasses
	//  to implement their own version of the method.
	public abstract float[] GetRightHandSide(float s, 
		float[] q,float[] deltaQ, float ds, float qScale);
}

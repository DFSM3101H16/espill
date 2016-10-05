using UnityEngine;
using System.Collections;
using System;

public class ODESolver
{
	//  Fourth-order Runge-Kutta ODE solver.  
	public static void RungeKutta4(ODE ode, float ds) {

		//  Define some convenience variables to make the
		//  code more readable
		int j;
		int numEqns = ode.NumEqns;
		float s;
		float[] q;
		float[] dq1 = new float[numEqns];
		float[] dq2 = new float[numEqns];
		float[] dq3 = new float[numEqns];
		float[] dq4 = new float[numEqns];

		//  Retrieve the current values of the dependent
		//  and independent variables.
		s = ode.S;
		q = ode.GetAllQ();

		// Compute the four Runge-Kutta steps, The return 
		// value of getRightHandSide method is an array of 
		// delta-q values for each of the four steps.
		dq1 = ode.GetRightHandSide(s, q, q, ds, 0.0f);
		dq2 = ode.GetRightHandSide(s+0.5f*ds, q, dq1, ds, 0.5f);
		dq3 = ode.GetRightHandSide(s+0.5f*ds, q, dq2, ds, 0.5f);
		dq4 = ode.GetRightHandSide(s+ds, q, dq3, ds, 1.0f);

		//  Update the dependent and independent variable values
		//  at the new dependent variable location and store the
		//  values in the ODE object arrays.
		ode.S = s + ds;

		for(j=0; j<numEqns; ++j) {
			q[j] = q[j] + (dq1[j] + 2.0f*dq2[j] + 2.0f*dq3[j] + dq4[j])/6.0f;
			ode.SetQ(q[j], j);
		}     

		return;
	}
}

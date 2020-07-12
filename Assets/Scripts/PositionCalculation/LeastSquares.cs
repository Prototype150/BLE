using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Matrix_project;

public class LeastSquares : BasicScript 
{
	protected override void FillArrays()
	{
		Beacons = GameObject.FindGameObjectsWithTag("Beacon");

		x_t = new float[Beacons.Length];
		y_t = new float[Beacons.Length];
		radiuses = new float[Beacons.Length];

		for (int i = 0; i < Beacons.Length; i++)
		{
			radiuses[i] = Beacons[i].GetComponent<BLE_Script>().CountedDistance;
			x_t[i] = Beacons[i].transform.position.x;
			y_t[i] = Beacons[i].transform.position.y;
		}
	}

	public override Vector2 CountPhonePosition()
	{
		FillArrays();
		Matrix A = new Matrix(Beacons.Length - 1, 2);
		Matrix b = new Matrix(Beacons.Length - 1, 1);

		for(int i = 0; i < Beacons.Length-1; i++)
		{
			A[i, 0] = 2 * (x_t[i] - x_t[Beacons.Length - 1]);
			A[i, 1] = 2 * (y_t[i] - y_t[Beacons.Length - 1]);
			b[i, 0] =( x_t[i] * x_t[i] - x_t[Beacons.Length - 1] * x_t[Beacons.Length - 1] + y_t[i] * y_t[i] - y_t[Beacons.Length - 1] * y_t[Beacons.Length - 1] + radiuses[Beacons.Length - 1] * radiuses[Beacons.Length - 1] - radiuses[i] * radiuses[i]);
		}

		Matrix AT = new Matrix();
		Matrix ATO = new Matrix();
		Matrix result = new Matrix();

		A.TransformTransponate();
		AT.Copy(A);
		A.TransformTransponate();
		ATO.Copy(AT * A);
		ATO.TransformOpposite();
		result.Copy(ATO * AT * b);

		return new Vector2((float)result[0, 0], (float)result[1, 0]);
	}
}

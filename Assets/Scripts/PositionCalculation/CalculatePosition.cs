using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatePosition : MonoBehaviour {

	public int MethodNumber;
	public GameObject dot;
	void Start()
	{
		MethodNumber = PlayerPrefs.GetInt("number", 0);
	}

	public void Calculate()
	{
		BasicScript a = null;
		Vector2 b;
		switch (MethodNumber)
		{
			case 1:
				a = GetComponent<WeightenTr>();
				break;
			case 2:
				a = GetComponent<LeastSquares>();
				break;
		}
		if (a != null)
		{
			b = a.CountPhonePosition();
			GameObject t = Instantiate(dot);
			t.transform.position = new Vector2(b.x, b.y);
		}
		else
			Debug.Log("Something went wrong, please, reload the application");
	}
}

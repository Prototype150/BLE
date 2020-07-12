using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Destroy : MonoBehaviour {

	void OnMouseDown()
	{
		if (GameObject.FindGameObjectWithTag("Menu").GetComponent<MainMenu>().sas)
			Destroy(gameObject);
	}
}

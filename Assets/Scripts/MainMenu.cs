using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour 
{
	public GameObject beacon;
	public bool sas;
	int n;
	public void StartTheApp(int a)
	{
		sas = true;
		SceneManager.LoadScene(1);
		n = a;
	}
	public void StopTheApp()
	{
		SceneManager.LoadScene(0);
	}
	void OnDestroy()
	{
		PlayerPrefs.SetInt("number", n);
	}
	public void Quit()
	{
		Application.Quit();
	}
	public void RemovePoint()
	{
		var objects = GameObject.FindGameObjectsWithTag("Point");
		for (int i = 0; i < objects.Length; i++)
			Destroy(objects[i]);




	}

	public void ChangeAllowed()
	{
		sas = !sas;
	}

	public void CreateBeacon()
	{
		GameObject t = Instantiate(beacon);
		t.transform.position = new Vector2(0, 0);
		t.transform.localScale = t.transform.localScale * 3;
	}
}

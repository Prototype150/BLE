using UnityEngine;

public class BLE_Script : MonoBehaviour {

	public GameObject dotPrefap;
	public Transform Phone;


	[SerializeField]
	float RealDistance;
	[SerializeField]
	public float CountedDistance;
	[SerializeField]
	float SignalStrenght;
	public ushort Number;




	void Update() 
	{
		Phone = GameObject.FindGameObjectWithTag("Phone").transform;
		CountRealDistance();
		CountSignalStrenght();
		CountDistance();
	}

	private void CountRealDistance()
	{
		RealDistance = Mathf.Sqrt(Mathf.Pow(Phone.position.x - transform.position.x, 2) + Mathf.Pow(Phone.position.y - transform.position.y, 2));
	}

	private void CountSignalStrenght()
	{
		SignalStrenght = -69 - 20 * Mathf.Log10(RealDistance);
		SignalStrenght += SignalStrenght * Random.Range(-1,1 ) / 100;
	}

	private void CountDistance()
	{
		CountedDistance = Mathf.Pow(10, ((-69 - SignalStrenght) / 20));
	}

}

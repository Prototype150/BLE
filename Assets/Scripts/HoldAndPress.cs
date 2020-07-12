using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class HoldAndPress : MonoBehaviour,IPointerDownHandler,IPointerUpHandler {

	[SerializeField]
	Rigidbody2D player;
	bool pointerDown;

	Vector2 movement;

	[SerializeField]
	int Number;

	[SerializeField]
	int moveSpeed = 5;



	private void Update () 
	{
		if (pointerDown)
		{
			if (Number == 1)
				movement.x = 1;
			if(Number == 2)
				movement.x = -1;
			if (Number == 3)
				movement.y = 1;
			if (Number == 4)
				movement.y = -1;
		}
		player.position += movement * moveSpeed * Time.deltaTime;
		movement = Vector2.zero;
	}

	public void OnPointerUp(PointerEventData a)
	{
		Reset();
	}

	public void OnPointerDown(PointerEventData a)
	{
		pointerDown = true;
	}

	private void Reset()
	{
		pointerDown = false;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draagable : MonoBehaviour {
    private bool selected;
    void Update()
    {
        if(selected == true)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(cursorPos.x, cursorPos.y);
        }
    }
    void OnMouseDown()
    {
        selected = true;
    }

    void OnMouseUp()
    {
        selected = false;
    }
}

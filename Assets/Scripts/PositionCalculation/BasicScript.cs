using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BasicScript : MonoBehaviour {

    protected GameObject[] Beacons;

    protected float[] x_t, y_t, radiuses;

    abstract public Vector2 CountPhonePosition();
    abstract protected void FillArrays();
}

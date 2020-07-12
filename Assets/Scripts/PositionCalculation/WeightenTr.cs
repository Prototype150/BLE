using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeightenTr: BasicScript
{
    protected override void FillArrays()
    {
        radiuses = new float[3];
        x_t = new float[3];
        y_t = new float[3];

        Beacons = GameObject.FindGameObjectsWithTag("Beacon");

        Beacons = Beacons.OrderBy(x => x.GetComponent<BLE_Script>().CountedDistance).Take(3).ToArray();

        for (int i = 0; i < Beacons.Length; i++)
        {
            radiuses[i] = Beacons[i].GetComponent<BLE_Script>().CountedDistance;
            x_t[i] = Beacons[i].transform.position.x;
            y_t[i] = Beacons[i].transform.position.y;
        }
    }
    override public Vector2 CountPhonePosition()
    {
        FillArrays();
        Vector2[] points = FindSuitableArea();

        Vector2 AB = points[1] - points[0];
        Vector2 AC = points[2] - points[0];

        Vector2 result = points[0] + ((AB + AC) / 3);

        float bottom = 0, topX = 0, topY = 0;

        for (int i = 0; i < 2; i++)
            bottom += 1 / (radiuses[i] + radiuses[i + 1]);

        bottom += 1 / (radiuses[2] + radiuses[0]);

        for (int i = 0; i < 2; i++)
        {
            topX += points[i].x / (radiuses[i] + radiuses[i + 1]);
            topY += points[i].y / (radiuses[i] + radiuses[i + 1]);
        }

        topX += points[2].x / (radiuses[2] + radiuses[0]);
        topY += points[2].y / (radiuses[2] + radiuses[0]);

        result.x = topX / bottom;
        result.y = topY / bottom;

        return result;
    }

    private Vector2[] FindSuitableArea()
    {
        Vector2[] intersectionPoints = new Vector2[6];

        UsefullFunctions.FindCircleIntersections(x_t[0], y_t[0], radiuses[0], x_t[1], y_t[1], radiuses[1], out intersectionPoints[0], out intersectionPoints[1]);
        UsefullFunctions.FindCircleIntersections(x_t[1], y_t[1], radiuses[1], x_t[2], y_t[2], radiuses[2], out intersectionPoints[2], out intersectionPoints[3]);
        UsefullFunctions.FindCircleIntersections(x_t[2], y_t[2], radiuses[2], x_t[0], y_t[0], radiuses[0], out intersectionPoints[4], out intersectionPoints[5]);

        for (int i = 0; i < intersectionPoints.Length; i++)
        {
            if (float.IsNaN(intersectionPoints[i].x) || float.IsNaN(intersectionPoints[i].y))
                throw new Exception("Not all intersection points were calculated");
        }

        Vector2[] result = new Vector2[3];

        float minPer;
        minPer = UsefullFunctions.findDistanceBetweenPoints(intersectionPoints[0], intersectionPoints[1]) + UsefullFunctions.findDistanceBetweenPoints(intersectionPoints[1], intersectionPoints[2]) + UsefullFunctions.findDistanceBetweenPoints(intersectionPoints[2], intersectionPoints[0]);
        result = new Vector2[] { intersectionPoints[0], intersectionPoints[1], intersectionPoints[2] };
        for (int i = 0; i < 3; i++)
            for (int j = i + 1; j < 4; j++)
                for (int k = j + 1; k < 5; k++)
                {
                    float z = UsefullFunctions.findDistanceBetweenPoints(intersectionPoints[i], intersectionPoints[j]) + UsefullFunctions.findDistanceBetweenPoints(intersectionPoints[j], intersectionPoints[k]) + UsefullFunctions.findDistanceBetweenPoints(intersectionPoints[k], intersectionPoints[i]);
                    if (z < minPer)
                    {
                        minPer = z;
                        result = new Vector2[] { intersectionPoints[i], intersectionPoints[j], intersectionPoints[k] };
                    }
                }
        return result;
    }
}
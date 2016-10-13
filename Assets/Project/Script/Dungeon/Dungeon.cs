using UnityEngine;
using System.Collections.Generic;
using System;

public class Dungeon : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
	}

    Vector3 getRandomPointInCircle(float radius)
    {
        Vector3 pos = Vector3.zero;

        float t = 2 * (float)Math.PI * UnityEngine.Random.Range(0f, radius);
        float u = UnityEngine.Random.Range(0f, radius) + UnityEngine.Random.Range(0f, radius);
        float r = 0f;

        if (u > 1)
            r = 2 - u;
        else
            r = u;

        pos.x = (int)Mathf.Round(radius * r * (float)Math.Cos(t));
        pos.y = 0;
        pos.z = (int)Mathf.Round(radius * r * (float)Math.Sin(t));

        return pos;
    }
}

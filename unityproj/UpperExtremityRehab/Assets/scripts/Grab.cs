using UnityEngine;
using System.Collections;
using System;

public class Grab : MonoBehaviour
{
	public GameObject _grabPosition;
	
	
	void Start()
	{
		
	}
	
	
	void Update()
	{
		
		
	}
	
	GameObject GetClosestObject ()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("canGrab");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
	
}
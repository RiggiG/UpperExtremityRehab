using UnityEngine;
using System.Collections;
using System;


public class PourBatter : MonoBehaviour
{
	public GameObject _batterSource;

	
	void Start ()
	{
	
	}
	
	void Update ()
	{
		var rot = this.gameObject.transform.eulerAngles.z;
		Debug.Log("Batter cup rotation: " + rot);
		if (rot > 250 && rot < 290) 
		{
			
			if (_batterSource.gameObject.transform.localScale.y <= 0.01)
			{
				Debug.Log("Batter cup empty.");
				return;
			}
			
			_batterSource.gameObject.transform.localScale -= new Vector3(0,0.0005f,0);
			
			if (Vector3.Distance(this.transform.position, GameObject.FindWithTag("pancake").transform.position) < 0.5)
			{
				Debug.Log("Attempting pancake pour");
				FillMug(GameObject.FindWithTag("pancake"));
				
			}
			
			
		}
		
	}
	

	void FillMug(GameObject toFill)
	{
		if (this.gameObject.transform.localScale.x < .48f) 
		{
			toFill.gameObject.transform.localScale += new Vector3(0.001f,0,0.001f);
		}
	}
	
}
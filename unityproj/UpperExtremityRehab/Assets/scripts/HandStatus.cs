using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class HandStatus : MonoBehaviour 
{
	public GameObject _bodySourceManager, handLeft, handRight, grabPosL, grabPosR;
    private BodySourceManager _bodyManager;
	Renderer rendLeft, rendRight;
	// Use this for initialization
	void Start () 
    {
		handLeft = GameObject.Find("HandL");
		rendLeft = handLeft.GetComponent<Renderer>();
		handRight = GameObject.Find("HandR");
		rendRight = handRight.GetComponent<Renderer>();
		Debug.Log("hand state tracker init");
		
		handLeft.SetActive(true);
		handRight.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (_bodySourceManager == null)
        {
            return;
        }

        _bodyManager = _bodySourceManager.GetComponent<BodySourceManager>();
        if (_bodyManager == null)
        {
            return;
        }

        Body[] data = _bodyManager.GetData();
        if (data == null)
        {
            return;
        }
	
		foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
               if (body.HandRightState == HandState.Closed)
			   {
					
					Debug.Log("Right hand closed");
					rendRight.material.SetColor("_Color", Color.green);
					GrabRight(GetClosestObject(grabPosR));
			   }
			   else
			   {
				    Debug.Log("Right hand open");
					rendRight.material.SetColor("_Color", Color.white);
			   }

			   if (body.HandLeftState == HandState.Closed)
			   {
					Debug.Log("Left hand closed");
					GrabLeft(GetClosestObject(grabPosL));
					rendLeft.material.SetColor("_Color", Color.green);
			   }
			   else
			   {
				    Debug.Log("Left hand open");
					rendLeft.material.SetColor("_Color", Color.white);
			   	
			   }
				
            }
        }
		
	
	}
	
	GameObject GetClosestObject(GameObject origin)
	{
		GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("canGrab");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = origin.transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
			if (distance > 0.04)
			{
				closest = null;
			}
        }
		
        return closest;
		
	}
	
	void GrabRight(GameObject toGrab)
	{
		if (toGrab == null)
			return;
		
		toGrab.transform.position = grabPosR.transform.position;
		toGrab.transform.rotation = grabPosR.transform.rotation;

	}
	
	void GrabLeft(GameObject toGrab)
	{
		if (toGrab == null)
			return;
		toGrab.transform.position = grabPosL.transform.position;
		toGrab.transform.rotation = grabPosL.transform.rotation;
	}
	
}
using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class HandStatus : MonoBehaviour 
{
	public float rayDistance;
	public GameObject _bodySourceManager, handLeft, handRight;
    private BodySourceManager _bodyManager;
	Renderer rendLeft, rendRight;
	// Use this for initialization
	void Start () 
    {
		handLeft = GameObject.Find("SphereL");
		rendLeft = handLeft.GetComponent<Renderer>();
		handRight = GameObject.Find("SphereR");
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
					Grab(Windows.Kinect.JointType.HandRight);
			   }
			   else
			   {
				    Debug.Log("Right hand open");
					rendRight.material.SetColor("_Color", Color.white);
			   }

			   if (body.HandLeftState == HandState.Closed)
			   {
					Debug.Log("Left hand closed");
					Grab(Windows.Kinect.JointType.HandLeft);
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
	
	void Grab (Windows.Kinect.JointType joint) {
		
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward))){
			Debug.Log("hit");
			
		}
		return;
	}
	
}
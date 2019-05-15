using UnityEngine;
using System.Collections;
/**using System;
using System.Collections.Generic;



public class Grab : MonoBehaviour
{
	//public GameObject _grabPosition;
	private SteamVR_TrackedObject trackedObj;
	
	private GameObject collidingObject;
	private GameObject objectInHand;
	
	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index);}
	}
	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	void Update()
	{
		if (Controller.GetHairTriggerDown())
		{
			if (collidingObject && collidingObject.tag == "canGrab") {GrabObject();}
		}
		if (Controller.GetHairTriggerUp())
		{
			if (objectInHand) {ReleaseObject();}
		}
		
	}
	private void SetCollidingObject(Collider col)
	{
		if (collidingObject || !col.GetComponent<Rigidbody>())
		{
		return;
		}
		collidingObject = col.gameObject;
	}
	
	public void OnTriggerEnter(Collider other)
	{
		SetCollidingObject(other);
	}
	
	public void OnTriggerStay(Collider other)
	{
		SetCollidingObject(other);
	}
	
	public void OnTriggerExit(Collider other)
	{
		if (!collidingObject)
		{ return; }
		collidingObject = null;
	}
	
	private void GrabObject() 
	{
		objectInHand = collidingObject;
		collidingObject = null;
		var joint = AddFixedJoint();
		joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
	}
	
	private FixedJoint AddFixedJoint()
	{
		FixedJoint fx = gameObject.AddComponent<FixedJoint>();
		fx.breakForce = 20000;
		fx.breakTorque = 20000;
		return fx;
	}
	
	private void ReleaseObject()
	{
		if (GetComponent<FixedJoint>())
		{
			GetComponent<FixedJoint>().connectedBody = null;
			Destroy(GetComponent<FixedJoint>());
			objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
			objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
		}
		objectInHand = null;
	}
/*from Kinect implementation	
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
	
}**/
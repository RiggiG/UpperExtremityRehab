using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;

public class JointRotationRawController : MonoBehaviour 
{
	public JointType jointType;
	public BodySourceManager bodyManager;
	
	void Update () 
	{
		foreach (KeyValuePair<ulong, Body> pairIdBody in bodyManager.GetBodies())
		{
			if (bodyManager.OrderOf(pairIdBody.Key) == 0)
			{
				ApplyJointRotation(pairIdBody.Value);
			}
		}
	}
	
	private void ApplyJointRotation(Body body)
	{
		this.transform.rotation = Utils.GetQuaternion(body.JointOrientations[this.jointType]) * Quaternion.FromToRotation(Vector3.up, Vector3.forward);
	}
}

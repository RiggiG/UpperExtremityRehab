using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;

public class JointRotationSmoothController : MonoBehaviour
{
	public JointType jointType;
	public BodySourceManager bodyManager;
	public int queueSize = 15;	
	private Queue<Quaternion> rotations;
	
	void Start () 
	{
		this.rotations = new Queue<Quaternion>();
		for (int i = 1; i < this.queueSize; i += 1)
		{
			this.rotations.Enqueue(this.transform.rotation);
		}
	}

	void Update ()
	{
		foreach (KeyValuePair<ulong, Body> pairIdBody in this.bodyManager.GetBodies())
		{
			if (this.bodyManager.OrderOf(pairIdBody.Key) == 0)
			{
				ApplyJointRotation(pairIdBody.Value);
			}
		}
	}
	
	private void ApplyJointRotation(Body body)
	{
		Quaternion lastRotation = Utils.GetQuaternion(body.JointOrientations[this.jointType]) * Quaternion.FromToRotation(Vector3.up, Vector3.forward);
		this.rotations.Enqueue(lastRotation);
		this.transform.rotation = SmoothFilter(this.rotations, this.transform.rotation);
		this.rotations.Dequeue();
	}
	
	private Quaternion SmoothFilter(Queue<Quaternion> quaternions, Quaternion lastMedian)
	{
		Quaternion median = new Quaternion(0, 0, 0, 0);
		
		foreach (Quaternion quaternion in quaternions)
		{
			float weight = 1 - (Quaternion.Dot(lastMedian, quaternion) / (Mathf.PI / 2)); // 0 degrees of difference => weight 1. 180 degrees of difference => weight 0.
			Quaternion weightedQuaternion = Quaternion.Lerp(lastMedian, quaternion, weight);

			median.x += weightedQuaternion.x;
			median.y += weightedQuaternion.y;
			median.z += weightedQuaternion.z;
			median.w += weightedQuaternion.w;
		}
		
		median.x /= quaternions.Count;
		median.y /= quaternions.Count;
		median.z /= quaternions.Count;
		median.w /= quaternions.Count;
		
		return NormalizeQuaternion(median);
	}
	
	public Quaternion NormalizeQuaternion(Quaternion quaternion)
	{
		float x = quaternion.x, y = quaternion.y, z = quaternion.z, w = quaternion.w;
		float length = 1.0f / (w * w + x * x + y * y + z * z);
		return new Quaternion(x * length, y * length, z * length, w * length);
	}
}

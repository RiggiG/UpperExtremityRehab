using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class JointPosition : MonoBehaviour 
{
    public Windows.Kinect.JointType _jointType;
    public GameObject _bodySourceManager;
    private BodySourceManager _bodyManager;

	// Use this for initialization
	void Start () 
    {
		
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

        // get the first tracked body...
        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
               //this.gameObject.transform.position = new Vector3
               // this.gameObject.transform.localPosition =  body.Joints[_jointType].Position;
                var pos = body.Joints[_jointType].Position;
				//var rot = body.JointOrientations[_jointType].Orientation;
				
                this.gameObject.transform.position = new Vector3(pos.X, pos.Y + 2, pos.Z * -1);
                //if(_jointType == Windows.Kinect.JointType.HandLeft){
				//Debug.Log("Kinect data: \n" + pos.X + " " + pos.Y + " " + pos.Z);
				//Debug.Log("Kinect data: \n" + rot.X + " " + rot.Y + " " + rot.Z + " " + rot.W + " ");
				//}
				//this.gameObject.transform.rotation = new Quaternion(rot.X, rot.Y, rot.Z, rot.W);
				
				break;
            }
        }
	}
}

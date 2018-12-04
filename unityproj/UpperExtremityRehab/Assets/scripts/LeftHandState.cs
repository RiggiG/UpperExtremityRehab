using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class LeftHandState : MonoBehaviour 
{
	
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
	
		foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
               switch (body.HandLeftState)
			   {
					case HandState.Unknown: 
						break;
					case HandState.NotTracked: 
						break;
					case HandState.Open: 
						Debug.Log("Left hand open");
						this.gameObject.SetActive(false);
						break;
					case HandState.Closed: 
						Debug.Log("Left hand closed");
						this.gameObject.SetActive(true);
						break;
					case HandState.Lasso: 
						break;
					default:
						break;
			   }
				
            }
        }
		
	
	}
	
}
using UnityEngine;
using System.Collections;
using System;


public class Pour : MonoBehaviour
{
	public GameObject _coffeeSource, _pourOrigin;

	
	void Start ()
	{
	
	}
	
	void Update ()
	{
		var rot = this.gameObject.transform.eulerAngles.z;
		//Debug.Log("Carafe rotation: " + rot);
		if (rot > 250 && rot < 290) 
		{
			//Debug.Log("Meets pour angle");
			var closestMug = GetClosestMug();			
			
			if (_coffeeSource.gameObject.transform.localScale.y <= 0.01)
			{
				Debug.Log("Carafe empty.");
				return;
			}
			
			_coffeeSource.gameObject.transform.localScale -= new Vector3(0,0.0005f,0);
			if (Vector3.Distance(_pourOrigin.transform.position, closestMug.transform.position) < 0.3)
			{
				Debug.Log("Attempting mug filling");
				FillMug(closestMug);
				
			}
			
			
		}
		
	}
	
	GameObject GetClosestMug ()
    {
        GameObject[] mugs;
        mugs = GameObject.FindGameObjectsWithTag("mug");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = _pourOrigin.transform.position;
        foreach (GameObject mug in mugs)
        {
            Vector3 diff = mug.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = mug;
                distance = curDistance;
            }
        }
        return closest;
    }
	
	void FillMug(GameObject toFill)
	{
		
		toFill.gameObject.transform.GetChild(1).localScale += new Vector3(0,0.0005f,0);
	}
	
}
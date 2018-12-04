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
		if (rot < -30) 
		{
			var closestMug = GetClosestMug();			
			
			if (_coffeeSource.gameObject.transform.localScale.y <= 0)
			{
				return;
			}
			
			_coffeeSource.gameObject.transform.localScale -= new Vector3(0,0.0001f,0);
			if ((Math.Abs(closestMug.transform.position.z - _pourOrigin.transform.position.z) <= 0.025) && (Math.Abs(closestMug.transform.position.x - _pourOrigin.transform.position.x) <= 0.025))
			{
				
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
        Vector3 position = transform.position;
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
		
		toFill.gameObject.transform.GetChild(1).localScale += new Vector3(0,0.0001f,0);
	}
	
}
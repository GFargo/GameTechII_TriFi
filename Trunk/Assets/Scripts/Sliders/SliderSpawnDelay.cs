using UnityEngine;
using System.Collections;

public class SliderSpawnDelay : MonoBehaviour 
{
	private const float DISTANCE = 13.5f;
	private TempoManager mTempoManager;
	private Vector3 initialPostion;
	
	void Start () 
	{
		mTempoManager = GameObject.Find("Main Camera").GetComponent<TempoManager>();
		initialPostion = transform.position;
	}
	
	void Update () 
	{
		float totalDelay = mTempoManager.GetTotalDelay();
		float currentDelay = mTempoManager.GetCurrentDelay();
		
		float percentage;
		if (Mathf.Abs(totalDelay) > 0)
			percentage = 1 - (currentDelay / totalDelay);
		else 
			percentage = 0;
				
		transform.position = new Vector3(initialPostion.x, initialPostion.y, initialPostion.z + (DISTANCE * percentage));
	}
}

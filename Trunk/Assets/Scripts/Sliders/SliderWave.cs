using UnityEngine;
using System.Collections;

public class SliderWave : MonoBehaviour 
{
	private const float DISTANCE = 13.5f;
	private WaveManager mWaveManager;
	private Vector3 initialPostion;
	
	void Start() 
	{
		mWaveManager = GameObject.Find("Main Camera").GetComponent<WaveManager>();
		initialPostion = transform.position;
	}
	
	void Update() 
	{
		float totalWave = mWaveManager.GetWaveCount();
		float currentWave = mWaveManager.GetCurrentWaveCount();
		
		float percentage = (currentWave - 1) / totalWave;
				
		transform.position = new Vector3(initialPostion.x, initialPostion.y, initialPostion.z + DISTANCE - (DISTANCE * percentage));
	}
}
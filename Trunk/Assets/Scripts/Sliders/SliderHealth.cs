using UnityEngine;
using System.Collections;

public class SliderHealth : MonoBehaviour 
{
	private const float DISTANCE = 13.5f;
	private LevelManager mLevelManager;
	private Vector3 initialPostion;
	
	void Start() 
	{
		mLevelManager = GameObject.Find("Main Camera").GetComponent<LevelManager>();
		initialPostion = transform.position;
	}
	
	void Update() 
	{
		float startHealth = mLevelManager.GetStartHealth();
		float currentHealth = mLevelManager.GetHealth();
		
		float percentage = currentHealth / startHealth;
		
		transform.position = new Vector3(initialPostion.x, initialPostion.y, initialPostion.z + (DISTANCE * percentage));
	}
}

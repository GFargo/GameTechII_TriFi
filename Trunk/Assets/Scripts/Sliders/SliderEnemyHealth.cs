using UnityEngine;
using System.Collections;

public class SliderEnemyHealth : MonoBehaviour 
{
	private const float DISTANCE = 13.5f;
	private EnemyManager mEnemyManager;
	private LevelManager mLevelManager;
	private Vector3 initialPostion;
	
	void Start() 
	{
		mEnemyManager = GameObject.Find("Main Camera").GetComponent<EnemyManager>();
		initialPostion = transform.position;
	}
	
	void Update() 
	{
		GameObject enemy = mEnemyManager.GetSelectedEnemy();
		if (!enemy) enemy = mEnemyManager.GetFarthestEnemy();
		if (enemy)
		{
			Enemy selectedEnemy = enemy.GetComponent<Enemy>();
			
			float startHealth = selectedEnemy.GetStartHealth();
			float currentHealth = selectedEnemy.GetHealth();
			float percentage = currentHealth / startHealth;
			
			if (percentage < 0) percentage = 0;
			
			transform.position = new Vector3(initialPostion.x, initialPostion.y, initialPostion.z + (DISTANCE * percentage));
		}
		else transform.position = initialPostion;
	}
}

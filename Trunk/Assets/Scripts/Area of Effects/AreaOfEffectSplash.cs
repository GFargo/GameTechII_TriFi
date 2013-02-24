using UnityEngine;
using System.Collections;

public class AreaOfEffectSplash : MonoBehaviour 
{
	private const float SMALL = 10.0f;
	private const float MEDIUM = 33.0f;
	private const float LARGE = 56.0f;
	
	private EnemyManager mEnemyManager;
	private float mNumberOfFrames;
	private float mDamage;
	private float mDistancePerFrame;
	private float mDamagePerFrame;
	
	public int range;
	public float time;
	
	void Start ()
	{
		mEnemyManager = GameObject.Find("Main Camera").GetComponent<EnemyManager>();

		if (range != 1 && range != 3 && range != 5) range = 1; // if invalid range
		
		mNumberOfFrames = 0.0f;
	}
	
	void FixedUpdate () 
	{
		if (mNumberOfFrames == 0.0f)
		{
			mNumberOfFrames = time/Time.deltaTime;
			
			if (range == 1)
				mDistancePerFrame = SMALL/mNumberOfFrames;
			else if (range == 3)
				mDistancePerFrame = MEDIUM/mNumberOfFrames;
			else if (range == 5)
				mDistancePerFrame = LARGE/mNumberOfFrames;
			
			mDamagePerFrame = mDamage/mNumberOfFrames;
		}
		
		mEnemyManager.DamageEnemiesInRange(transform.position, range, mDamagePerFrame);//Time.deltaTime * hitPointDamagePerSecond);
		
		gameObject.transform.Translate(0.0f, 0.0f, -mDistancePerFrame);//-speed);
		
		if (range == 1 && gameObject.transform.position.y >= SMALL) Destroy(gameObject);
		else if (range == 3 && gameObject.transform.position.y >= MEDIUM) Destroy(gameObject);
		else if (range == 5 && gameObject.transform.position.y >= LARGE) Destroy(gameObject);
	}
	
	public void SetSplashDamage(float damage) { mDamage = damage; }
}

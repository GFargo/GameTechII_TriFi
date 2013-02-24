using UnityEngine;
using System.Collections;

public enum DIRECTION
{
	NONE,
	POS_X,
	NEG_X,
	POS_Z,
	NEG_Z
}

public class Enemy : MonoBehaviour 
{
	// Private
	
	private WaveManager mWaveManager;
	private EnemyManager mEnemyManager;
	
	private TilePath mPath;
	private int mPathCount;
	private bool mDead;
	private DIRECTION mDirection;
	private DIRECTION mPreviousDirection;
	
	private float hitPoints;
	private float startHitPoints;
	
	public float baseHitPoints;
	public float num;
	public float armor;
	public float worth;
	public float levelDamage;
	
	// Public 
	
	public float speed;
	
	public Material unselected;
	public Material selected;
	
	// Initialization
	
	void Start () 
	{
		mWaveManager = GameObject.Find("Main Camera").GetComponent<WaveManager>();
		mEnemyManager = GameObject.Find("Main Camera").GetComponent<EnemyManager>();
		
		mPathCount = 1;
		mDead = false;
		mDirection = DIRECTION.NONE;
		mPreviousDirection = DIRECTION.NONE;
		
		hitPoints = baseHitPoints * (( mWaveManager.GetCurrentWaveCount()/num) + 1);
		startHitPoints = hitPoints;
	}
	
	// Update
	
	void Update () 
	{	
		if (mPreviousDirection != mDirection)
		{
			if (mDirection == DIRECTION.POS_X)
				transform.Rotate(0.0f, -transform.rotation.eulerAngles.y + 180.0f, 0.0f);
			else if (mDirection == DIRECTION.NEG_X)
				transform.Rotate(0.0f, -transform.rotation.eulerAngles.y, 0.0f);
			else if (mDirection == DIRECTION.POS_Z)
				transform.Rotate(0.0f, -transform.rotation.eulerAngles.y + 90.0f, 0.0f);
			else if (mDirection == DIRECTION.NEG_Z)
				transform.Rotate(0.0f, -transform.rotation.eulerAngles.y - 90.0f, 0.0f);
			
			mPreviousDirection = mDirection;
		}
		
		//print(hitPoints);
		
		if (hitPoints <= 0) mDead = true;
	}
	
	// Accessors
	
	public bool GetDead() { return mDead; }
	public DIRECTION GetDirection() { return mDirection; }
	public float GetLevelDamage() { return levelDamage; }
	public TilePath GetPath() { return mPath; }
	public int GetPathCount() { return mPathCount; }
	public float GetSizeX() { return gameObject.renderer.bounds.size.x; }
	public float GetSizeZ() { return gameObject.renderer.bounds.size.z; }
	public float GetSpeed() { return speed; }
	public int GetTileFromEnd() { return mPath.GetPathCount() - mPathCount;	}
	public float GetWorth() { return worth; }
	public int GetStartHealth() { return (int)startHitPoints; }
	public int GetHealth() { return (int)hitPoints; }
	
	// Mutators
	
	public void IncrementPathCount (int increment) { mPathCount += increment; }
	public void SetDirection(DIRECTION direction) { mDirection = direction; }
	public void SetPath(TilePath path) { mPath = path; }
	public void SetPathCount(int pathCount) { mPathCount = pathCount; }
	public void SetSpeed(int bpm) { speed *= bpm / 100.0f; }
	
	public void ApplyDamage(float hitPointDamage, bool applyArmor)
	{
		float damage;
		
		if (applyArmor) damage = (hitPointDamage - armor);
		else damage = hitPointDamage;
		
		hitPoints -= (damage > 0 ? damage : 0);
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Projectile>())
		{
			Destroy(other.gameObject);
			Projectile projectile = other.GetComponent<Projectile>();
			
			if (projectile)
			{
				float damage = (projectile.GetHitPointDamage() - armor);
				hitPoints -= (damage > 0 ? damage : 0);
			}
		}
//		else if (other.gameObject.GetComponent<AreaOfEffectSplash>() && other != mPreviousCollider)
//		{
//			AreaOfEffectSplash aoe = other.GetComponent<AreaOfEffectSplash>();
//			
//			if (aoe)
//			{
//				print("hello");
//				
//				float damage = (aoe.GetHitPointDamage() - armor);
//				hitPoints -= (damage > 0 ? damage : 0);
//			}
//			
//			mPreviousCollider = other;
//		}
	}
	
	private void OnMouseDown()
	{
		mEnemyManager.SetSelectedEnemy(gameObject);
		gameObject.renderer.material = selected;
	}
	
	public void ResetMaterial()
	{
		gameObject.renderer.material = unselected;
	}
}

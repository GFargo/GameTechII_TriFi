using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour 
{
	protected const int FIRE_COUNT = 4;
	
	protected GameObject tile;
	protected GameObject mTowerUpper;
	
	protected EnemyManager mEnemyManager;
	protected TempoManager mTempoManager;
	protected int mCurrentTile;
	protected bool mTempoOnTile;
	protected int fireRate;
	protected bool[] mFire;
	protected AudioClip mSound;
	protected float mRange;
	//protected int mFireEvery;
	protected bool mHit;
	protected bool mFireEveryToggle;
	
	protected int mNextTileCount;
	protected int mPrevTileCount;
	protected bool mNext;
	
	protected float mTotalCost = 0;
	protected int mCurrentUpgrade = 0;
	
	public string towerName;
	public GameObject towerIcon;
	public float range;
	public float speed;
	public float hitPointDamage;
	public float buy;
	public float upgradeCost;
	public float sellPercent;
	public GameObject projectile;
	//public bool fireOnTempo;
	public int fireEvery;
	public bool fireOne;
	public bool fireTwo;
	public bool fireThree;
	public bool fireFour;
	public float highVolume;
	public float lowVolume;
	
	public GameObject towerUpgrade;
	public AudioClip towerUpgradeSound;
	
	void Start() 
	{
		if (fireEvery > 12) fireEvery = 12;
		
		mEnemyManager = GameObject.Find("Main Camera").GetComponent<EnemyManager>();
		mTempoManager = GameObject.Find("Main Camera").GetComponent<TempoManager>();
		
		mNextTileCount = (int)tile.GetComponent<Tile>().GetTileCoord().x;//0;//mTempoManager.GetCurrentTileCount();
		
		mTowerUpper = transform.FindChild("TowerUpper").gameObject;
		
		gameObject.AddComponent<AudioSource>();
		gameObject.GetComponent<AudioSource>().playOnAwake = false;
		gameObject.transform.Rotate(0.0f, 180.0f, 0.0f);
		
		speed *= mTempoManager.GetBeatsPerMinuteDivided();
		//print(range);
		//print(tile.renderer.bounds.size.x);
		mRange = (range * tile.renderer.bounds.size.x) + (tile.renderer.bounds.size.x / 2.0f);
		
		mCurrentTile = (int)tile.GetComponent<Tile>().GetTileCoord().x;
		
		//towerIcon = Instantiate(towerIcon) as GameObject;
		
		//towerIcon.active = false;
		
		mFire = new bool[FIRE_COUNT];
		
		mFire[0] = fireOne;
		mFire[1] = fireTwo;
		mFire[2] = fireThree;
		mFire[3] = fireFour;
		
		for (int i = 0; i < FIRE_COUNT; i++)
			fireRate += (mFire[i] ? 1 : 0);
		
		if (highVolume == 0)
			highVolume = 1.0f;
		if (lowVolume == 0)
			lowVolume = 0.1f;
		
		mPrevTileCount = 0;
		mHit = false;
	}
	
	protected virtual void Update()//FixedUpdate() 
	{	
		Vector3 look = Vector3.zero;
		GameObject target = null;
		
		mTempoOnTile = false;
		if (mTempoManager.GetCurrentTileCount() == mCurrentTile)
		{
			mTempoOnTile = true;
			mHit = true;
		}
		
		if (/*!mTempoOnTile*/ mPrevTileCount != mTempoManager.GetCurrentTileCount() && mNextTileCount != mTempoManager.GetCurrentTileCount())
			mFireEveryToggle = false;
		
		if (mNextTileCount == mTempoManager.GetCurrentTileCount() && mHit)
		{
			mPrevTileCount = mTempoManager.GetCurrentTileCount();
			mFireEveryToggle = true;

			mNextTileCount += fireEvery;
			if (mNextTileCount >= 12) mNextTileCount -= 12;
		}
		
		if (mEnemyManager.GetFarthestEnemy(gameObject.transform.position, speed, mRange, ref look, ref target))
			//&&(!fireOnTempo || mTempoOnTile))
		{
			look.y = mTowerUpper.transform.position.y;
			mTowerUpper.transform.LookAt(look);
			//transform.FindChild("TowerUpper").transform.LookAt(look); // look at enemies
			//gameObject.transform.LookAt(look); // look at enemies
			
			for (int i = 0; i < FIRE_COUNT; i ++)
			{
				if (mFire[i] && mTempoManager.GetFire(i))
				{	
					if (/*mHit &&*/ mFireEveryToggle)
						Shoot(look, target, mTempoOnTile);
					break;
				}
			}
		}
		else if (mTempoOnTile)
		{
			for (int i = 0; i < FIRE_COUNT; i ++)
			{
				if (mFire[i] && mTempoManager.GetFire(i))
				{
					Sound(1.0f);
					break;
				}
			}
		}
	}
	
	protected virtual void Shoot(Vector3 look, GameObject target, bool volume)
	{
		if (volume) Sound(highVolume);
		else Sound(lowVolume);
		
		GameObject proj = (GameObject)Instantiate(projectile, 
			new Vector3(transform.position.x, transform.localScale.y + 1.0f, transform.position.z), mTowerUpper.transform.rotation);
		proj.GetComponent<Projectile>().SetRange(mRange);
		proj.GetComponent<Projectile>().SetHitPointDamage(hitPointDamage);
		proj.GetComponent<Projectile>().SetTarget(target);
		proj.GetComponent<Projectile>().SetSpeed(speed);
		proj.rigidbody.AddForce(mTowerUpper.transform.forward * speed * mTempoManager.GetBeatsPerMinuteDivided());
	}
	
	protected void Sound(float volume)
	{
		audio.PlayOneShot(mSound, volume);	
	}
	
	// Input Events
	
	protected void OnMouseOver()
	{
		Tile tm = tile.GetComponent<Tile>();
		if (tm) tm.OnMouseOver();
	}
	
	protected void OnMouseDown()
	{
		Tile tm = tile.GetComponent<Tile>();
		if (tm) tm.OnMouseDown();
	}
	
	protected void OnMouseExit()
	{
		Tile tm = tile.GetComponent<Tile>();
		if (tm) tm.OnMouseExit();
	}
	
	public float GetBuy() { return buy; }
	public string GetName() { return towerName; }
	public GameObject GetIcon() { return towerIcon; }
	public int GetFireEvery() { return fireEvery; }
	public int GetRange() { return (int)range; }
	public virtual float GetDamage() { return hitPointDamage; }
	public int GetSell() { return (int)Mathf.Ceil(/*buy*/mTotalCost * (sellPercent/100.0f)); }
	public float GetTotalCost() { return mTotalCost; }
	public int GetUpgrade() { return mCurrentUpgrade; }
	public GameObject GetUpgradeObject() { return towerUpgrade; }
	public AudioClip GetUpgradeSound() { return towerUpgradeSound; }
	public float GetUpgradeCost() { return upgradeCost; }
	
	public void SetSound(AudioClip sound) { this.mSound = sound; }
	public void SetTile(GameObject tile) { this.tile = tile; }
	public void SetTotalCost(float totalCost) { mTotalCost = totalCost; }
	public void SetUpgrade(int upgrade) { mCurrentUpgrade = upgrade; }
	
	public void DecrementTotalCost(float decrement) { mTotalCost -= decrement; }
	public void IncrementTotalCost(float increment) { mTotalCost += increment; }
	
	public void DecrementUpgrade(int decrement) { mCurrentUpgrade -= decrement; }
	public void IncrementUpgrade(int increment) { mCurrentUpgrade += increment; }
}
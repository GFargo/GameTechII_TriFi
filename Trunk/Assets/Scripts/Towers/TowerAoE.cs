using UnityEngine;
using System.Collections;

public class TowerAoE : Tower 
{
	public int numberOfShots;
	
	protected override void Update()
	{
		Vector3 look = Vector3.zero;
		GameObject target = null;
		
		mTempoOnTile = false;
		if (mTempoManager.GetCurrentTileCount() == mCurrentTile)
		{
			mTempoOnTile = true;
			mHit = true;
		}
		
		if (mPrevTileCount != mTempoManager.GetCurrentTileCount() && mNextTileCount != mTempoManager.GetCurrentTileCount())
			mFireEveryToggle = false;
		
		if (mNextTileCount == mTempoManager.GetCurrentTileCount() && mHit)
		{
			mPrevTileCount = mTempoManager.GetCurrentTileCount();
			mFireEveryToggle = true;

			mNextTileCount += fireEvery;
			if (mNextTileCount >= 12) mNextTileCount -= 12;
		}
		
		if (mEnemyManager.GetFarthestEnemy(gameObject.transform.position, speed, mRange, ref look, ref target))
			//&& (!fireOnTempo || mTempoOnTile))
		{
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
	
	protected override void Shoot(Vector3 look, GameObject target, bool volume)
	{
		if (volume) Sound(1.0f);
		else Sound(0.1f);
		
		float degreesPerShot = 360.0f / numberOfShots;
		GameObject proj;
		
		for (int i = 0; i < numberOfShots; i++)
		{
			proj = (GameObject)Instantiate(projectile,//proj, 
				new Vector3(transform.position.x, transform.localScale.y + 1.0f, transform.position.z), transform.rotation);
			proj.GetComponent<Projectile>().SetRange(mRange);
			proj.GetComponent<Projectile>().SetHitPointDamage(hitPointDamage);
			proj.transform.Rotate(0.0f, degreesPerShot * i, 0.0f);
			proj.transform.Translate(-transform.forward * (numberOfShots/5.0f));
			proj.rigidbody.AddForce(proj.transform.forward * speed);
		}
	}
}
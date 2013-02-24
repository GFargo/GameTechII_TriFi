using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
	private bool mLoadSuccess;
	
	// Managers
	
	private TileManager mTileManager;
	private WaveManager mWaveManager;
	private LevelManager mLevelManager;
	private TempoManager mTempoManager;
	
	private TilePath[] mTilePaths;
	
	private GameObject mSelectedEnemy;
	
	// Reset
	
	private List<GameObject> mEnemyList;
	private float mTimer;
		
	void Start() 
	{
		mTileManager = gameObject.GetComponent<TileManager>();
		mWaveManager = gameObject.GetComponent<WaveManager>();
		mTempoManager = gameObject.GetComponent<TempoManager>();
		mLevelManager = gameObject.GetComponent<LevelManager>();
		
		mTilePaths = new TilePath[2];
		
		if (mTileManager)
		{
			mLoadSuccess = true;
			mEnemyList = new List<GameObject>();
			
			mTilePaths[0] = mTileManager.GetTilePathOne();
			mTilePaths[1] = mTileManager.GetTilePathTwo();
			
			mTimer = 0.0f;
		}
	}
	
	public void ReStart()
	{
		mTimer = 0.0f;
		
		for (int i = mEnemyList.Count - 1; i >= 0; i--)
		{
			Destroy(mEnemyList[i]);
			mEnemyList.RemoveAt(i);
		}
	}
	
	public void Pause()
	{
		
	}
	
	void FixedUpdate ()
	{
		if (mLoadSuccess)
			MoveEnemies();
		
		Spawn();
		
		if (mEnemyList.Count > 0)
		{
			for (int i = mEnemyList.Count - 1; i >= 0; i--)
			{	
				if (mEnemyList[i].GetComponent<Enemy>().GetDead())
				{
					mLevelManager.IncrementBank(mEnemyList[i].GetComponent<Enemy>().GetWorth());
					Destroy(mEnemyList[i]);
					mEnemyList.RemoveAt(i);
				}
			}
		}
	}
	
	private void MoveEnemies()
	{
		for (int i = 0; i < mEnemyList.Count; i++)
		{
			Enemy enemyScript = mEnemyList[i].GetComponent<Enemy>();
			int count = enemyScript.GetPathCount();
			Vector3 enemyPosition = mEnemyList[i].transform.position;
			TilePath path = enemyScript.GetPath();
			float speed = enemyScript.GetSpeed();
			DIRECTION direction = DIRECTION.NONE;
			
			if (count < path.GetPathCount())
			{	
				GameObject tile = mTileManager.GetTileMap().GetTile((int)path.GetPath(count).x, (int)path.GetPath(count).y);
				Vector3 tilePosition = tile.transform.position;
				
				if (enemyPosition.x < tilePosition.x)
				{
					enemyPosition.x += speed;
					if (enemyPosition.x > tilePosition.x)
						enemyPosition.x = tilePosition.x;
					
					direction = DIRECTION.POS_X;
				}
				else if (enemyPosition.x > tilePosition.x)
				{
					enemyPosition.x -= speed;
					if (enemyPosition.x < tilePosition.x)
						enemyPosition.x = tilePosition.x;
					
					direction = DIRECTION.NEG_X;
				}
				else if (enemyPosition.z < tilePosition.z)
				{
					enemyPosition.z += speed;
					if (enemyPosition.z > tilePosition.z)
						enemyPosition.z = tilePosition.z;
					
					direction = DIRECTION.POS_Z;
				}
				else if (enemyPosition.z > tilePosition.z)
				{
					enemyPosition.z -= speed;
					if (enemyPosition.z < tilePosition.z)
						enemyPosition.z = tilePosition.z;
					
					direction = DIRECTION.NEG_Z;
				}
				if (enemyPosition.x == tilePosition.x &&
					enemyPosition.z == tilePosition.z)
				{
					mEnemyList[i].GetComponent<Enemy>().IncrementPathCount(1);
					if (mEnemyList[i].GetComponent<Enemy>().GetPathCount() == path.GetPathCount())
					{
						mLevelManager.DecrementLevelHealth(mEnemyList[i].GetComponent<Enemy>().GetLevelDamage());
						
						Destroy(mEnemyList[i]);
						mEnemyList.RemoveAt(i);
						break;
					}
				}
			}
			mEnemyList[i].transform.position = enemyPosition;
			mEnemyList[i].GetComponent<Enemy>().SetDirection(direction);
		}
	}
	
	private void Spawn()
	{
		if (mEnemyList.Count == 0 && 
			mTempoManager.GetCurrentTileCount() == 0)
		{
			mWaveManager.IterateWave();
			if (mWaveManager.GetCurrentWave() != null)
				mTileManager.ActivatePath(mWaveManager.GetCurrentWave().GetPathNumber());
			mTimer = 0.0f;
		}
		
		if (mWaveManager.GetCurrentWave() != null && mTempoManager.GetCurrentTime() >= 0)
		{
			mTimer += Time.deltaTime;
			
			int path = mWaveManager.GetCurrentWave().GetPathNumber() - 1;
			
			if (path > 1 || path < 0) path = 0;
			
			if (mWaveManager.GetCurrentWave().GetEnemyCount() > 0 && 
			   (mTimer >= (mWaveManager.GetCurrentWave().GetEnemyTime() / (mTempoManager.GetBeatsPerMinute() / 100.0f)) || mEnemyList.Count == 0))
			{
				Vector3 pos = mTileManager.GetTileMap().GetTile(
					(int)mTilePaths[path].GetPath(0).x,
					(int)mTilePaths[path].GetPath(0).y).transform.position;
				
				//pos.y = 0.0f;
				
				mEnemyList.Add(Instantiate(mWaveManager.GetCurrentWave().GetEnemyObject(),
					pos, 				
					new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as GameObject);
				
				mEnemyList[mEnemyList.Count - 1].transform.Translate(0.0f, 
					mTileManager.GetTileHeight() /*+ 
					(mEnemyList[mEnemyList.Count - 1].renderer.bounds.size.y / 2.0f)*/, 0.0f);
				mEnemyList[mEnemyList.Count - 1].GetComponent<Enemy>().SetPath(mTilePaths[path]);
				mEnemyList[mEnemyList.Count - 1].GetComponent<Enemy>().SetSpeed(mTempoManager.GetBeatsPerMinute());
				//mEnemyList[mEnemyList.Count - 1].renderer.enabled = false;
				mWaveManager.GetCurrentWave().DecrementEnemyCount(1);
				
				mTimer = 0.0f;
			}
		}
	}
	
	public void DamageEnemiesInRange(Vector3 position, float range, float damage)
	{
		if (mLoadSuccess)
		{
			for (int i = 0; i < mEnemyList.Count; i++)
			{
				Enemy enemy = mEnemyList[i].GetComponent<Enemy>();
				
				Vector3 enemyPosition = new Vector3(mEnemyList[i].transform.position.x, position.y, mEnemyList[i].transform.position.z);
				Vector3 differance = position - enemyPosition;
				
				if (Mathf.Pow(differance.x, 2) + Mathf.Pow(differance.z, 2) <= Mathf.Pow(range * mTileManager.GetTileMap().GetTileSize(), 2))
					enemy.ApplyDamage(damage, false);
			}
		}
	}
	
	// Accessors
	
	public GameObject GetFarthestEnemy()
	{
		if (mEnemyList.Count > 0)
			return mEnemyList[0];
		return null;
	}
	
	public bool GetFarthestEnemy(Vector3 position, float speed, float range, ref Vector3 look, ref GameObject target)
	{
		bool found = false;
		int tilesFromEnd = 0;
		
		if (mLoadSuccess)
		{
			for (int i = 0; i < mEnemyList.Count; i++)
			{
				Enemy enemy = mEnemyList[i].GetComponent<Enemy>();
				
				Vector3 enemyPosition = new Vector3(mEnemyList[i].transform.position.x, position.y, mEnemyList[i].transform.position.z);
				Vector3 differance = position - enemyPosition;
				
				if (Mathf.Pow(differance.x, 2) + Mathf.Pow(differance.z, 2) < Mathf.Pow(range /*mTileManager.GetTileMap().GetTileSize()*/, 2))
				{
					if (!found || enemy.GetTileFromEnd() < tilesFromEnd)
					{
						found = true; 
						tilesFromEnd = enemy.GetTileFromEnd();
						
						float dist =  mTileManager.GetTileWidth() * 1.0f;
						
						if (differance.x > dist || differance.z > dist)
						{
//							if (enemy.GetDirection() == DIRECTION.POS_X)
//								enemyPosition.x += (enemy.GetSizeX() * (maxSpeed - speed)) * 
//									mTempoManager.GetBeatsPerMinuteDivided();
//							else if (enemy.GetDirection() == DIRECTION.NEG_X)
//								enemyPosition.x -= (enemy.GetSizeX() * (maxSpeed - speed)) * 
//									mTempoManager.GetBeatsPerMinuteDivided();
//							else if (enemy.GetDirection() == DIRECTION.POS_Z)
//								enemyPosition.z += (enemy.GetSizeZ() * (maxSpeed - speed)) * 
//									mTempoManager.GetBeatsPerMinuteDivided();
//							else if (enemy.GetDirection() == DIRECTION.NEG_Z)
//								enemyPosition.z -= (enemy.GetSizeZ() * (maxSpeed - speed)) * 
//									mTempoManager.GetBeatsPerMinuteDivided();
						}
						
						target = mEnemyList[i];
						look = enemyPosition;
					}
				}
			}
		}
		return found;
	}
	
	public GameObject GetSelectedEnemy() { return mSelectedEnemy; }
	
	public void SetSelectedEnemy(GameObject selectedEnemy) 
	{ 
		if (mSelectedEnemy)
			mSelectedEnemy.GetComponent<Enemy>().ResetMaterial();
		mSelectedEnemy = selectedEnemy; 
	}
}
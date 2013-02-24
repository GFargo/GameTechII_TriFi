using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public enum TOWER
{
	BASE,
	AOE,
	SPLASH,
	NONE
};

public class TowerManager : MonoBehaviour
{ 
	private const int TOWER_COUNT = 3;
	
	private int mCurrentTower;
	private GameObject mTowerBaseIcon;
	private GameObject mTowerAreaOfEffectIcon;
	private GameObject mTowerSplashIcon;
	private GameObject mTower;
	private AudioClip mSound;
	
	public GameObject towerBase;
	public AudioClip towerBaseSound;
	public GameObject towerAreaOfEffect;
	public AudioClip towerAreaOfEffectSound;
	public GameObject towerSplash;
	public AudioClip towerSplashSound;
	
	
	
	private List<Tile> mTowerList;
	
	public GUIText text;
	
	void Awake() 
	{
		mTowerList = new List<Tile>();
		mCurrentTower = 0;
	}
	
	void Update () 
	{
		if (Input.GetKeyUp(KeyCode.S))
		{	
			mCurrentTower++;
			if (mCurrentTower >= TOWER_COUNT)
				mCurrentTower = 0;
		}
	}
	
	public GameObject GetCurrentTower() 
	{ 
		switch(mCurrentTower)
		{
			case (int)TOWER.BASE:
				return towerBase;
			case (int)TOWER.AOE:
				return towerAreaOfEffect;
			case (int)TOWER.SPLASH:
				return towerSplash;
			default:
				return null;
		}
	}
	
	public AudioClip GetCurrentSound()
	{
		switch(mCurrentTower)
		{
			case (int)TOWER.BASE:
				return towerBaseSound;
			case (int)TOWER.AOE:
				return towerAreaOfEffectSound;
			case (int)TOWER.SPLASH:
				return towerSplashSound;
			default:
				return null;
		}
	}
	
	public void AddTile(Tile tile) { mTowerList.Add(tile); }
	public void SetCurrentTower(TOWER type) { mCurrentTower = (int)type; }
}
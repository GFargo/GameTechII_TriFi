using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour 
{
	private TowerManager mTowerManager;
	private LevelManager mLevelManager;
	private GUIManager mGUIManager;
	private TileManager mTileManager;
	
	private GameObject tower;
	private GameObject towerLower;
	private GameObject towerUpper;
	private GameObject enemy;
	private int enemyInstanceID;
	private bool mTileActive;		
	private Vector2 mTileCoord;		// index in tilemap array
	private int mTotalColumns;		// total columns in tilemap
	
	private bool mTempo;
	private bool mClick;
	private bool mHover;
	private bool mPath;
	
	private bool mUpgradedTower;
	
	public bool mouse;
	public Material standard;
	public Material click;
	public Material hover;
	public Material tempo;
	public Material path;
	
	void Start () 
	{	
		mTowerManager = GameObject.Find("Main Camera").GetComponent<TowerManager>();
		mLevelManager = GameObject.Find("Main Camera").GetComponent<LevelManager>();
		mGUIManager = GameObject.Find("Main Camera").GetComponent<GUIManager>();
		mTileManager = GameObject.Find("Main Camera").GetComponent<TileManager>();
		
		mTileActive = false;
		mTempo = false;
		mClick = false;
		mHover = false;
		
		mUpgradedTower = false;
	}
	
	void Update () 
	{
		if (mClick) renderer.material = click;
		else if (mHover) renderer.material = hover;
		else if (mTempo) renderer.material = tempo;
		else if (mPath) renderer.material = path;
		else renderer.material = standard;
		
		if (mUpgradedTower)
		{
			mGUIManager.SetUpgradeInfoMenu(tower.GetComponent<Tower>());
			mUpgradedTower = false;
		}
	}

	public bool GetActive() { return mTileActive; }
	public GameObject GetEnemy() { return enemy; }
	public int GetEnemyInstanceID() { return enemyInstanceID; }
	public Vector2 GetTileCoord() { return mTileCoord; }
	public int GetTotalColumns() { return mTotalColumns; }
	public GameObject GetTower() { return tower; }

	public void SetClick(bool click) { mClick = click; }
	public void SetHover(bool hover) { mHover = hover; }
	public void SetTempo(bool tempo) { mTempo = tempo; }
	public void SetTileCoord(int x, int y) { mTileCoord = new Vector2(x, y); }
	public void SetTotalColumns(int totalColumns) { mTotalColumns = totalColumns; }
	
	// Trigger
	
	private void OnTriggerEnter(Collider other)
	{
		if (!mTileActive)
		{
			enemyInstanceID = other.gameObject.GetInstanceID();
			enemy = other.gameObject;
			enemy.renderer.enabled = true;
			mTileActive = true;
		}
	}
	
	private void OnTriggerExit(Collider other)
	{
		if (mTileActive)
		{
			enemy = null;
			mTileActive = false;	
		}
	}
	
	// Mouse Events
	
	public void OnMouseDown()
	{
		if (mouse &&  mLevelManager != null && !mLevelManager.GetPause())
		{
			GameObject go = GameObject.Find("Main Camera");
			go.GetComponent<TileManager>().SetLastActive(gameObject);
			mClick = true;
			
			if (tower) mGUIManager.SetUpgradeInfoMenu(tower.GetComponent<Tower>());
			else mGUIManager.SetTowerMenu();
		}
	}
	
	public void OnMouseOver()
	{
		if (mouse && mLevelManager != null && !mLevelManager.GetPause())
		{
			mTileManager.SetLastHover(gameObject);
			mHover = true;
		}
	}
	
	public void OnMouseExit()
	{
		if (mouse)
		{	
			mTileManager.SetLastHover(null);
			mHover = false;
		}
	}
		
	// Other
	
	public void BuildTower()
	{
		GameObject t = mTowerManager.GetCurrentTower();
		
		if (t)
		{
			float worth = t.GetComponent<Tower>().GetBuy();
			
			if (!mTileActive && 
				mLevelManager.GetBank() >= worth)
			{
				mTileActive = true;
				
				tower = mTowerManager.GetCurrentTower();
				
				tower = (GameObject)Instantiate(t, 
					new Vector3(transform.position.x, renderer.bounds.size.y, transform.position.z), transform.rotation);
				
				//tower.transform.Translate(transform.position.x, renderer.bounds.size.y, transform.position.z);
				tower.GetComponent<Tower>().SetSound(mTowerManager.GetCurrentSound());
				tower.GetComponent<Tower>().SetTile(gameObject);
				tower.GetComponent<Tower>().SetTotalCost(worth);
				
				mTowerManager.AddTile(this);
				mLevelManager.DecrementBank(worth);
				
				//mGUIManager.SetUpgradeInfoMenu(tower.GetComponent<Tower>());
			}
		}
	}
	
	public void UpgradeTower()
	{
		GameObject t = mTowerManager.GetCurrentTower();
		
		t = tower;
		
		if (t)
		{
			float worth = t.GetComponent<Tower>().GetUpgradeCost();//mTowerManager.GetCurrentTower().GetComponent<Tower>().GetUpgradeCost();
			
			if (mTileActive && 
				mLevelManager.GetBank() >= worth &&
				tower.GetComponent<Tower>().GetUpgradeObject() != null)
			{
				mTileActive = true;
				
				Tower prevTower = tower.GetComponent<Tower>();
				
				float totalCost = prevTower.GetTotalCost();
				
				Destroy(tower);
				
				tower = prevTower.GetUpgradeObject();//mTowerManager.GetCurrentTower();
				tower = (GameObject)Instantiate(tower, 
					new Vector3(transform.position.x, renderer.bounds.size.y, transform.position.z), transform.rotation);
				
				Tower towerScript = tower.GetComponent<Tower>();
				
				towerScript.SetSound(prevTower.GetUpgradeSound());
				towerScript.SetTile(gameObject);
				towerScript.SetUpgrade(prevTower.GetUpgrade());
				towerScript.IncrementUpgrade(1);
				towerScript.SetTotalCost(worth + totalCost);
				
				mTowerManager.AddTile(this);
				mLevelManager.DecrementBank(worth);
				
				mGUIManager.SetUpgradeInfoMenu(tower.GetComponent<Tower>());
				
				//mUpgradedTower = true;
			}
		}
	}
	
	public void DestroyTower(bool sell)
	{
		if (mTileActive)
		{
			if (sell) mLevelManager.IncrementBank(tower.GetComponent<Tower>().GetSell());//towerUpper.GetComponent<Tower>().GetSell());
			
			mTileActive = false;
			Destroy(tower);
		}
	}
	
	public void ActivatePath()
	{
		mPath = true;
	}
	
	public void DeactivatePath()
	{
		mPath = false;
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class TileManager : MonoBehaviour 
{
	// Private
	
	private GUIManager mGUIManager;
	
	private bool mLoadSuccess;
	private TileMap mTileMap;
	private TilePath mPath;
	private TilePath mPathTwo;
	private GameObject tileActive;
	private GameObject tileHover;
	private float mTileWidth;
	private float mTileHeight;
	
	// Public
	
	public TextAsset idFile;
	public TextAsset tileMapFile;
	public TextAsset pathOneFile;
	public TextAsset pathTwoFile;
	
	public KeyCode buildKey;
	public KeyCode upgradeKey;
	public KeyCode sellKey;
	
	void Awake ()
	{
		mGUIManager = gameObject.GetComponent<GUIManager>();
		
		tileActive = null;
		
		if (idFile && tileMapFile)
		{
			mTileMap = new TileMap(idFile, tileMapFile, "Prefabs/Tiles/");
			mTileMap.Start();
			mLoadSuccess = mTileMap.GetLoadSuccess();
			
			if (pathOneFile && mLoadSuccess)
			{
				mPath = new TilePath(pathOneFile, mTileMap.GetRows(), mTileMap.GetColumns());
				mPath.Start();
				mLoadSuccess = mPath.GetLoadSuccess();
			}
			if (pathTwoFile && mLoadSuccess)
			{
				mPathTwo = new TilePath(pathTwoFile, mTileMap.GetRows(), mTileMap.GetColumns());
				mPathTwo.Start();
				mLoadSuccess = mPathTwo.GetLoadSuccess();
			}
		}
		else
			mLoadSuccess = false;
		
		mTileWidth = mTileMap.GetTileSize();
		mTileHeight = mTileMap.GetTileHeight();
	}

	void FixedUpdate ()
	{
		if (mLoadSuccess && tileActive)
		{
			if (Input.GetKeyUp(buildKey))
				tileActive.GetComponent<Tile>().BuildTower();
			if (Input.GetKeyUp(upgradeKey))
				tileActive.GetComponent<Tile>().UpgradeTower();
			if (Input.GetKeyUp(sellKey))
			{
				tileActive.GetComponent<Tile>().DestroyTower(true);
				mGUIManager.SetTowerMenu();
			}
		}
	}
	
	public void ActivatePath(int path)
	{
		if (path == 1)
		{
			if (mPathTwo != null)
			{
				for (int i = 0; i < mPathTwo.GetPathCount(); i++)
					mTileMap.GetTile((int)mPathTwo.GetPath(i).x, (int)mPathTwo.GetPath(i).y).GetComponent<Tile>().DeactivatePath();
				for (int i = 0; i < mPath.GetPathCount(); i++)
					mTileMap.GetTile((int)mPath.GetPath(i).x, (int)mPath.GetPath(i).y).GetComponent<Tile>().ActivatePath();
			}
		}
		else if (path == 2)
		{
			if (mPath != null)
			{
				for (int i = 0; i < mPath.GetPathCount(); i++)
					mTileMap.GetTile((int)mPath.GetPath(i).x, (int)mPath.GetPath(i).y).GetComponent<Tile>().DeactivatePath();
				for (int i = 0; i < mPathTwo.GetPathCount(); i++)
					mTileMap.GetTile((int)mPathTwo.GetPath(i).x, (int)mPathTwo.GetPath(i).y).GetComponent<Tile>().ActivatePath();	
			}
		}
	}
	
	public GameObject GetActiveTile() { return tileActive; }
	public GameObject GetHoverTile() { return tileHover; }
	public TileMap GetTileMap() { return mTileMap; }
	public TilePath GetTilePathOne() { return mPath; }
	public TilePath GetTilePathTwo() { return mPathTwo;	}
	public float GetTileWidth() { return mTileWidth; }
	public float GetTileHeight() { return mTileHeight; }
	public int GetTileColumnsCount() { return mTileMap.GetColumns(); }
	
	public void SetLastActive(GameObject lastActive)
	{
		if (this.tileActive != null)
			this.tileActive.GetComponent<Tile>().SetClick(false);
		this.tileActive = lastActive;
		
		GameObject tower = tileActive.GetComponent<Tile>().GetTower();
		if (tower) mGUIManager.SetUpgradeInfoMenu(tower.GetComponent<Tower>());
		//else mGUIManager.SetTowerMenu();
	}
	
	public void SetLastHover(GameObject lastHover)
	{
		this.tileHover = lastHover;
	}
}
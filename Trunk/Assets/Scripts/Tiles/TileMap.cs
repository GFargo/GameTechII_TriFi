using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileMap 
{
	private bool mLoadSuccess;
	private GameObject[,] mTileMap;
	private Dictionary<int, GameObject> mTileIDDictionary;
	private TextAsset mIDFile;
	private TextAsset mTileMapFile;
	private string mPrefabDirectory;
	private int mColumns;
	private int mRows;
	
	// Constructors
	public TileMap(TextAsset IDFile, TextAsset tileMapFile, string prefabDirectory)
	{
		mLoadSuccess = false;
		mTileIDDictionary = new Dictionary<int, GameObject>();
		mIDFile = IDFile;
		mTileMapFile = tileMapFile;
		mPrefabDirectory = prefabDirectory;
	}

	// Intialization
	public void Start () 
	{	
		LoadID();	// load data...
		mLoadSuccess = LoadSize();
		if (mLoadSuccess)
			LoadMap();
	}
	
	// Accessors
	
	public bool GetLoadSuccess() { return mLoadSuccess; }
	public int GetRows()    { return mRows; }
	public int GetColumns() { return mColumns; }
	
	public GameObject GetTile(int ID)
	{
		if (mTileIDDictionary.ContainsKey(ID))
			return mTileIDDictionary[ID];
		return null;
	}
	
	public GameObject GetTile(int x, int y)
	{
		if (x >= 0 && y >= 0 && x < mColumns && y < mRows)
			return mTileMap[x, y];
		return null;
	}
	
	public float GetTileSize()
	{
		if (mRows > 0 && mColumns > 0)
			return mTileMap[0, 0].renderer.bounds.size.x;//mTileMap[0, 0].transform.localScale.x;
		return 0.0f;
	}
	
	public float GetTileHeight()
	{
		if (mRows > 0 && mColumns > 0)
			return mTileMap[0, 0].renderer.bounds.size.y;//mTileMap[0, 0].transform.localScale.x;
		return 0.0f;
	}
	
	public void ActivateTileColumn(int column)
	{
		if (mColumns > 0 && mRows > 0)
		{
			for (int i = 0; i < mRows; i++)
			{
				mTileMap[column, i].GetComponent<Tile>().SetTempo(true);
				if (column > 0) mTileMap[column - 1, i].GetComponent<Tile>().SetTempo(false);
				else mTileMap[mColumns - 1, i].GetComponent<Tile>().SetTempo(false);
			}
		}
	}
	
	#region Load Data
	
	private bool CheckString(string str)
	{
		if (str != "" && str != "\n" && str != null)
			return true;
		return false;
	}
	
	private GameObject InstantiateTile(int ID, int column, int row)
	{
		GameObject go = null; 
		
		if (mTileIDDictionary.ContainsKey(ID))
		{	
			Vector3 scale = mTileIDDictionary[ID].renderer.bounds.size;//mTileIDDictionary[ID].transform.localScale;
			go =  GameObject.Instantiate(mTileIDDictionary[ID],
				new Vector3(column * (scale.x) - ((scale.x) * (mColumns/2)), 
				0, 
				-row * (scale.z) + ((scale.z) * (mRows/2))), 
				new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as GameObject;
			go.GetComponent<Tile>().SetTileCoord(column, row);
			go.GetComponent<Tile>().SetTotalColumns(mColumns);
		}
		
		return go;
	}
	
	private void LoadID() // creates dictionary for lookup by tilemap
	{	
		string[] lines = mIDFile.text.Split('\n'); 
		
		for (int i = 0; i < lines.Length; i++)
		{
			if (CheckString(lines[i])) // error checking
			{
				string[] bits = lines[i].Split(',');

				if (bits.Length == 2) // error checking
				{
					GameObject go = Resources.Load(mPrefabDirectory + bits[1].Trim()) as GameObject;
					if (go == null) go = Resources.Load(mPrefabDirectory + "Default") as GameObject; // check if prefab exists
					mTileIDDictionary.Add(int.Parse(bits[0]), go);
				}
			}
		}
	}
	
	private void LoadMap()
	{	
		mTileMap = new GameObject[mColumns, mRows];
		int r = 0, c = 0; // rows, columns
		string[] lines = mTileMapFile.text.Split('\n');
		
		for (int i = 0; i < lines.Length; i++)
		{
			if (CheckString(lines[i])) // error checking
			{
				string[] bits = lines[i].Split(',');
				for (c = 0; c < mColumns; c++)
					mTileMap[c, r] = InstantiateTile(int.Parse(bits[c]), c, r); // adding tiles to array
				r++;
			}
		}
	}
	
	private bool LoadSize()
	{
		string[] lines = mTileMapFile.text.Split('\n');
		
		int i = 0;
		
		for (i = 0; i < lines.Length; i++)
		{
			if (CheckString(lines[i])) // error checking
			{
				string line = lines[i];
				if (mColumns == 0)
					mColumns = line.Split(',').Length;
				else if (mColumns != line.Split(',').Length)
				{
					string error = mTileMapFile.name + " (" + mTileMapFile.GetType() + ") " + "has invalid data";
					Debug.LogError(error);
					return false;
				}
				mRows++;
			}
		}
		return true;
	}
	
	#endregion
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TilePath
{
	private bool mLoadSuccess;
	private List<Vector2> mPath;
	private int[,] mPathMap;
	private TextAsset mPathFile;
	private int mRows;
	private int mColumns;
	
	// Constructors
	public TilePath(TextAsset pathFile, int rows, int columns)
	{
		mPath = new List<Vector2>();
		mPathFile = pathFile;
		mLoadSuccess = LoadSize(rows, columns);
	}
	
	// Initialization
	public void Start () 
	{
		if (mLoadSuccess)
		{
			LoadMap();
			mLoadSuccess = FindStart();
			FindPath();
		}
	}
	
	// Accessors
	public bool GetLoadSuccess()
	{
		return mLoadSuccess;
	}
	
	public Vector2 GetPath(int index)
	{
		if (mPath.Count >= index)
			return mPath[index];
		return Vector2.zero;
	}
	
	public int GetPathCount()
	{
		return mPath.Count;
	}
	
	// Other
	private bool CheckTile(int count, int x, int y)
	{
		if (mPathMap[x, y] == (count + 1))
			return true;
		return false;
	}
	
	private bool CheckString(string str)
	{
		if (str != "" && str != "\n" && str != null)
			return true;
		return false;
	}
	
	private void FindPath()
	{		
		if (mPath.Count > 0)
		{
			bool found = true;
			int count = 1, currentX = 0, currentY = 0, tempX = 0, tempY = 0;
			currentX = (int)mPath[0].x;
			currentY = (int)mPath[0].y;
			
			while (found)
			{
				found = false;
				
				if (currentY - 1 >= 0 && !found)
				{
					tempY = currentY - 1;
					if (CheckTile(count, currentX, tempY))
					{
						found = true;
						mPath.Add(new Vector2(currentX, tempY));
						currentY = tempY;
						count++;
					}
				}
				if (currentX + 1 < mColumns && !found)
				{
					tempX = currentX + 1;
					if (CheckTile(count, tempX, currentY))
					{
						found = true;
						mPath.Add(new Vector2(tempX, currentY));
						currentX = tempX;
						count++;
					}
				}
				if (currentY + 1 < mRows && !found)
				{
					tempY = currentY + 1;
					if (CheckTile(count, currentX, tempY))
					{
						found = true;
						mPath.Add(new Vector2(currentX, tempY));
						currentY = tempY;
						count++;
					}
				}
				if (currentX - 1 >= 0 && !found)
				{
					tempX = currentX - 1;
					if (CheckTile(count, tempX, currentY))
					{
						found = true;
						mPath.Add(new Vector2(tempX, currentY));
						currentX = tempX;
						count++;
					}
				}
			}
		}
	}
	
	private bool FindStart()
	{
		for (int r = 0; r < mRows; r++)				// search for start
		{
			for (int c = 0; c < mColumns; c++)
			{
				if (mPathMap[c, r] == 1)
				{
					mPath.Add(new Vector2(c, r));
					return true;
				}
			}
		}
	
		Debug.LogError(mPathFile.name + " (" + mPathFile.GetType() + ") " + "start not found");
		return false;
	}
	
	private void LoadMap()
	{
		int i = 0, r = 0, c = 0; // rows, columns
		string[] lines = mPathFile.text.Split('\n');
		
		mPathMap = new int[mColumns, mRows];
		
		for (i = 0; i < lines.Length; i++)
		{
			if (CheckString(lines[i])) // error checking
			{
				string[] bits = lines[i].Split(',');
				
				for (c = 0; c < mColumns; c++)
					mPathMap[c, r] = int.Parse(bits[c]);
				r++;
			}
		}
	}
	
	private bool LoadSize(int rows, int columns)
	{	
		string[] lines = mPathFile.text.Split('\n');
		
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
					Debug.LogError(mPathFile.name + " (" + mPathFile.GetType() + ") " + "has invalid data");
					return false;
				}
				mRows++;
			}
		}
		
		if (mRows != rows || mColumns != columns)
		{
			Debug.LogError(mPathFile.name + " (" + mPathFile.GetType() + ") " + "has invalid data; does not match tile map size");
			return false;
		}
		return true;
	}
}

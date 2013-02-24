using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour 
{
	private LevelManager mLevelManager;
	private TempoManager mTempoManager;
	
	private int mTotalWaveCount;
	
	private List<Wave> mWaveList;
	private int mCurrentWaveCount;
	
	public TextAsset waveFile;
	
	void Start()
	{
		mLevelManager = gameObject.GetComponent<LevelManager>();
		mTempoManager = gameObject.GetComponent<TempoManager>();
		
		mCurrentWaveCount = 1;
		mWaveList = new List<Wave>();
		LoadSize();
		LoadWaves();
	}
	
	void Update () 
	{ 
		if (mWaveList.Count == 0)
			mLevelManager.LoadNext();
	}
	
	private bool CheckString(string str)
	{
		if (str != "" && str != "\n" && str != null)
			return true;
		return false;
	}
	
	private void LoadWaves()
	{
		string[] lines = waveFile.text.Split('\n');
		
		for (int i = 0; i < lines.Length; i++)
		{
			if (CheckString(lines[i])) // error checking
			{
				string[] bits = lines[i].Split(',');
				if (bits.Length == 4)
					mWaveList.Add(new Wave(int.Parse(bits[0]), bits[1].Trim(), 
                        int.Parse(bits[2]), int.Parse(bits[3])));
			}
		}
	}
	
	private bool LoadSize()
	{
		string[] lines = waveFile.text.Split('\n');
		
		mTotalWaveCount = 0;
		
		for (int i = 0; i < lines.Length; i++)
		{
			if (CheckString(lines[i])) // error checking
				mTotalWaveCount++;
		}
		return true;
	}
	
	public Wave GetCurrentWave() { return (mWaveList.Count > 0 ? mWaveList[0] : null); }
	public int GetCurrentWaveCount() { return mCurrentWaveCount; }
	public int GetWaveCount() { return mTotalWaveCount; }
	
	public void IterateWave()
	{
		if (mWaveList.Count > 0 && mWaveList[0].GetEnemyCount() <= 0)
		{
			mCurrentWaveCount++;
			mTempoManager.ResetSpawnDelay();
			mWaveList.RemoveAt(0);
		}
	}
}

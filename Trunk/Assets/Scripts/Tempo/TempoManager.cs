using UnityEngine;
using System.Collections;

public class TempoManager : MonoBehaviour 
{
	private const int FIRE_COUNT = 4;
	
	private TileManager mTileManager;
	
	private bool[] mFire;
	private float mTimePerBeat;
	private float mCurrentTime;
	private int mTotalTileCount;
	private int mSixteenthCount;
	private int mCurrentTileCount;
	private float mBeatsPerMinuteDivided;
	private float mCurrentDelay;
	
	public int beatsPerMinute;
	public float startDelay;
	
	void Start() 
	{	
		mTileManager = gameObject.GetComponent<TileManager>();
		mTotalTileCount = mTileManager.GetTileColumnsCount();
		
		mFire = new bool[FIRE_COUNT];
		
		float bps = beatsPerMinute / 60.0f;
		mTimePerBeat = 1.0f / bps;

		//startDelay = 2;
		startDelay = ((startDelay * mTimePerBeat) * mTotalTileCount) * -1;
		mCurrentDelay = startDelay;
		
		mTimePerBeat /= (float)FIRE_COUNT;
		
		mCurrentTime = 0.0f;
		mSixteenthCount = 0;
		mCurrentTileCount = 0;
		
		mBeatsPerMinuteDivided = beatsPerMinute / 100.0f;
	}
	
	void Update() 
	{	
		if (Time.timeScale != 0)
		{
			for (int i = 0; i < FIRE_COUNT; i++)
				mFire[i] = false;
			
			mTileManager.GetTileMap().ActivateTileColumn(mCurrentTileCount);
			
			if (mSixteenthCount == 0 && mCurrentTime == 0.0f)
				mFire[0] = true;
			
			mCurrentTime += Time.deltaTime;
			//mCurrentDelay += Time.deltaTime;
			//if (mCurrentDelay > 0) mCurrentDelay = 0.0f;
			
			if (!mFire[0])
			{
				if (mCurrentTime >= mTimePerBeat)
				{
					mCurrentDelay += mTimePerBeat;
					if (mCurrentDelay > 0) mCurrentDelay = 0.0f;
					
					mCurrentTime = 0.0f;
					mSixteenthCount++;
					
					if (mSixteenthCount % FIRE_COUNT == 0)
					{
						mSixteenthCount = 0;
						
						mCurrentTileCount++;
						if (mCurrentTileCount == mTotalTileCount)
							mCurrentTileCount = 0;
					}
					else if (mSixteenthCount % 3 == 0)
						mFire[3] = true;
					else if (mSixteenthCount % 2 == 0) 
						mFire[2] = true;
					else if (mSixteenthCount % 1 == 0)
						mFire[1] = true;
					
				}
			}
		}
	}
	
	public int GetBeatsPerMinute() { return beatsPerMinute; }
	public float GetBeatsPerMinuteDivided() { return mBeatsPerMinuteDivided; }
	public int GetCurrentTileCount() { return mCurrentTileCount; }
	public float GetCurrentTime() { return mCurrentTime + mCurrentDelay; }
	public float GetCurrentDelay() { return mCurrentDelay; }
	public float GetTotalDelay() { return startDelay; }
	public void ResetSpawnDelay() { mCurrentDelay = startDelay; }
	public bool GetFire(int num) { return mFire[num < 4 ? num : 0]; }
}
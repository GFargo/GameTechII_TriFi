using UnityEngine;
using System.Collections;

public class Wave 
{
	private int mPathNumber;
	private string mEnemyType;
	private int mEnemyCount;
	private int mEnemyTime;
	private int mEnemySpeed;
	private GameObject mEnemyObject;
	
	public Wave(int pathNumber, string enemyType, int enemyCount, int enemyTime)
	{
		mPathNumber = pathNumber;
		mEnemyType = enemyType;
		mEnemyCount = enemyCount;
		mEnemyTime = enemyTime;
		
		mEnemyObject = Resources.Load("Prefabs/Enemies/" + enemyType) as GameObject;
	}

	void Update()
	{
		
	}
	
	public int GetPathNumber() { return mPathNumber; }
	public string GetEnemyType() { return mEnemyType; }
	public int GetEnemyCount() { return mEnemyCount; }
	public int GetEnemyTime() { return mEnemyTime; }
	public GameObject GetEnemyObject() { return mEnemyObject; }
	
	public void DecrementEnemyCount(int decrement) { mEnemyCount -= decrement; }
	
}

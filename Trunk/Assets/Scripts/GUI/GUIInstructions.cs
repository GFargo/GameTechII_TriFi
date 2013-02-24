using UnityEngine;
using System.Collections;

public class GUIInstructions : MonoBehaviour 
{
	private LevelManager mLevelManager;
	
	void Start()
	{
		mLevelManager = GameObject.Find("Main Camera").GetComponent<LevelManager>();
	}
		
	void Update()
	{
		if (Time.timeScale == 1)
		{
			Destroy(gameObject);
			mLevelManager.TogglePause();
		}
	}
	
	void OnMouseUpAsButton()
	{
		mLevelManager.SetButtonRender(true);
		Destroy(gameObject);
	}
}

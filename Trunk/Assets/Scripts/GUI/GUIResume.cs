using UnityEngine;
using System.Collections;

public class GUIResume : MonoBehaviour 
{
	private LevelManager mLevelManager;
	
	public Texture resumeTex;
	public Texture resumeHoverTex;
	
	void Start()
	{
		mLevelManager = GameObject.Find("Main Camera").GetComponent<LevelManager>();	
	}
	
	void OnMouseEnter()
	{
		guiTexture.texture = resumeHoverTex;
	}
	
	void OnMouseExit()
	{
		guiTexture.texture = resumeTex;	
	}
	
	void OnMouseUpAsButton()
	{
		if (mLevelManager != null)
			mLevelManager.TogglePause();
	}
}
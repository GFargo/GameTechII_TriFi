using UnityEngine;
using System.Collections;

public class GUIStart : MonoBehaviour 
{
	private LevelManager mLevelManager;
	
	public Texture startTex;
	public Texture startHoverTex;
	
	void Start()
	{
		mLevelManager = GameObject.Find("Main Camera").GetComponent<LevelManager>();	
	}
	
	void OnMouseEnter()
	{
		guiTexture.texture = startHoverTex;
	}
	
	void OnMouseExit()
	{
		guiTexture.texture = startTex;
	}
	
	void OnMouseUpAsButton()
	{
		GameObject.Find("Spotlight").GetComponent<Light>().intensity = 2;
		Time.timeScale = 1;
		mLevelManager.SetVolume(1);
	}
}

using UnityEngine;
using System.Collections;

public class GUILevelComplete : MonoBehaviour 
{
	public Texture levelCompleteTex;
	public Texture levelCompleteHoverTex;
	
	void OnMouseEnter()
	{
		guiTexture.texture = levelCompleteHoverTex;
	}
	
	void OnMouseExit()
	{
		guiTexture.texture = levelCompleteTex;
	}
	
	void OnMouseUpAsButton()
	{
		int level = Application.loadedLevel + 1;
		
		if (level < Application.levelCount) Application.LoadLevel(level);
		else Application.LoadLevel("MainMenu");
	}
}

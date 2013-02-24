using UnityEngine;
using System.Collections;

public class GUIQuit : MonoBehaviour 
{
	public Texture quitTex;
	public Texture quitHoverTex;
	
	void OnMouseEnter()
	{
		guiTexture.texture = quitHoverTex;
	}
	
	void OnMouseExit()
	{
		guiTexture.texture = quitTex;	
	}
	
	void OnMouseUpAsButton()
	{	
		Time.timeScale = 1;	
		Application.LoadLevel("MainMenu");
	}
}

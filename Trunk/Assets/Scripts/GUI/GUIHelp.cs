using UnityEngine;
using System.Collections;

public class GUIHelp : MonoBehaviour 
{
	private GameObject mInstructions;
	private LevelManager mLevelManager;
		
	public Texture helpTex;
	public Texture helpHoverTex;
	public GameObject instructions;
	
	void Start()
	{
		mLevelManager = GameObject.Find("Main Camera").GetComponent<LevelManager>();
	}
	
	void OnMouseEnter()
	{
		guiTexture.texture = helpHoverTex;
	}
	
	void OnMouseExit()
	{
		guiTexture.texture = helpTex;
	}
	
	void OnMouseUpAsButton()
	{
		GameObject.Instantiate(instructions);
		mLevelManager.SetButtonRender(false);
	}
}
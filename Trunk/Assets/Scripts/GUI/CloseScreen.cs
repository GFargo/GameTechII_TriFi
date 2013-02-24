using UnityEngine;
using System.Collections;

public class CloseScreen : MonoBehaviour 
{
	private GUIManager mGUIManager;
	
	void Start() 
	{
		mGUIManager = GameObject.Find("Main Camera").GetComponent<GUIManager>();
	}
	
	void OnMouseUpAsButton()
	{
		mGUIManager.SetTowerMenu();
	}
}
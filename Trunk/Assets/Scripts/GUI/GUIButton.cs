using UnityEngine;
using System.Collections;

public class GUIButton : MonoBehaviour 
{
	protected GUIManager mGUIManager;
	protected LevelManager mLevelManager;
	protected TileManager mTileManager;
	protected TowerManager mTowerManager;
	
	protected bool mActive;
	protected bool mHover;
	
	public Material inactiveMaterial;
	public Material hoverMaterial;
	
	protected virtual void Start() 
	{ 
		mGUIManager = GameObject.Find("Main Camera").GetComponent<GUIManager>();
		mLevelManager = GameObject.Find("Main Camera").GetComponent<LevelManager>();
		mTileManager = GameObject.Find("Main Camera").GetComponent<TileManager>();
		mTowerManager = GameObject.Find("Main Camera").GetComponent<TowerManager>();
		
		mActive = false;
		mHover = false;
	}
	
	protected virtual void Update()
	{	
		if (mHover) renderer.material = hoverMaterial;
		else  renderer.material = inactiveMaterial;
	}
	
	protected virtual void OnMouseUpAsButton() { if (mLevelManager && !mLevelManager.GetPause()) mActive = true; }
	protected virtual void OnMouseEnter() { if (mLevelManager && !mLevelManager.GetPause()) mHover = true; }
	protected virtual void OnMouseExit() { if (mLevelManager && !mLevelManager.GetPause()) mHover = false; }
	protected virtual void OnMouseDown() { mHover = false; }
	protected virtual void OnMouseDrag() { }
	protected virtual void OnMouseUp() { }
	
	public bool GetActive() { return mActive; }
	public void SetActive(bool active) { mActive = active; }
}
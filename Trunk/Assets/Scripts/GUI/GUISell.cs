using UnityEngine;
using System.Collections;

public class GUISell : GUIButton 
{
	
	protected override void Start()
	{
		base.Start();
	}
	
	protected override void OnMouseUpAsButton()
	{
		if (!mLevelManager.GetPause())
		{
			GameObject tile = mTileManager.GetActiveTile();
			if (tile) tile.GetComponent<Tile>().DestroyTower(true);
			mGUIManager.SetTowerMenu();
		}
	}
}
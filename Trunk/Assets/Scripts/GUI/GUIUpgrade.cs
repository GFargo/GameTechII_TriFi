using UnityEngine;
using System.Collections;

public class GUIUpgrade : GUIButton 
{
	//private TileManager mTileManager;
	
	protected override void Start()
	{
		//mTileManager = GameObject.Find("Main Camera").GetComponent<TileManager>();
		base.Start();
	}
	
	protected override void OnMouseUpAsButton()
	{
		if (!mLevelManager.GetPause())
		{
			GameObject tile = mTileManager.GetActiveTile();
			if (tile) tile.GetComponent<Tile>().UpgradeTower();
		}
	}
}
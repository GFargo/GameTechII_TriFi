using UnityEngine;
using System.Collections;

public class GUIClose : GUIButton 
{
	protected override void OnMouseUpAsButton()
	{
		if (!mLevelManager.GetPause())
		{
			mHover = false;
			mGUIManager.SetTowerMenu();
		}
		base.OnMouseUpAsButton();
	}
}

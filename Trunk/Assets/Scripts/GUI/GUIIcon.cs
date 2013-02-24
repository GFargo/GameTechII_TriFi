using UnityEngine;
using System.Collections;

public class GUIIcon : GUIButton
{
	private Vector3 mTowerMenuPosition;
	
	private GameObject mCursorIcon;
	private Vector3 mOffset;
	
	public TOWER type;
	public KeyCode key;
	public GameObject cursorIcon;
	
	protected override void Start()
	{
		mTowerMenuPosition = transform.position;
		mCursorIcon = null;
		base.Start();
	}
	
	protected override void Update()
	{
		if (Input.GetKeyDown(key))
			OnMouseUpAsButton();
		if (mCursorIcon)
		{
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
			RaycastHit hit = new RaycastHit();
			
			if (Physics.Raycast(ray, out hit, 200))
				mCursorIcon.transform.position = ray.GetPoint(hit.distance);
		}
		
		base.Update();
	}
	
	protected override void OnMouseDrag() 
	{ 
		if (mLevelManager && !mLevelManager.GetPause()) 
		{
			mTowerManager.SetCurrentTower(gameObject.GetComponent<GUIIcon>().GetTowerType());
			
			if (mCursorIcon == null)
			{
				Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
				RaycastHit hit = new RaycastHit();
			
				if (Physics.Raycast(ray, out hit, 200))
					mCursorIcon = GameObject.Instantiate(cursorIcon, ray.GetPoint(hit.distance), Quaternion.identity) as GameObject;
				
				mCursorIcon.transform.Rotate(new Vector3(0, 180, 0));
			}
		} 
	}
	
	protected override void OnMouseUp() 
	{ 
		if (mLevelManager && !mLevelManager.GetPause())
		{
			GameObject hoverTile = mTileManager.GetHoverTile();
			if (hoverTile != null)
				hoverTile.GetComponent<Tile>().BuildTower();
			if (mCursorIcon)
				Destroy(mCursorIcon);
		}
	}
	
	protected override void OnMouseUpAsButton()
	{
		if (!mLevelManager.GetPause() && !mGUIManager.GetUpgradeMenuAcitve())
			mGUIManager.SetActiveIcon(gameObject, MENU.BUY_INFO);
		base.OnMouseUpAsButton();
	}
	
	public Vector3 GetMenuPosition() { return mTowerMenuPosition; }
	public TOWER GetTowerType() { return type; }
	public void SetTowerMenu() { transform.position = mTowerMenuPosition; }
}

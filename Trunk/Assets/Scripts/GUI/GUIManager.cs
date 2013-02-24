using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MENU
{
	MAIN,
	BUY_INFO,
	UPGRADE_INFO
}

public class GUIManager : MonoBehaviour 
{
	private LevelManager mLevelManager;
	private TowerManager mTowerManager;
	
	private GameObject mCurrentIcon;
	private GameObject mCurrentUpgradeIcon;
	private List<GameObject> mTowerIconsList;
	private GameObject mBuyButton;
	private GameObject mCloseButton;
	private GameObject mSellButton;
	private GameObject mUpgradeButton;
	private GameObject mType;
	private GameObject mCurrent;
	private GameObject mUpgrade;
	private GameObject mUpgradeValue;
	private GameObject mSellValue;
	private GameObject mTextRight;
	
	private bool mUpgradeMenuActive;
	
	public List<GameObject> towerIconList;
	public GameObject buyButton;
	public GameObject closeButton;
	public GameObject sellButton;
	public GameObject upgradeButton;
	public GameObject type;
	public GameObject current;
	public GameObject upgrade;
	public GameObject upgradeValue;
	public GameObject sellValue;
	public GameObject textRight;
	
	void Start() 
	{
		mLevelManager = gameObject.GetComponent<LevelManager>();
		mTowerManager = gameObject.GetComponent<TowerManager>();

		mTowerIconsList = new List<GameObject>();
		
		for (int i = 0; i < towerIconList.Count; i++)
			mTowerIconsList.Add(Instantiate(towerIconList[i]) as GameObject);
		
		mCloseButton = Instantiate(closeButton) as GameObject;
		mCloseButton.active = false;
		mBuyButton = Instantiate(buyButton) as GameObject;
		mBuyButton.active = false;
		mSellButton = Instantiate(sellButton) as GameObject;
		mSellButton.active = false;
		mUpgradeButton = Instantiate(upgradeButton) as GameObject;
		mUpgradeButton.active = false;
		
		mUpgradeMenuActive = false;
		
		mType = GameObject.Instantiate(type) as GameObject;
		mType.GetComponent<TextMesh>().text = " ";
		
		mCurrent = GameObject.Instantiate(current) as GameObject;
		mCurrent.GetComponent<TextMesh>().text = " ";
		
		mUpgrade = GameObject.Instantiate(upgrade) as GameObject;
		mUpgrade.GetComponent<TextMesh>().text = " ";
		
		mUpgradeValue = GameObject.Instantiate(upgradeValue) as GameObject;
		mUpgradeValue.GetComponent<TextMesh>().text = " ";
		
		mSellValue = GameObject.Instantiate(sellValue) as GameObject;
		mSellValue.GetComponent<TextMesh>().text = " ";
		
		mTextRight = GameObject.Instantiate(textRight) as GameObject;
		mTextRight.GetComponent<TextMesh>().text = " ";
		
		mCurrentIcon = null;
		mCurrentUpgradeIcon = null;

		mTextRight.GetComponent<TextMesh>().text = 
			"Level:" + mLevelManager.GetLevelNumber() + 
			"\nBank:" + mLevelManager.GetBank();
	}
	
	void FixedUpdate() 
	{	
		mTextRight.GetComponent<TextMesh>().text = 
			"Level:" + mLevelManager.GetLevelNumber() + 
			"\nBank:" + mLevelManager.GetBank();
	}
	
	public void SetActiveIcon(GameObject currentIcon, MENU type)
	{
		if (mTowerIconsList.Count > 0)
		{
			mCurrentIcon = currentIcon;	
			mTowerManager.SetCurrentTower(mCurrentIcon.GetComponent<GUIIcon>().GetTowerType());
			
			DeactivateButtons();
			
			mCurrentIcon.active = true;
			mCurrentIcon.transform.position = mTowerIconsList[0].GetComponent<GUIIcon>().GetMenuPosition();
			
			if (type == MENU.BUY_INFO) SetBuyInfoMenu();
		}
	}
	
	public void SetTowerMenu()
	{
		DeactivateButtons();
		
		for (int i = 0; i < mTowerIconsList.Count; i++)
		{
			mTowerIconsList[i].GetComponent<GUIIcon>().SetTowerMenu();
			mTowerIconsList[i].active = true;
			mTowerIconsList[i].GetComponent<GUIIcon>().SetActive(false);
		}
		
		mTowerManager.SetCurrentTower(TOWER.NONE);
		mType.GetComponent<TextMesh>().text = "";
		mCurrent.GetComponent<TextMesh>().text = "";
		mUpgrade.GetComponent<TextMesh>().text = "";
	}
	
	public void SetBuyInfoMenu()
	{	
		Tower tower = mTowerManager.GetCurrentTower().GetComponent<Tower>();
		
		SetType();
		SetCurrent(tower);
	}
	
	public void SetUpgradeInfoMenu(Tower tower)
	{
		if (mTowerIconsList.Count > 0)
		{
			DeactivateButtons();
			
			mUpgradeMenuActive = true;
			
			mCurrentUpgradeIcon = tower.GetIcon();
			mCurrentUpgradeIcon = Instantiate(mCurrentUpgradeIcon) as GameObject;
			mCurrentUpgradeIcon.active = true;
			mCurrentUpgradeIcon.transform.position = mTowerIconsList[0].GetComponent<GUIIcon>().GetMenuPosition();
			
			mSellButton.active = true;
			
			GameObject tu = tower.GetComponent<Tower>().GetUpgradeObject();

			if (tu)
			{
				Tower towerUpgrade = tu.GetComponent<Tower>();
				
				mUpgradeButton.active = true;
				
				SetType();
				SetCurrent(tower);
				SetUpgrade(towerUpgrade);
				SetUpgradeValue(tower);
				SetSellValue(tower);
				
			}
			else
			{
				SetType();
				SetCurrent(tower);
				SetSellValue(tower);
			}
		}
	}
	
	public void DeactivateButtons()
	{
		mUpgradeMenuActive = false;
		
		mType.GetComponent<TextMesh>().text = "";
		mCurrent.GetComponent<TextMesh>().text = "";
		mUpgrade.GetComponent<TextMesh>().text = "";
		mUpgradeValue.GetComponent<TextMesh>().text = "";
		mSellValue.GetComponent<TextMesh>().text = "";
		
		if (mCurrentIcon)
			mCurrentIcon.active = false;
		if (mCurrentUpgradeIcon)
		{
			Destroy(mCurrentUpgradeIcon);
			mCurrentUpgradeIcon = null;
		}
		
		mCloseButton.active = false;
		mBuyButton.active = false;
		mUpgradeButton.active = false;
		mSellButton.active = false;
		
		for (int i = 0; i < mTowerIconsList.Count; i++)
			mTowerIconsList[i].active = false;
	}
	
	private void SetType()
	{
		mType.GetComponent<TextMesh>().text = 
					"Status: " +
					"\nRange: " +
					"\nRate: " +
					"\nDamage: ";	
	}
	
	private void SetCurrent(Tower tower)
	{
		mCurrent.GetComponent<TextMesh>().text = 
					"Current" +
					"\n" + tower.GetRange() + 
					"\n" + tower.GetFireEvery() + 
					"\n" + tower.GetDamage();
	}
	
	private void SetUpgrade(Tower tower)
	{
		mUpgrade.GetComponent<TextMesh>().text = 
					"Upgrade" +
					"\n" + tower.GetRange() + 
					"\n" + tower.GetFireEvery() + 
					"\n" + tower.GetDamage();
	}
	
	private void SetUpgradeValue(Tower tower)
	{
		mUpgradeValue.GetComponent<TextMesh>().text = 
					/*"Upgrade:" + */tower.GetUpgradeCost().ToString();
	}
	
	private void SetSellValue(Tower tower)
	{
		mSellValue.GetComponent<TextMesh>().text = 
					/*"   Sell:" +*/tower.GetSell().ToString();
	}
	
	public bool GetUpgradeMenuAcitve()
	{
		return mUpgradeMenuActive;	
	}
}

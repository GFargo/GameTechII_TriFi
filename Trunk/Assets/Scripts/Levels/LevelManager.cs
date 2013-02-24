using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	private float mCurrentHealth;
	private float mCurrentBank;
	private GameObject mStart;
	private GameObject mResume;
	private GameObject mHelp;
	private GameObject mQuit;
	private GameObject mLevelComplete;
	private float mVolume;
	
	public int levelNumber;
	public float levelHealth;
	public int bank;
	
	public GameObject startButton;
	public GameObject resumeMenu;
	public GameObject helpMenu;
	public GameObject quitMenu;
	public GameObject levelCompleteButton;

	void Start() 
	{
		mCurrentHealth = levelHealth;
		mCurrentBank = bank;
		
		mStart = GameObject.Instantiate(startButton) as GameObject;
		mResume = GameObject.Instantiate(resumeMenu) as GameObject;
		mHelp = GameObject.Instantiate(helpMenu) as GameObject;
		mQuit = GameObject.Instantiate(quitMenu) as GameObject;
		mLevelComplete = GameObject.Instantiate(levelCompleteButton) as GameObject;
		
		mStart.guiTexture.enabled = false;
		mResume.guiTexture.enabled = false;
		mHelp.guiTexture.enabled = false;
		mQuit.guiTexture.enabled = false;
		mLevelComplete.guiTexture.enabled = false;
		
		StartButton();
	}
	
	private void Update()
	{
		if (mCurrentHealth <= 0) ReLoad();
		if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) TogglePause();
		if (Input.GetKeyDown(KeyCode.R))
		{
			PlayerPrefs.SetFloat("bank", bank);
			ReLoad();
		}
		
//		if (Time.timeScale == 0 && 
//			!mStart.guiTexture.enabled &&
//			!mLevelComplete.guiTexture.enabled)
//		{
//			//mResume.guiTexture.enabled = true;
//			//mHelp.guiTexture.enabled = true;
//			//mQuit.guiTexture.enabled = true;
//		}
		if (Time.timeScale == 1)
		{
			mResume.guiTexture.enabled = false;
			mHelp.guiTexture.enabled = false;
			mQuit.guiTexture.enabled = false;
			mStart.guiTexture.enabled = false;
		}
    }
	
	public void StartButton()
	{
		Time.timeScale = 0;
		GameObject.Find("Spotlight").GetComponent<Light>().intensity = 1;
		mVolume = AudioListener.volume;
		AudioListener.volume = 0;
		mStart.guiTexture.enabled = true;
	}
	
	public void ReLoad()
	{
		Application.LoadLevel(Application.loadedLevel);
		StartButton();
	}
	
	public void LoadNext()
	{
		Time.timeScale = 0;
		GameObject.Find("Spotlight").GetComponent<Light>().intensity = 1;
		AudioListener.volume = 0;
		mLevelComplete.guiTexture.enabled = true;
		
		//if (level < Application.levelCount) Application.LoadLevel(level);
		//else Application.LoadLevel("MainMenu");
	}
	
	private void Load() { mCurrentBank = PlayerPrefs.GetFloat("bank"); }
	private void Save() { PlayerPrefs.SetFloat("bank", mCurrentBank); }
	
	public float GetBank() { return mCurrentBank; }
	public float GetStartHealth() { return levelHealth; }
	public float GetHealth() { return mCurrentHealth; }
	public int GetLevelNumber() { return levelNumber; }
	public bool GetPause() { return (Time.timeScale == 0 ? true : false); }
	
	public void SetVolume(float volume) 
	{ 
		mVolume = volume;
		AudioListener.volume = mVolume;
	}
	public float GetVolume() { return mVolume; }
	
	public void DecrementLevelHealth(float decrement) { mCurrentHealth -= decrement; }
	public void DecrementBank(float decrement) { mCurrentBank -= decrement; }
	public void IncrementBank(float increment) { mCurrentBank += (int)Mathf.Ceil(increment); }
	
	public void TogglePause ()
	{ 
		Time.timeScale = (Time.timeScale == 0 ? 1 : 0);
		
		if (Time.timeScale == 0)
		{
			GameObject.Find("Spotlight").GetComponent<Light>().intensity = 0.25f;
			mVolume = AudioListener.volume;
			AudioListener.volume = 0;
			mResume.guiTexture.enabled = true;
			mHelp.guiTexture.enabled = true;
			mQuit.guiTexture.enabled = true;
		}
		else if (Time.timeScale == 1)
		{
			GameObject.Find("Spotlight").GetComponent<Light>().intensity = 2;
			SetVolume(mVolume);
			mResume.guiTexture.enabled = false;
			mHelp.guiTexture.enabled = false;
			mQuit.guiTexture.enabled = false;
		}
	}
	
	public void SetButtonRender(bool val)
	{
		mResume.guiTexture.enabled = val;
		mHelp.guiTexture.enabled = val;
		mQuit.guiTexture.enabled = val;	
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VolumeScreen : MonoBehaviour
{
	private LevelManager mLevelManager;
	private int currentVolume;
	private int maxVolume;
	
	public List<Material> materials;
	
	void Start () 
	{
		mLevelManager = GameObject.Find("Main Camera").GetComponent<LevelManager>();
		maxVolume = materials.Count - 1;
	}
	
	void Update () 
	{
		currentVolume = (int)((mLevelManager.GetVolume() > 1 ? 1 : mLevelManager.GetVolume()) * maxVolume);
		renderer.material = materials[currentVolume];
		
		if (Input.GetKeyDown(KeyCode.Minus))
		{
			currentVolume -= 1;
			if (currentVolume < 0)
				currentVolume = 0;
			else
			{
				mLevelManager.SetVolume((float)currentVolume/(maxVolume));
				renderer.material = materials[currentVolume];
			}
		}
		if (Input.GetKeyDown(KeyCode.Equals))
		{
			currentVolume += 1;
			if (currentVolume > maxVolume)
				currentVolume = maxVolume;
			else
			{
				mLevelManager.SetVolume((float)currentVolume/(maxVolume));
				renderer.material = materials[currentVolume];
			}
		}
	}
}

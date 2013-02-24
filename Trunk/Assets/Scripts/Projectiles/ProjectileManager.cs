using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileManager : MonoBehaviour 
{
	private List<GameObject> mProjectileList;
	
	void Start () 
	{
		mProjectileList = new List<GameObject>();
	}
	
	void Update () 
	{
		
	}
	
	public void AddProjectile(GameObject projectile) 
	{ 
		mProjectileList.Add(projectile); 
	}
}

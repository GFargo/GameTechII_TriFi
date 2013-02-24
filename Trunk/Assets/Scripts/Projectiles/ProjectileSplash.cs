using UnityEngine;
using System.Collections;

public class ProjectileSplash : Projectile 
{
	public GameObject areaOfEffect;
	private float mSplashDamage;
	private bool triggered;
	
	private void OnTriggerEnter(Collider other)
	{
		GameObject aoe = null;
		aoe = Instantiate(areaOfEffect, new Vector3(other.transform.position.x, 
			areaOfEffect.transform.position.y, 
			other.transform.position.z), 
			areaOfEffect.transform.rotation) as GameObject;
		
		aoe.GetComponent<AreaOfEffectSplash>().SetSplashDamage(mSplashDamage);
		
		Destroy(gameObject);
	}
	
	public void SetSplashDamage(float damage) { mSplashDamage = damage; }
}

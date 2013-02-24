using UnityEngine;
using System.Collections;

public class TowerSplash : Tower 
{
	public float areaOfEffectRange;
	public float splashDamage;
	
	protected override void Shoot(Vector3 look, GameObject target, bool volume)
	{
		if (volume) Sound(1.0f);
		else Sound(0.1f);
		
		GameObject proj = (GameObject)Instantiate(projectile, 
			new Vector3(transform.position.x, transform.localScale.y + 1.0f, transform.position.z), mTowerUpper.transform.rotation);
		ProjectileSplash projScript = proj.GetComponent<ProjectileSplash>();
		projScript.SetRange(mRange);
		projScript.SetHitPointDamage(hitPointDamage);
		projScript.SetSplashDamage(splashDamage);
		projScript.SetTarget(target);
		projScript.SetSpeed(speed);

		proj.rigidbody.AddForce(mTowerUpper.transform.forward * speed * mTempoManager.GetBeatsPerMinuteDivided());
	}
	
	public override float GetDamage() { return splashDamage; }
}
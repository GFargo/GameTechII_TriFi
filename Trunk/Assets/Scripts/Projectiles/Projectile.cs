using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	protected ProjectileManager mProjectileManager;
	
	private Vector3 mOriginalPosition;
	protected float mRange;
	protected float mHitPointDamage;
	protected GameObject mTarget;
	protected float mSpeed;
	
	private void Start() 
	{
		mProjectileManager = GameObject.Find("Main Camera").GetComponent<ProjectileManager>();
		mOriginalPosition = gameObject.transform.position;
	}
	
	private void FixedUpdate() 
	{		
		//print(Vector3.Distance(mOriginalPosition, gameObject.transform.position));
		//print(mRange);
		
		if (Vector3.Distance(mOriginalPosition, gameObject.transform.position) >= mRange)
			Destroy(gameObject);
		
		if (mTarget)
		{
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			
			Vector3 direction = mTarget.transform.position - gameObject.transform.position;
			direction.y = 0;//gameObject.transform.position.y;
			
			direction.Normalize();
			rigidbody.AddForce(direction * mSpeed);
		}
	}
	
	public float GetHitPointDamage() { return mHitPointDamage; }
	
	public void SetOriginalPosition(Vector3 originalPosition) { mOriginalPosition = originalPosition; }
	public void SetRange(float range) { mRange = range; }
	public void SetHitPointDamage(float hitPointDamage) { mHitPointDamage = hitPointDamage; }
	public void SetTarget(GameObject target) { mTarget = target; }
	public void SetSpeed(float speed) { mSpeed = speed; }
	
	public virtual void OnCollisionEnter(Collision collision) { Destroy(gameObject); }
}

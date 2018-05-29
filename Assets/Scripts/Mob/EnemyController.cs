using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* Controls the Enemy AI */

public class EnemyController : MonoBehaviour {

	public Animator animator;

	public float lookRadius = 10f;	// Detection range for player

	public Transform target;	// Reference to the player
	NavMeshAgent agent; // Reference to the NavMeshAgent


	public bool isRange;
	float attackSpeed = 2f;
	float currAttackSpeed = 0;
	bool isAttack = false;

	void Start () {
		agent = GetComponent<NavMeshAgent>();        
	}
	
	void Update () {
		if (!target) {
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		}
		// Distance to the target
		float distance = Vector3.Distance (target.position, transform.position);

		currAttackSpeed -= Time.deltaTime;	

		if (Vector3.Distance (this.gameObject.transform.position, target.gameObject.transform.position) <= agent.stoppingDistance) {
			isRange = true;
			Debug.Log (Vector3.Distance (this.gameObject.transform.position, target.gameObject.transform.position));
		} else {
			isRange = false;
			StopCoroutine (Attack ());
		}
		if (target) {	
			// If inside the lookRadius
			if (distance <= lookRadius) {
				// Move towards the target
				agent.SetDestination (target.position);

				// If within attacking distance
				if (distance <= agent.stoppingDistance && target) {
					FaceTarget ();   // Make sure to face towards the target

					if (isRange) {							
						if (currAttackSpeed <= 0) {
							StartCoroutine (Attack ());
							currAttackSpeed = attackSpeed;
						}
					}
				}
			}
		}
		if (isRange && distance <= agent.stoppingDistance && target && isAttack) {
			animator.SetInteger ("State", 2);
		} else if (distance <= lookRadius && distance > agent.stoppingDistance && target) {
			animator.SetInteger ("State", 1);
		} else if (!isRange) { 
			animator.SetInteger ("State", 0);
		} else if (distance <= lookRadius && distance < agent.stoppingDistance && target) {
			animator.SetInteger ("State", 0);
		} 
	}

	// Rotate to face the target
	void FaceTarget ()
	{
		Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);	
	}

	// Show the lookRadius in editor
	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}    

	IEnumerator Attack()
	{			
		isAttack = true;
		AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
		yield return new WaitForSeconds (1f);
		target.gameObject.GetComponent<PlayerStats> ().Hurt (this.gameObject.GetComponent<CharacterStats> ().damage);
		print ("Hit from mob");
		isAttack = false;
	}
}

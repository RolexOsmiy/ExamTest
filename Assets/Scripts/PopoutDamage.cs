using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopoutDamage : MonoBehaviour {

	public Animator animator;
	public Text damageText;

	void Update () 
	{
		AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
		Destroy(transform.parent.gameObject, clipInfo[0].clip.length);
		damageText = animator.GetComponent<Text>();
	}
}

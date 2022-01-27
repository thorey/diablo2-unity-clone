using UnityEngine;
using System.Collections;

public class SpecialAttack : MonoBehaviour {

	public Fighter player;
	public KeyCode key;
	public double damagePercentage;
	public int stunTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (key)) {
			//player.resetAttackFunction ();
			//player.specialAttack = true;
			//player.attackFunction (stunTime, damagePercentage, key);
		}
	}
}

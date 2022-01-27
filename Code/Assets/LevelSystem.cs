using UnityEngine;
using System.Collections;

public class LevelSystem : MonoBehaviour {

	public int level;
	public int exp;
	public int maxExp;
	public Fighter player;
	public float expPercentage;
	public AudioSource levelUp;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		maxExp = (int)(Mathf.Pow (level, 2) + 100);
		LevelUp ();
		expPercentage = (float)exp / (float)maxExp;
	}

	void LevelUp()
	{
		if (exp >= maxExp) {
			exp = exp - (int)(Mathf.Pow (level, 2) + 100);
			level = level + 1;
			levelUp.Play ();
			LevelEffect();
		}
	}

	void LevelEffect()
	{
		player.maxHealth = player.maxHealth + (int)Mathf.Pow (level, 3);
		player.maxMana = player.maxMana + (int)Mathf.Pow (level, 2);
		player.damage = player.damage + (int)Mathf.Pow (level, 1) + 5;
		player.health = player.maxHealth;
		player.mana = player.maxMana;

		GameObject.FindGameObjectWithTag ("magicAttack").GetComponent<Strike> ().damage = GameObject.FindGameObjectWithTag ("magicAttack").GetComponent<Strike> ().damage + (int)Mathf.Pow (level, 2);
		//Debug.Log (GameObject.FindGameObjectWithTag ("magicAttack").GetComponent<Strike> ().damage);
		GameObject[] enemies;
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies) {
			if (enemy.GetComponent<Mob> ().health > 0) {
				enemy.GetComponent<Mob> ().maxHealth = enemy.GetComponent<Mob> ().maxHealth + (int)Mathf.Pow (level, 3);
				enemy.GetComponent<Mob> ().health = enemy.GetComponent<Mob> ().maxHealth;
				enemy.GetComponent<Mob> ().damage = enemy.GetComponent<Mob> ().damage + (int)Mathf.Pow (level, 1);
			}
		}

	}
}

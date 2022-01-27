using UnityEngine;
using System.Collections;

public class Fighter : MonoBehaviour {

	public GameObject opponent;
	Animator anim;
	public int damage;
	public double impactTime;
	public int range;
	public int maxHealth;
	public int health;
	public int maxMana;
	public int mana;
	public int spellCost;
	private bool dead = false;
	public float combatEscapeTime;
	public float countDown;
	private bool attacked = false;
	//public bool specialAttack = false;
	private Object specialAttack;
	private Object magicAttack;
	public double damagePercentage;
	public LevelSystem playerLevel;
	public int stunTime;
	KeyCode lastInput;
	public AudioSource hit1;
	public AudioSource hit2;
	public AudioSource attack1;
	public AudioSource attack2;
	public AudioSource damage1;
	public AudioSource damage2;
	public AudioSource death;
	public AudioSource footstep1;
	public AudioSource footstep2;
	public AudioSource footstep3;
	private bool firstStep = false;
	private bool secondStep = false;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		health = maxHealth;
		mana = maxMana;
		InvokeRepeating ("regenHealth", 0f, 1f);
		InvokeRepeating ("regenMana", 0f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		if(!isDead()){

			if (Input.GetKeyUp (KeyCode.Mouse0)) { //normal attack
				attackFunction (KeyCode.Mouse0);
				lastInput = KeyCode.Mouse0;
			} else if(Input.GetKeyUp (KeyCode.Alpha1)){ //special attack
				attackFunction (KeyCode.Alpha1);
				lastInput = KeyCode.Alpha1;
			} else if(Input.GetKeyUp (KeyCode.Alpha2) && (mana - spellCost) >= 0){ //special attack
				magicAttackFunction (KeyCode.Alpha2);
				lastInput = KeyCode.Alpha2;
			}

			if (lastInput == KeyCode.Alpha1) //special attack?
				checkForHit (5, 2.0); //stun for 5 seconds and double damage
			else
				checkForHit (0, 1.0);

			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Run")) {
				//Debug.Log (anim.GetCurrentAnimatorStateInfo (0).normalizedTime);
				if (anim.GetCurrentAnimatorStateInfo (0).normalizedTime%1.0f < .5 && !firstStep) {
					if(Random.Range(0f, 1f) < 0.33)
						footstep1.Play ();
					else if(Random.Range(0f, 1f) < 0.66)
						footstep2.Play ();
					else
						footstep3.Play();
					firstStep = true;
					secondStep = false;
				}
				else if (anim.GetCurrentAnimatorStateInfo (0).normalizedTime%1.0f > .5 && !secondStep) {
					if(Random.Range(0f, 1f) < 0.33)
						footstep1.Play ();
					else if(Random.Range(0f, 1f) < 0.66)
						footstep2.Play ();
					else
						footstep3.Play();
					secondStep = true;
					firstStep = false;
				}

			}

		}
		else{
			if (!dead) {
				anim.SetTrigger ("dead");
				death.Play ();
				dieMethod ();
			}
			else
				anim.SetBool ("restart", false);
		}
	}

	public void attackFunction(KeyCode key)
	{
		if (Input.GetKeyUp (key) && inRange()) {
			if (opponent != null) {
				transform.LookAt (opponent.transform.position);
				anim.SetTrigger ("attackEnemy");

			}
		} 
		else {
		}

		//impact ();
	}

	public void magicAttackFunction(KeyCode key)
	{
		if (Input.GetKeyUp (key)) {
			spendMana (spellCost);
			transform.LookAt (ClickToMove.cursorPosition);
			Quaternion rot = transform.rotation;
			rot.x = 0;
			rot.z = 0;
			magicAttack = Instantiate (Resources.Load ("magicAttack"), new Vector3 (transform.position.x, transform.position.y + 2.0f, transform.position.z), rot);
			Destroy (magicAttack, 10f);
		} 
		else {
		}

		//impact ();
	}

	public void checkForHit(int stunSeconds, double scaledDamage)
	{
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
			//Debug.Log (anim.GetCurrentAnimatorStateInfo (0).normalizedTime);
			if ((anim.GetCurrentAnimatorStateInfo (0).normalizedTime > .5 && !attacked) && inRange ()) {
				//animation finished and in range
				if(Random.Range(0f, 1f) < 0.5)
					hit1.Play ();
				else
					hit2.Play ();
				if(Random.Range(0f, 1f) < 0.3)
					attack1.Play ();
				else if(Random.Range(0f, 1f) < 0.5)
					attack2.Play ();
				countDown = combatEscapeTime;
				CancelInvoke ("combatEscapeCountDown");
				InvokeRepeating ("combatEscapeCountDown", 0, 1);
				opponent.GetComponent<Mob> ().getHit (damage*scaledDamage);
				opponent.GetComponent<Mob> ().getStunned (stunSeconds);
				if (lastInput == KeyCode.Alpha1) {
					specialAttack = Instantiate (Resources.Load ("specialAttack"), new Vector3 (opponent.transform.position.x, opponent.transform.position.y + 2.0f, opponent.transform.position.z), Quaternion.identity);
					Destroy (specialAttack, 2f);
				}
				attacked = true;
			} else {
				//attacked = false;
			}
		} else {
			attacked = false;
		}
	}

	public void resetAttackFunction()
	{
		
	}

	/*void OnTriggerExit(Collider other)
	{
		if (opponent != null) {
			countDown = combatEscapeTime;
			InvokeRepeating ("combatEscapeCountDown", 0, 1);
			opponent.GetComponent<Mob> ().getHit (damage);
		}
	}*/

	void combatEscapeCountDown()
	{
		countDown = countDown - 1;
		if (countDown == 0) {
			CancelInvoke ("combatEscapeCountDown");
		}
	}

	public void getHit( int damage)
	{
		if (health > 0 && !isDead ()) {
			if(Random.Range(0f, 1f) < 0.5)
				damage1.Play ();
			else
				damage2.Play ();
			health = health - damage;
			CancelInvoke ("combatEscapeCountDown");
			InvokeRepeating ("combatEscapeCountDown", 0, 1);
		}
		//Debug.Log (health);
	}

	public void spendMana( int cost)
	{
		mana = mana - cost;
	}

	public bool isDead(){
		if (health <= 0) {
			health = 0;
			return true;
		}
		else
			return false;
	}

	void dieMethod()
	{
		StartCoroutine (deathWait ());

	}

	public IEnumerator deathWait()
	{

		dead = true;
		yield return new WaitForSeconds (5f);
		anim.SetBool ("restart", true);
		health = maxHealth;
		dead = false;
	}

	void regenHealth()
	{
		if (!dead) {
			if((health < maxHealth && health + 5 <=maxHealth) && countDown == 0)
				health = health + 5;
			else if (health < maxHealth && health + 2 <= maxHealth)
				health = health + 2;
			else if(health < maxHealth)
				health = health + 1;
		}
		//Debug.Log ("health = " +health);
	}

	void regenMana()
	{
		if (!dead) {
			if((mana< maxMana && mana + 5 <=maxMana) && countDown == 0)
				mana = mana + 5;
			else if (mana < maxMana && mana + 2 <= maxMana)
				mana = mana + 2;
			else if(mana < maxMana)
				mana = mana + 1;
		}
		//Debug.Log ("mana = "+mana);
	}


	bool inRange(){
		if (opponent != null && Vector3.Distance (transform.position, opponent.transform.position) < range)
			return true;
		else
			return false;
	}
}

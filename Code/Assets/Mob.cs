using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour {

	public float speed;
	public float range;
	public float detectionRange;
	public CharacterController controller;
	public Fighter hero;
	public LevelSystem playerLevel;
	public Transform player;
	private Animator anim;
	public int health;
	public int maxHealth;
	public int damage;
	private bool dead = false;
	private bool attacked = false;
	public int stunTime;
	public AudioSource attack1;
	public AudioSource attack2;
	public AudioSource attack3;
	public AudioSource pain1;
	public AudioSource pain2;
	public AudioSource pain3;
	public AudioSource death;
	public AudioSource roar1;
	public AudioSource roar2;
	bool patrolling = false;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		hero = player.GetComponent<Fighter> ();
		health = maxHealth;
		InvokeRepeating ("playRoar", Random.Range (5f, 200f), Random.Range (60f, 120f));
	}
	
	// Update is called once per frame
	void Update () {

		if (!isDead ()) {
			//Debug.Log (stunTime);
			if (stunTime <= 0) {
				if (!inRangeDetect () && !patrolling) {
					patrol ();
				} else if (inRangeDetect () && patrolling) {
						stopPatrol ();
						chase ();
					} else if (!inRangeAttack () && !patrolling) {
						chase ();
					} else if(inRangeAttack()){
						anim.SetBool ("moving", false);
						if (!hero.isDead ()) {
							anim.SetTrigger ("attackEnemy");
							if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
								//Debug.Log (anim.GetCurrentAnimatorStateInfo (0).normalizedTime);
								if ((anim.GetCurrentAnimatorStateInfo (0).normalizedTime > .5 && !attacked) && inRangeAttack ()) {
									//animation finished and in range
									hero.getHit (damage);
									if (Random.Range (0f, 1f) < 0.33)
										attack1.Play ();
									else if (Random.Range (0f, 1f) < 0.66)
										attack2.Play ();
									else
										attack3.Play ();
									attacked = true;
								} else {
									//attacked = false;
								}
							} else {
								attacked = false;
							}
						}
				}
			}
		} 
		else {
			anim.SetTrigger ("dead");
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Death")) {
				//Debug.Log (anim.GetCurrentAnimatorStateInfo (0).normalizedTime);
				if (anim.GetCurrentAnimatorStateInfo (0).normalizedTime > .5 && !dead) {
					//animation finished and in range
					death.Play();
					dieMethod();
					dead = true;
				} else {
					//attacked = false;
				}
			} else {
				dead = false;
			}
		}
	}

	bool inRangeAttack(){
		if (Vector3.Distance (transform.position, player.position) < range)
			return true;
		else
			return false;
	}

	bool inRangeDetect(){
		if (Vector3.Distance (transform.position, player.position) < detectionRange)
			return true;
		else
			return false;
	}

	bool isDead(){
		if (health <= 0) {
			health = 0;
			return true;
		}
		else
			return false;
	}

	void dieMethod()
	{
		playerLevel.exp = playerLevel.exp + (25 + (int)(Mathf.Pow (playerLevel.level, 2)));
		Destroy (gameObject, 5f);
	}

	public void getStunned(int seconds)
	{
		CancelInvoke ("stunCountDown");
		stunTime = stunTime + seconds;
		InvokeRepeating ("stunCountDown", 0f, 1f);
	}

	void stunCountDown()
	{
		stunTime = stunTime - 1;
		if (stunTime == 0)
			CancelInvoke ("stunCountDown");
	}

	void playRoar()
	{
		if(Random.Range(0f, 1f) < 0.5)
			roar1.Play ();
		else
			roar2.Play ();
	}

	void chase()
	{
		Vector3 lookAt = new Vector3 ( 0, controller.height, 0);
		lookAt = lookAt + player.position;
		transform.LookAt (lookAt);
		controller.SimpleMove (transform.forward * speed);
		anim.SetBool("moving", true);
	}

	void patrol()
	{
		patrolling = true;
		InvokeRepeating ("rotate", 0f, Random.Range (5f, 10f));
		InvokeRepeating ("walk", 0f, .1f);
		InvokeRepeating ("wait", Random.Range (5f, 10f), Random.Range (2f, 5f));
	}

	void rotate()
	{
		transform.Rotate(new Vector3(0f,Random.Range(30f, 180f), 0f));
	}

	void walk()
	{
		controller.SimpleMove (transform.forward * speed);
		anim.SetBool("moving", true);
	}

	void wait()
	{
		anim.SetBool("moving", false);
		patrolWait ();
	}

	public IEnumerator patrolWait()
	{

		yield return new WaitForSeconds (Random.Range (5f, 10f));
	}

	void stopPatrol()
	{
		patrolling = false;
		anim.SetBool("moving", false);
		CancelInvoke ("walk");
		CancelInvoke ("rotate");
		CancelInvoke ("wait");
	}

	void OnMouseOver()
	{
		player.GetComponent<Fighter>().opponent = gameObject;
	}

	public void getHit( double damage)
	{
		if (health > 0) {
			if(Random.Range(0f, 1f) < 0.33)
				pain1.Play ();
			else if(Random.Range(0f, 1f) < 0.66)
				pain2.Play ();
			else
				pain3.Play ();
			health = health - (int)damage;
		}
		//Debug.Log (health);
	}

}

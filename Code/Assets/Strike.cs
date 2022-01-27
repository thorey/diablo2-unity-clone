using UnityEngine;
using System.Collections;

public class Strike : MonoBehaviour {

	public float speed;
	public float damage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
			other.GetComponent<Mob> ().getHit (damage);
	}
}

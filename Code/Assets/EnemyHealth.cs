using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public Fighter player;
	public Mob target;
	public float healthPercentage;
	public Texture2D frame;
	public Rect framePosition;
	public Texture2D healthBar;
	public Rect healthBarPosition;
	float horizontalDistance = 0.099f;
	float verticalDistance = 0.29f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (player.opponent != null) {
			target = player.opponent.GetComponent<Mob> ();
			healthPercentage = (float)target.health / (float)target.maxHealth;
		} else {
			target = null;
			healthPercentage = 0;
		}
	}

	void OnGUI()
	{
		if (target != null&&player.countDown>0) {
			drawFrame ();
			drawHealth ();
		}

	}

	void drawFrame()
	{
		framePosition.width = 400;
		framePosition.height = 30;
		framePosition.x = (Screen.width - framePosition.width) / 2;
		framePosition.y = 20;

		GUI.DrawTexture (framePosition, frame);
	}

	void drawHealth()
	{
		healthBarPosition.width = (framePosition.width-80) * healthPercentage;
		healthBarPosition.height = framePosition.height-18;
		healthBarPosition.x = framePosition.x + (framePosition.width * horizontalDistance);
		healthBarPosition.y = framePosition.y + (framePosition.height * verticalDistance);

		GUI.DrawTexture (healthBarPosition, healthBar);
	}
}

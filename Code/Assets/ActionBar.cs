using UnityEngine;
using System.Collections;

public class ActionBar : MonoBehaviour {

	public Texture2D actionBar;
	public Rect position;
	public SkillSlot[] skill;
	public Texture2D skill1;
	public Texture2D skill2;
	public Texture2D background;
	public Rect backgroundPos;
	public Texture2D expBar;
	public Rect expBarPos;
	public Texture2D healthOrb;
	public Rect healthOrbPos;
	public Texture2D healthOrbBackground;
	public Texture2D manaOrb;
	public Rect manaOrbPos;
	public Texture2D manaOrbBackground;
	public float skillX;
	public float skillY;
	public float skillWidth;
	public float skillHeight;
	public float skillDistance;
	float healthClipFactor;
	float manaClipFactor;
	// Use this for initialization
	void Start () {
		init ();
	}
	
	// Update is called once per frame
	void Update () {
		updateSkillSlots ();
		updateExpBar ();
		// 0 = full health/mana
		healthClipFactor = Mathf.Ceil(((1 - getPlayerHealthPercentage ()) * (healthOrbPos.y * Screen.height * healthOrbPos.height)));
		manaClipFactor = Mathf.Ceil(((1 - getPlayerManaPercentage ()) * (manaOrbPos.y * Screen.height * manaOrbPos.height)));
	}

	void init()
	{
		skill = new SkillSlot[2];
		skill [0] = new SkillSlot ();
		skill [1] = new SkillSlot ();
		skill [0].key = KeyCode.Alpha1;
		skill [0].picture = skill1;
		skill [1].key = KeyCode.Alpha2;
		skill [1].picture = skill2;
	}

	void OnGUI()
	{
		drawBackground ();
		drawExpBar ();
		drawHealthOrb ();
		drawManaOrb ();
		drawActionBar ();
		drawSkillSlots ();
	}

	void drawActionBar()
	{
		GUI.DrawTexture (getScreenRect(position), actionBar);
	}

	void drawSkillSlots()
	{
		GUI.DrawTexture (getScreenRect(skill[0].position), skill[0].picture);
		GUI.DrawTexture (getScreenRect(skill[1].position), skill[1].picture);

	}

	void drawBackground()
	{
		GUI.DrawTexture (getScreenRect (backgroundPos), background);
	}

	void drawExpBar()
	{
		GUI.DrawTexture (getScreenRect (expBarPos), expBar);
	}

	void drawHealthOrb()
	{
		//Debug.Log (healthClipFactor);
		GUI.BeginGroup (getScreenRect(healthOrbPos));
		GUI.DrawTexture (new Rect (0f, 0f, Screen.width * healthOrbPos.width, Screen.height * healthOrbPos.height), healthOrbBackground);
		GUI.BeginGroup (new Rect (0f, healthClipFactor, Screen.width * healthOrbPos.width, Screen.height * healthOrbPos.height));
			GUI.DrawTexture(new Rect (0f, -healthClipFactor, Screen.width * healthOrbPos.width, Screen.height * healthOrbPos.height), healthOrb);
			GUI.EndGroup();
		GUI.EndGroup();
	}

	void drawManaOrb()
	{
		GUI.BeginGroup (getScreenRect(manaOrbPos));
		GUI.DrawTexture (new Rect (0f, 0f, Screen.width * manaOrbPos.width, Screen.height * manaOrbPos.height), manaOrbBackground);
		GUI.BeginGroup (new Rect (0f, manaClipFactor, Screen.width * manaOrbPos.width, Screen.height * manaOrbPos.height));
			GUI.DrawTexture(new Rect (0f, -manaClipFactor, Screen.width * manaOrbPos.width, Screen.height * manaOrbPos.height), manaOrb);
			GUI.EndGroup();
		GUI.EndGroup();
	}

	void updateSkillSlots()
	{
		skill [0].position.Set (skillX + 0 * (skillWidth + skillDistance), skillY, skillWidth, skillHeight);
		skill [1].position.Set (skillX + 1 * (skillWidth + skillDistance), skillY, skillWidth, skillHeight);
	}

	void updateExpBar()
	{
		expBarPos.width = 0.36f * GameObject.FindGameObjectWithTag ("Player").GetComponent<Fighter> ().playerLevel.expPercentage; //max exp bar width * experience percentage
	}

	Rect getScreenRect(Rect position)
	{
		return new Rect (Screen.width * position.x, Screen.height * position.y, Screen.width * position.width, Screen.height * position.height);

	}

	float getPlayerHealthPercentage()
	{
		float health = GameObject.FindGameObjectWithTag ("Player").GetComponent<Fighter> ().health;
		float maxHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<Fighter> ().maxHealth;
		return health / maxHealth;

	}

	float getPlayerManaPercentage()
	{
		float mana = GameObject.FindGameObjectWithTag ("Player").GetComponent<Fighter> ().mana;
		float maxMana = GameObject.FindGameObjectWithTag ("Player").GetComponent<Fighter> ().maxMana;
		return mana / maxMana;

	}
		
}

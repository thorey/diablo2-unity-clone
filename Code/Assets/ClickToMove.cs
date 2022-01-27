using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour {

    public float speed;
    public CharacterController controller;
    private Vector3 position;
    Animator anim;
	public static Vector3 cursorPosition;
	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
        position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		locateCursor ();
        if(Input.GetMouseButton(0))
        {
            //locate where the player clicked on the terrian
            locatePosition();
        }
        //move player to the position
		if(!GetComponent<Fighter>().isDead())
        moveToPosition();
	}

    void locatePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, 1000))
        {
			if(hit.collider.tag != "Player" && hit.collider.tag != "Enemy")
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }
    }

	void locateCursor()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if(Physics.Raycast(ray, out hit, 1000))
		{
			cursorPosition = hit.point;
		}
	}

    void moveToPosition()
    {
        //Game Object is moving
		if (Vector3.Distance(transform.position, position) > 3)
        {
            Quaternion newRotation = Quaternion.LookRotation(position - transform.position);
            newRotation.x = 0.0f;
            newRotation.z = 0.0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);
            controller.SimpleMove(transform.forward * speed);
            anim.SetBool("moving", true);
        }
        //Game Object is not moving
        else
        {
            anim.SetBool("moving", false);
        }
    }
}

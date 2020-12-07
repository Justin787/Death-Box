using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStuff : MonoBehaviour
{

	GameObject player; //reference to player object
	public float speed = 12;//speed of projectile
	bool facingRight = true;//whether we shoot to the right

	// Use this for initialization
	void Start()
	{
		//set up reference to player
		player = GameObject.Find("PLAYER_0");


		//if the player is not flipped, the projectile faces right
		facingRight = !player.GetComponent<playerStuff>().isFlipped;

		//if not facing right flip the image horizontally
		if (!facingRight)
		{
			transform.localScale =
				new Vector3(-transform.localScale.x, transform.localScale.y);
		}
	}

	// Update is called once per frame
	void Update()
	{
		//move continuously in the right direction
		if (facingRight)
		{
			transform.position += new Vector3(2 * (speed * Time.deltaTime), 0);
		}
		else
		{
			transform.position -= new Vector3(2 * (speed * Time.deltaTime), 0);
		}

		//if the projectile is off-screen, delete self
		//determine the viewport position of the object 0 = left side, 1 = right side
		Vector2 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
		if (viewportPos.x < 0 || viewportPos.x > 1)
		{
			Destroy(gameObject);
		}
	}
}

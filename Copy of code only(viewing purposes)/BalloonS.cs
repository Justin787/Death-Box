using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonS : MonoBehaviour
{

	GameObject player; //reference to player object
	bool facingRight = true;//whether we shoot to the right
	public Transform[] waypoints;
	int cur = 0;
	public float speed = .075f;
	float xPos;
	float xscale;
	float yscale;

	// Use this for initialization
	void Start()
	{
		//set up reference to player
		player = GameObject.Find("PLAYER_0");

		player = GameObject.Find("PLAYER_0");
		//enemy = GameObject.Find("glowstickCrab_2");

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

		//update current x position in separate int
		xPos = transform.position.x;

		// Waypoint not reached yet? then move closer
		if (transform.position != waypoints[cur].position)
		{
			Vector2 p = Vector2.MoveTowards(transform.position,
							waypoints[cur].position, speed);
			GetComponent<Rigidbody2D>().MovePosition(p);
		}
		// Waypoint reached, select next one
		else cur = (cur + 1) % waypoints.Length;

		// Animation
		Vector2 dir = waypoints[cur].position - transform.position;
		GetComponent<Animator>().SetFloat("DirX", dir.x);
		GetComponent<Animator>().SetFloat("DirY", dir.y);

		//if the projectile is off-screen, delete self
		//determine the viewport position of the object 0 = left side, 1 = right side
		Vector2 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
		if (viewportPos.x < 0 || viewportPos.x > 1)
		{
			Destroy(gameObject);
		}
	}
}

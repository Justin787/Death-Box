using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eye : MonoBehaviour
{
	public bool isFlipped = false;//whether the enemy is flipped horizontally
	public Transform[] waypoints;
	int cur = 0;
	public static float speed = .075f;
	float xPos;
	float xscale;
	float yscale;
	GameObject player;

	// GameObject enemy;

	public static int health = 1; //health points of enemy
	public static int hits = 1; //health taken per hit

	void Start()
	{
		xscale = transform.localScale.x;//store the player's starting size
		yscale = transform.localScale.y;//^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

		player = GameObject.Find("PLAYER_0");
		//enemy = GameObject.Find("glowstickCrab_2");

	}


	void FixedUpdate()
	{


		//update current x position in separate int
		xPos = transform.position.x;

		// Waypoint not reached yet? then move closer
		if (transform.position != player.transform.position)
		{
			Vector2 p = Vector2.MoveTowards(transform.position,
							player.transform.position, speed);
			GetComponent<Rigidbody2D>().MovePosition(p);
		}
		// Waypoint reached, select next one
		else cur = (cur + 1) % waypoints.Length;

		// Animation
		Vector2 dir = waypoints[cur].position - transform.position;
		GetComponent<Animator>().SetFloat("DirX", dir.x);
		GetComponent<Animator>().SetFloat("DirY", dir.y);

		enemyFlip();

		if (isFlipped)
		{
			transform.localScale = new Vector3(-xscale, yscale);
		}

		else
		{
			transform.localScale = new Vector3(xscale, yscale);
		}
	}

	void enemyFlip()
	{
		//if last saved position is to the left of current
		if (xPos <= player.transform.position.x)
		{
			isFlipped = false;
	
		}

		//player is to the left of the enemy
		else
		{
			isFlipped = true;
	
		}
	}

	void OnTriggerEnter2D(Collider2D co)
	{
		if (co.name == "projectile_0(Clone)")
		{
			if (health > 0)
			{
				health = health - hits;
			}

			else
			{
				Destroy(gameObject);

				SpawnScript.score += 5;
				SpawnScript.enemyPoints += 5;
			}

			Destroy(co.gameObject);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//for text objects
using UnityEngine.SceneManagement; // for scenes


public class playerStuff : MonoBehaviour
{
	Rigidbody2D rb2d; //reference to the rigidbody 2d component
	public float speed = 5f;//S P E E D
	Animator anim; //reference to the animator component
	public bool isFlipped = false;//whether the player is flipped horizontally
	float xscale, yscale;// keep track of original player size
	public GameObject projectilePrefab; //reference to the projectile prefab
	public GameObject projectilePrefab2;

	public GameObject Heart1;
	public GameObject Heart2;
	public GameObject Heart3;

	public int balloons;
	public static int lives;

	// Use this for initialization
	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		xscale = transform.localScale.x;//store the player's starting size
		yscale = transform.localScale.y;//^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
		lives = 3;
	}

	// Update is called once per frame
	void Update()
	{
		

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Balloon");
		balloons = enemies.Length;

		//left/right movement
		//if the player holds A, move left
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			RaycastHit2D hit = Physics2D.Linecast(transform.position - new Vector3(.45f, 0), transform.position);

			if((hit.collider == GetComponent<Collider2D>()))
			transform.position -= new Vector3(speed * Time.deltaTime, 0);

			//if we are not flipped, flip
			if (!isFlipped)
			{
				isFlipped = true;
				transform.localScale = new Vector3(-xscale, yscale);
			}
		}
		//if the player holds D, move right
		else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			RaycastHit2D hit = Physics2D.Linecast(transform.position - new Vector3(-.45f, 0), transform.position);

			if ((hit.collider == GetComponent<Collider2D>()))
			transform.position += new Vector3(speed * Time.deltaTime, 0);

				
			

			if (isFlipped)
			{
					isFlipped = false;
					transform.localScale = new Vector3(xscale, yscale);
			}
		}

		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			RaycastHit2D hit = Physics2D.Linecast(transform.position - new Vector3(0, -.45f), transform.position);

			if ((hit.collider == GetComponent<Collider2D>()))
				transform.position += new Vector3(0, speed * Time.deltaTime);
		}

		else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
			RaycastHit2D hit = Physics2D.Linecast(transform.position - new Vector3(0, .45f), transform.position);

			if ((hit.collider == GetComponent<Collider2D>()))
				transform.position -= new Vector3(0, speed * Time.deltaTime);
		}


		if (Input.GetKeyDown(KeyCode.R))
		{
			//reload the scene
			SceneManager.LoadScene("idk");
		}

		//if the user left clicks the mouse, create a projectile
		if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
		{
			//create a projectile at our position, with standard rotation
			Instantiate(projectilePrefab, transform.position, Quaternion.identity);
		}

		/*
		//if the user left clicks the mouse, create a projectile
		if (Input.GetButtonDown("Fire2") && balloons < 3)
		{
			//create a projectile at our position, with standard rotation
			Instantiate(projectilePrefab2, transform.position, Quaternion.identity);
		}
		*/

		//if player runs out of lives, they die
	
	    if (lives == 0)
		{
			print("die");
			Destroy(gameObject);
			Destroy(Heart1.gameObject);
		}
		else if (lives <= 1)
		{
			Destroy(Heart2.gameObject);
		}
		else if (lives <= 2)
		{
			Destroy(Heart3.gameObject);
		}
		print(lives);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.name == "Flying Eye_0(Clone)")
		{
			Destroy(other.gameObject);
			print("hit");
			lives--;
			print(lives);
		}

		if (other.tag == "Respawn")//if we hit the reset zone
		{
			//reload the scene
			SceneManager.LoadScene("idk");
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // for scenes


public class SpawnScript : MonoBehaviour
{
	//Spawn Areas
	public GameObject spawnAreaBL;
	public GameObject spawnAreaBR;
	public GameObject spawnAreaTR;

	//Text
	public Text winText;
	public Text enemiesText;
	public Text roundText;
	public Text scoreText;
	public Text sScoreText;
	public Text deathIns;

	//Enemies
	public GameObject flyingEye;

	//starting values
	bool enemiesAlive;
	public int numberOfEnemies;
	bool waveActive;
	int enemiesFEye = 5;
	int enemiesLeft = 5;
	int wave = 1;
	int enemiesOnScreen = 1;
	int et;
	public float intervals = 6f;
	float halfI1;
	float halfI2;
	bool dead;
	public static int score;

	//scoring (specified)
	public static int timePoints;
	public static int wavePoints;
	public static int enemyPoints;

	// Use this for initialization
	void Start()
	{
		deathIns.text = "";
		sScoreText.text = "";
        wavePoints = 0;
        timePoints = 0;
        enemyPoints = 0;
		score = 0;
		scoreText.text = "Score: " + score;
		roundText.text = "";
		enemiesText.text = "";
		winText.text = "";
		dead = false;
		waveActive = false;

		StartCoroutine(Waves());
		StartCoroutine(scoreTime());
	}

	// Update is called once per frame
	void Update()
	{
		GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
		if (player.Length == 0)
		{
			dead = true;
			StartCoroutine(Dead());
		}

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		numberOfEnemies = enemies.Length;

		if (waveActive)
		{
			et = numberOfEnemies + enemiesLeft;
			enemiesText.text = "Enemies Left: " + et;
		}

		else
		{
			enemiesText.text = "";
		}

		halfI1 = intervals / 2f;
		halfI2 = halfI1;

		if(waveActive)
		{
			roundText.text = "Wave: " + wave;
		}

		else if (waveActive == false)
		{
			roundText.text = "";
		}

		if (dead == false)
		scoreText.text = "Score: " + score;
	}

	IEnumerator Waves()
	{
		while (true)
		{
			//most values are already set for first wave
			waveActive = true;

			while (waveActive)
			{
				yield return new WaitForSeconds(1 / 10);

				while (enemiesLeft > 0)
				{

					yield return new WaitForSeconds(1/10);//praying that this prevents unity from crashing:(


					while (numberOfEnemies < enemiesOnScreen && enemiesLeft != 0)
					{
						yield return new WaitForSeconds(halfI1);
						winText.text = "";
						yield return new WaitForSeconds(halfI2);

						//Random number 1-4
						int rand = Random.Range(1, 5);

						//Randomizes spawn locations
						if (rand == 1)
						{
							//create a projectile at our position, with standard rotation
							Instantiate(flyingEye, transform.position, Quaternion.identity);
							enemiesLeft--;

							if (enemiesLeft != 0 && wave > 3)
							{
								Instantiate(flyingEye, spawnAreaBR.transform.position, Quaternion.identity);
								enemiesLeft--;
							}

							if (enemiesLeft != 0 && wave > 8)
							{
								Instantiate(flyingEye, spawnAreaBL.transform.position, Quaternion.identity);
								enemiesLeft--;
							}
						}
						else if (rand == 2)
						{
							Instantiate(flyingEye, spawnAreaBL.transform.position, Quaternion.identity);
							enemiesLeft--;

							if (enemiesLeft != 0 && wave > 3)
							{
								Instantiate(flyingEye, spawnAreaTR.transform.position, Quaternion.identity);
								enemiesLeft--;
							}

							if (enemiesLeft != 0 && wave > 8)
							{
								Instantiate(flyingEye, transform.position, Quaternion.identity);
								enemiesLeft--;
							}
						}
						else if (rand == 3)
						{
							Instantiate(flyingEye, spawnAreaBR.transform.position, Quaternion.identity);
							enemiesLeft--;

							if (enemiesLeft != 0 && wave > 3)
							{
								Instantiate(flyingEye, transform.position, Quaternion.identity);
								enemiesLeft--;
							}

							if (enemiesLeft != 0 && wave > 8)
							{
								Instantiate(flyingEye, spawnAreaTR.transform.position, Quaternion.identity);
								enemiesLeft--;
							}
						}
						else
						{
							Instantiate(flyingEye, spawnAreaTR.transform.position, Quaternion.identity);
							enemiesLeft--;

							if (enemiesLeft != 0 && wave > 3)
							{
								Instantiate(flyingEye, spawnAreaBL.transform.position, Quaternion.identity);
								enemiesLeft--;
							}

							if (enemiesLeft != 0 && wave > 8)
							{
								Instantiate(flyingEye, spawnAreaBR.transform.position, Quaternion.identity);
								enemiesLeft--;
							}
						}

					}
				}

				while (true)
				{
					yield return new WaitForSeconds(1 / 10);

					if (numberOfEnemies == 0)
					{
						break;
					}
				}

					waveActive = false; //exits loop when no enemies are left and continues onto next wave
					winText.text = "Wave " + wave + " Complete";
				    score += (wave * 10);   //score
				    wavePoints += (wave * 10); //individual score
				    yield return new WaitForSeconds(3);

					winText.text = "";
					wave++;
			}
			
			yield return new WaitForSeconds(7);
			//Wave Progression 
			winText.text = "They're coming...";
			enemiesFEye = enemiesFEye + 1;
			enemiesLeft = enemiesFEye;
		//	if (wave % 2 == 0)
		//	{
		//		eye.health = eye.health + 1;
		//	}
			eye.speed = eye.speed + .005f;

			intervals = intervals - .4f;

			if (wave == 3 || wave == 6 || wave == 12)
				enemiesOnScreen++;
		}
	}

	IEnumerator Dead()//player died
	{
		yield return new WaitForSeconds(1);

		while (true)
		{

			scoreText.text = "";

			deathIns.color = Color.red;
			deathIns.text = "Press 'R' to restart.";

			sScoreText.color = Color.yellow;
			sScoreText.text = "     Evaluation: \n" +
				              "Waves:            +" + wavePoints + "\n" +
							  "Survival Time:  +" + timePoints + "\n" +
							  "Enemy Kills:     +" + enemyPoints + "\n" +
							  "---------------------------------------\n" +
							  "Total Score:       " + score;

			winText.color = Color.red;
			winText.text = "GAME OVER";

			if(Input.GetKeyDown(KeyCode.R))
			{
				SceneManager.LoadScene("idk");
			}
			yield return new WaitForSeconds(1);
		}
	}

	IEnumerator scoreTime()//every second while the wave is active, the player earns 5 points
	{
		while (dead != true)
		{
			yield return new WaitForSeconds(3);

			if (waveActive)
			{
				score += 2;
				timePoints += 2;
			}
		}
	}
}

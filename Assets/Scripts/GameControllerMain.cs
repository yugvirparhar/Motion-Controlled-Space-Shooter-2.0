using UnityEngine;
using System.Collections;

public class GameControllerMain : MonoBehaviour
{
	public GameObject hazard;
	//public GameObject hazard2;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	private bool restart;
	private bool gameOver;
	private int score;
	private bool nextLevel;

	void Start()
	{
		gameOver = false;
		restart = false;
		gameOverText.text = "";
		restartText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	void Update()
	{
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R)) 
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}

		if (nextLevel)
		{
			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				Application.LoadLevel ("Level_2");
			}
		}
	}




	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);
		for (int a = 0; a < 2; a++)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);

				/*Vector3 spawnPosition2 = new Vector3 (Random.Range (-spawnValues.x, spawnValues.y), spawnValues.y, spawnValues.z);
				Instantiate (hazard2, spawnPosition2, spawnRotation);*/
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver) 
			{
				restartText.text = "Press 'R' to restart";
				restart = true;
				//break;
			}
		}

		if (gameOver == false)
		{
			gameOverText.text = "Level UP!! Press the Space Bar.";
			restartText.text = "Non Revolving Asteroids don't like to be Shot";
			nextLevel = true;

		}
	}

	public void AddScore (int scoreValue)
	{
		score += scoreValue;
		UpdateScore ();
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	public void GameOver()
	{ 
		gameOverText.text = "Game Over!";
		gameOver = true;
	}
}

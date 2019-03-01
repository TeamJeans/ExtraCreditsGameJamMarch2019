using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

	// Time limit variables
	[SerializeField]
	float timeLimit = 100.0f;
	float elapsedTimeSinceLevelStarted;
	bool startTime = false;
	[SerializeField]
	TextMeshProUGUI timeLimitText;

	[SerializeField]
	FinishLineManager finishLine;

	// UI objects
	[SerializeField]
	GameObject levelCompleteUI;
	[SerializeField]
	GameObject gameOverUI;
	[SerializeField]
	GameObject mainMenuUI;

	// Player related variables
	[SerializeField]
	GameObject playerObject;
	[SerializeField]
	Transform playerSpawnPoint;
	bool playerWon = false;

	bool gameStarted = false;

	void Start()
	{
		// Disable the ui when the game starts
		levelCompleteUI.SetActive(false);
		gameOverUI.SetActive(false);
		mainMenuUI.SetActive(true);

		// Initialise the timer text
		timeLimitText.text = timeLimit + "";
	}

	void Update()
	{
		//Check if the player has pressed A and start the game if so
		if (!gameStarted)
		{
			if (Input.GetKeyDown("a"))
			{
				startGame();
			}
		}

		// Start the timer
		if (startTime)
		{
			// Increment the time and check if it is equal to the time limit
			elapsedTimeSinceLevelStarted += Time.deltaTime;
			if (elapsedTimeSinceLevelStarted >= timeLimit)
			{
				Debug.Log("Time's Up!");
				startTime = false;

				// If the player hasn't crossed the finish line then they die and have to start again
				if (!finishLine.PlayerCrossed)
				{
					KillPlayer();
				}
			}

			// Update the time limit text to display the right time
			int timeLeftInSeconds = (int)(timeLimit - elapsedTimeSinceLevelStarted);
			timeLimitText.text = timeLeftInSeconds + "";
		}

		// If the player has vrossed the finishline then they have won
		if (finishLine.PlayerCrossed)
		{
			playerWon = true;
		}

		// If the player has won then show the end of level UI
		if (playerWon)
		{
			DisplayLevelCompleteUI();
		}

		// Check if the player has pressed the restart button
		if (Input.GetKeyDown("r"))
		{
			// Restart level
			RestartLevel();
		}
	}

	public void KillPlayer()
	{
		// Kills the player
		Debug.Log("Player killed!");
		DisplayGameOverUI();
	}

	public void DisplayLevelCompleteUI()
	{
		levelCompleteUI.SetActive(true);
	}

	public void DisplayGameOverUI()
	{
		gameOverUI.SetActive(true);
	}

	public void RestartLevel()
	{
		// Reset finish Line
		finishLine.PlayerCrossed = false;
		playerWon = false;

		// Reset UI
		levelCompleteUI.SetActive(false);
		gameOverUI.SetActive(false);

		// Reset elapsedTime
		elapsedTimeSinceLevelStarted = 0.0f;

		// Start the timer
		if (!startTime)
		{
			startTime = true;
			elapsedTimeSinceLevelStarted = 0.0f;
		}

		// RepositionPlayer
		playerObject.transform.position = new Vector3(playerSpawnPoint.position.x, playerSpawnPoint.position.y, playerSpawnPoint.position.z);
	}

	public void startGame()
	{
		gameStarted = true;

		// Disable the mainmenu ui
		mainMenuUI.SetActive(false);

		// Start the timer
		if (!startTime)
		{
			startTime = true;
			elapsedTimeSinceLevelStarted = 0.0f;
		}
	}
}

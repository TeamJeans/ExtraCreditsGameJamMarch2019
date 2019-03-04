using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	AudioManager am;

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
	[SerializeField]
	TextMeshProUGUI levelNameText;

	// Level names
	[SerializeField]
	string level1Name;
	[SerializeField]
	string level2Name;
	[SerializeField]
	string level3Name;

	// Animation
	[SerializeField]
	Animator levelNumberTextFadeAnim;

	// Player related variables
	[SerializeField]
	GameObject playerObject;
	[SerializeField]
	Transform playerSpawnPoint;
	bool playerWon = false;

	bool gameStarted = false;
	bool finalCountdownEnabled = false;
	int timeLeftInSeconds;

	void Awake()
	{
		am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
	}

	void Start()
	{
		// Disable the ui when the game starts
		levelCompleteUI.SetActive(false);
		gameOverUI.SetActive(false);

		// Check if the first level is loaded and only show the game title if so
		if (StaticValueHolder.CurrentLevel == 1)
		{
			mainMenuUI.SetActive(true);
		}
		else
		{
			// Start the next level without needing input to skip the game title
			gameStarted = true;
			startGame();
		}

		// Initialise the timer text
		timeLimitText.text = timeLimit + "";

		// Change the level name depending on the current level
		switch (StaticValueHolder.CurrentLevel)
		{
			case 1:
				ChangeLevelName(level1Name);
				break;
			case 2:
				ChangeLevelName(level2Name);
				break;
			case 3:
				ChangeLevelName(level3Name);
				break;
			default:
				break;
		}

		// RepositionPlayer
		playerObject.transform.position = new Vector3(playerSpawnPoint.position.x, playerSpawnPoint.position.y, playerSpawnPoint.position.z);

		finalCountdownEnabled = false;
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
			timeLeftInSeconds = (int)(timeLimit - elapsedTimeSinceLevelStarted);
			if (elapsedTimeSinceLevelStarted >= timeLimit)
			{
				// Stop the countdown
				finalCountdownEnabled = false;
				Debug.Log("Time's Up!");
				startTime = false;

				// If the player hasn't crossed the finish line then they die and have to start again
				if (!finishLine.PlayerCrossed)
				{
					KillPlayer();
				}
			}

			// Update the time limit text to display the right time
			timeLeftInSeconds = (int)(timeLimit - elapsedTimeSinceLevelStarted);
			timeLimitText.text = timeLeftInSeconds + "";
		}

		// If the player has vrossed the finishline then they have won
		if (finishLine.PlayerCrossed)
		{
			playerWon = true;
		}

		// If the player has won then show the end of level UI and let them goto the next level
		if (playerWon)
		{
			DisplayLevelCompleteUI();
			if (Input.GetKeyDown("a"))
			{
				// Go the the next level
				switch (StaticValueHolder.CurrentLevel)
				{
					case 1:
						StaticValueHolder.CurrentLevel++;
						GoToScene("Level2");
						break;
					case 2:
						StaticValueHolder.CurrentLevel++;
						GoToScene("Level3");
						break;
					case 3:
						StaticValueHolder.CurrentLevel++;
						GoToScene("WinScene");
						break;
					default:
						break;
				}

			}
		}

		// Check if the player has pressed the restart button
		if (Input.GetKeyDown("r"))
		{
			// Restart level
			RestartLevel();
		}

		Debug.Log("Final Countdown:" + finalCountdownEnabled);

		// If the countdown has reached start playing the clock sound
		if (!finalCountdownEnabled && timeLeftInSeconds <= 10)
		{
			Debug.Log("Works");
			finalCountdownEnabled = true;
			am.PlaySound("ClockTick");
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
		finalCountdownEnabled = false;

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
		finalCountdownEnabled = false;

		// Disable the mainmenu ui
		mainMenuUI.SetActive(false);

		// Start the timer
		if (!startTime)
		{
			startTime = true;
			elapsedTimeSinceLevelStarted = 0.0f;
		}

		// Show the level number
		levelNumberTextFadeAnim.SetBool("PlayFadeAnim", true);
	}

	public void ChangeLevelName(string levelName)
	{
		levelNameText.text = "LEVEL " + StaticValueHolder.CurrentLevel + ":" + levelName;
	}

	public void GoToScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}

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
	bool playVictorySound = false;
	int timeLeftInSeconds;
	float counterForASecond = 0;

	void Awake()
	{
		am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
	}

	void Start()
	{
		// Setup the amount of time left
		timeLeftInSeconds = (int)timeLimit;

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
	}

	void Update()
	{
		// TEST STUFF
		//AudioListener[] aL = FindObjectsOfType<AudioListener>();
		//for (int i = 0; i < aL.Length; i++)
		//{
		//	Debug.Log(aL[i]);
		//}
		//Debug.Log("Space");

		//Check if the player has pressed A and start the game if so
		if (!gameStarted)
		{
			if (Input.GetKeyDown("a"))
			{
				startGame();
			}
		}

		// Calculate the amount of time that the player has left
		timeLeftInSeconds = (int)(timeLimit - elapsedTimeSinceLevelStarted);

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
			timeLimitText.text = timeLeftInSeconds + "";
		}

		// If the player has vrossed the finishline then they have won
		if (finishLine.PlayerCrossed)
		{
			if (!playVictorySound)
			{
				playerWon = true;
				playVictorySound = true;
				am.PlaySound("VictorySound");
			}
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

		// If the countdown has reached start playing the clock sound
		if (!finalCountdownEnabled && timeLeftInSeconds <= 10 && elapsedTimeSinceLevelStarted <= timeLimit)
		{
			finalCountdownEnabled = true;
			timeLimitText.color = Color.red;
		}

		// Activate the countdown
		counterForASecond += Time.deltaTime;
		if (counterForASecond >= 1 && finalCountdownEnabled)
		{
			counterForASecond = 0;
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
		playVictorySound = false;

		// Reset UI
		levelCompleteUI.SetActive(false);
		gameOverUI.SetActive(false);
		timeLimitText.color = Color.white;

		// Reset elapsedTime
		elapsedTimeSinceLevelStarted = 0.0f;

		// Setup the amount of time left
		timeLeftInSeconds = (int)timeLimit;

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
		// Play the start noise
		am.PlaySound("StartNoise");

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

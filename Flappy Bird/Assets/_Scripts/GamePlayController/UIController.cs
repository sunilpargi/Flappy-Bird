using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	public static UIController instance;

	[SerializeField]
	private Text scoreText, endScore, bestScore, gameOverText;

	[SerializeField]
	private Button restartGameButton, instructionsButton;

	[SerializeField]
	private GameObject pausePanel;

	[SerializeField]
	private GameObject[] birds;

	[SerializeField]
	private Sprite[] medals;

	[SerializeField]
	private Image medalImage;
	public AudioClip uITouch;
	void Awake()
	{
		MakeInstance();
		Time.timeScale = 0f;
	}

	// Use this for initialization
	void Start()
	{

	}

	void MakeInstance()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public void PauseGame()
	{
		AudioSource.PlayClipAtPoint(uITouch, Camera.main.transform.position);
		if (BIrd.instance != null)
		{
			if (BIrd.instance.isAlive)
			{
				pausePanel.SetActive(true);
				gameOverText.text = "Pause";
				endScore.text = "" + BIrd.instance.score;
				bestScore.text = "" + GameplayController.instance.GetHighscore();
				
				restartGameButton.onClick.RemoveAllListeners();
				restartGameButton.onClick.AddListener(() => ResumeGame());
				Time.timeScale = 0f;
			}
		}
	}

	public void GoToMenuButton()
	{
		AudioSource.PlayClipAtPoint(uITouch, Camera.main.transform.position);
		SceneFader.instance.FadeIn("MainMenu");
	}

	public void ResumeGame()
	{
		AudioSource.PlayClipAtPoint(uITouch, Camera.main.transform.position);
		pausePanel.SetActive(false);
		Time.timeScale = 1f;
	}

	public void RestartGame()
	{
		AudioSource.PlayClipAtPoint(uITouch, Camera.main.transform.position);
		SceneFader.instance.FadeIn(Application.loadedLevelName);
	}

	public void PlayGame()
	{
		AudioSource.PlayClipAtPoint(uITouch, Camera.main.transform.position);
		scoreText.gameObject.SetActive(true);
		birds[GameplayController.instance.GetSelectedBird()].SetActive(true);
		instructionsButton.gameObject.SetActive(false);
		Time.timeScale = 1f;
	}

	public void SetScore(int score)
	{
		scoreText.text = "" + score;
	}

	public void PlayerDiedShowScore(int score)
	{
		pausePanel.SetActive(true);
		gameOverText.text = "Game over";
		scoreText.gameObject.SetActive(false);

		endScore.text = "" + score;

		if (score > GameplayController.instance.GetHighscore())
		{
			GameplayController.instance.SetHighscore(score);
		}

		bestScore.text = "" + GameplayController.instance.GetHighscore();

		if (score <= 20)
		{
			medalImage.sprite = medals[0];
		}
		else if (score > 20 && score < 40)
		{
			medalImage.sprite = medals[1];

			if (GameplayController.instance.IsGreenBirdUnlocked() == 0)
			{
				GameplayController.instance.UnlockGreenBird();
			}

		}
		else
		{
			medalImage.sprite = medals[2];

			if (GameplayController.instance.IsGreenBirdUnlocked() == 0)
			{
				GameplayController.instance.UnlockGreenBird();
			}

			if (GameplayController.instance.IsRedBirdUnlocked() == 0)
			{
				GameplayController.instance.UnlockRedBird();
			}

		}

		restartGameButton.onClick.RemoveAllListeners();
		restartGameButton.onClick.AddListener(() => RestartGame());

	}
}

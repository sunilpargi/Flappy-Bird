using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
	public static MainMenuController instance;

	[SerializeField]
	private GameObject[] birds;

	private bool isGreenBirdUnlocked, isRedBirdUnlocked;
	public AudioClip uITouch;
	void Awake()
	{
		MakeInstance();
	}

	void Start()
	{
		birds[GameplayController.instance.GetSelectedBird()].SetActive(true);
		CheckIfBirdsAreUnlocked();
	}

	void MakeInstance()
	{
		if (instance == null)
		{
			instance = this;
		}
	}
	void CheckIfBirdsAreUnlocked()
	{
		if (GameplayController.instance.IsRedBirdUnlocked() == 1)
		{
			isRedBirdUnlocked = true;
		}

		if (GameplayController.instance.IsGreenBirdUnlocked() == 1)
		{
			isGreenBirdUnlocked = true;
		}
	}
	public void ChangeBird()
	{

		if (GameplayController.instance.GetSelectedBird() == 0)
		{

			if (isGreenBirdUnlocked)
			{
				
				birds[0].SetActive(false);
				GameplayController.instance.SetSelectedBird(1);
				birds[GameplayController.instance.GetSelectedBird()].SetActive(true);
			}

		}
		else if (GameplayController.instance.GetSelectedBird() == 1)
		{

			if (isRedBirdUnlocked)
			{
				
				birds[1].SetActive(false);
				GameplayController.instance.SetSelectedBird(2);
				birds[GameplayController.instance.GetSelectedBird()].SetActive(true);

			}
			else
			{
				
				birds[1].SetActive(false);
				GameplayController.instance.SetSelectedBird(0);
				birds[GameplayController.instance.GetSelectedBird()].SetActive(true);

			}

		}
		else if (GameplayController.instance.GetSelectedBird() == 2)
		{
			birds[2].SetActive(false);
			GameplayController.instance.SetSelectedBird(0);
			birds[GameplayController.instance.GetSelectedBird()].SetActive(true);
		}
		AudioSource.PlayClipAtPoint(uITouch, Camera.main.transform.position);
	}

	public void PlayGame()
	{
		AudioSource.PlayClipAtPoint(uITouch, Camera.main.transform.position);
		SceneFader.instance.FadeIn("Gameplay");
	}
}

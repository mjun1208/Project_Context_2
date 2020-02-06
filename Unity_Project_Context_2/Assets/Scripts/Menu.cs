using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
	[SerializeField]
	public GameObject startScreen;
	[SerializeField]
	public GameObject infoScreen;

	public static bool b_GameStarted = false;

	//TODO: basic menu for gamejam, to be fixed after gamejam

    void Start()
    {
		Time.timeScale = 0;
		infoScreen.SetActive(false);
		startScreen.SetActive(!b_GameStarted);
	}

    public void StartGame() 
	{
		b_GameStarted = true;
		startScreen.SetActive(!b_GameStarted);
		Time.timeScale = 1;
	}

	public void InfoScreen() 
	{
		infoScreen.SetActive(!infoScreen.activeSelf);
	}
}

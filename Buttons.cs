using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Buttons : MonoBehaviour {

    public static int CurrentCanvas;
    public GameObject[] levels;
    public GameObject[] GoButt;
    public GameObject PauseMenu, Interface;

    private GameObject Player;
    private AudioSource audio;
    //private string tmpstr;


	void Start () {
        Player = GameObject.Find("Player");
        CurrentCanvas = 0;
        audio = Player.GetComponent<AudioSource>();
    }

	void Update () {
		
	}

    private void EditCount(int value)
    {
        SPlayerPrefs.SetInt("Count", value);
        SPlayerPrefs.Save();
    }

    void OnMouseUpAsButton()
    {
		switch (gameObject.name)
        {
            case "Play":
                //CameraShake.Shake(0.5f, 0.25f, CameraShake.ShakeMode.XY);
                SceneManager.LoadScene("LevelSelect", LoadSceneMode.Single);
                EditCount(0);
                break;

            case "Go":
                if (SPlayerPrefs.GetInt("Buyed_" + CurrentCanvas) == 1)
                {
                    if (CurrentCanvas == 6)
                        SceneManager.LoadScene("LevelTest", LoadSceneMode.Single);
                    else
                        SceneManager.LoadScene("Level1_" + (CurrentCanvas + 1), LoadSceneMode.Single);
                } else if (SPlayerPrefs.GetInt("Buyed_" + CurrentCanvas) == 0)
                {
                    LvlSelectInfo.BuyLevel(CurrentCanvas);
                }
                break;

            case "Shop":
                SceneManager.LoadScene("Shop", LoadSceneMode.Single);
                break;

            case "BackToMenu":
                SceneManager.LoadScene("main", LoadSceneMode.Single);
                break;

            case "Twitter":
                Application.OpenURL("https://twitter.com/PionInkApps");
                Debug.Log("twit");
                break;

            case "Pause":
                Time.timeScale = 0;
                PauseMenu.SetActive(true);
                Interface.SetActive(false);
                audio.Pause();
                break;

            case "RightArrow":
                if (CurrentCanvas < levels.Length-1)
                {
                    levels[CurrentCanvas].SetActive(false);
                    CurrentCanvas++;
                    levels[CurrentCanvas].SetActive(true);
                    EditCount(CurrentCanvas);
                }     
                break;

            case "LeftArrow":
                if (CurrentCanvas > 0)
                {
                    levels[CurrentCanvas].SetActive(false);
                    CurrentCanvas--;
                    levels[CurrentCanvas].SetActive(true);
                    EditCount(CurrentCanvas);
                } 
                break;
            
        }
        
    }
	// Наведена
	void OnMouseEnter() {
		switch (gameObject.name) 
		{
		    case "Play":
                GameObject.Find("TNTCube").transform.localScale = new Vector3 (2.5f, 2.5f, 2.5f);
			    break;
            case "RightArrow":
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                break;
            case "LeftArrow":
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                break;
            case "Go":
                GameObject.Find("GoButton").transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                break;
            //case "Resume":
            //    transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            //    break;
            case "BackToMenu":
                transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                break;
        }
	}

    // Не наведена
    void OnMouseExit() {
		switch (gameObject.name) 
		{
		    case  "Play":
                GameObject.Find("TNTCube").transform.localScale = new Vector3 (2f, 2f, 2f);
			    break;
            case "RightArrow":
                transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            case "LeftArrow":
                transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            case "Go":
                GameObject.Find("GoButton").transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            //case "Resume":
            //    transform.localScale = new Vector3(1f, 1f, 1f);
            //    break;
            case "BackToMenu":
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                break;
        }
	}

    public void Continue()
    {
        PauseMenu.SetActive(false);
        Interface.SetActive(true);
        Time.timeScale = 1;
        Player.GetComponent<Player>().isPause = false;
        audio.Play();

    }

    public void ToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect", LoadSceneMode.Single);
        EditCount(0);
        Time.timeScale = 1;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("main", LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public void ChangeVolume()
    {
        audio.mute = !audio.mute;
    }

    // Нажата
    private void OnMouseDown()
    {
        switch (gameObject.name)
        {
            case "JumpButt":
                Player.GetComponent<Player>().JumpBool = true;
                break;
            case "Pause":
                Player.GetComponent<Player>().isPause = true;
                break;
        }
    }

    // Отжата
    private void OnMouseUp()
    {
        switch (gameObject.name)
        {
            case "JumpButt":
                Player.GetComponent<Player>().JumpBool = false;
                break;
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    public GameObject sun;
    private Transform player;
    public int lengthMap = 275;
    private int lvlCount;
    private float X, progress;

	void Start () {
        if (SPlayerPrefs.HasKey("Count"))
            lvlCount = SPlayerPrefs.GetInt("Count");
        else SPlayerPrefs.SetInt("Count", 0);
        if (SPlayerPrefs.HasKey("Progress_" + lvlCount))
            progress = SPlayerPrefs.GetFloat("Progress_" + lvlCount);
        else SPlayerPrefs.SetFloat("Progress_" + lvlCount, 0);
        player = GameObject.Find("Player").transform;
    }
	
	void Update () {
        X = (player.position.x / lengthMap) ;
        sun.GetComponent<RectTransform>().localPosition = new Vector3(X * 800 - 400, 192, 0);
        if (X >= 1)
            progress = 100;
        else if (X * 100 > progress)
            progress = X * 100;
        Debug.Log("progress: " + progress);
    }

    private void OnDisable()
    {
        Debug.Log("Save in disable");
        if(progress > SPlayerPrefs.GetFloat("Progress_" + lvlCount)) {
            Debug.Log("Save " + progress);
            SPlayerPrefs.SetFloat("Progress_" + lvlCount, progress);
            SPlayerPrefs.Save();
        }
        
    }
}

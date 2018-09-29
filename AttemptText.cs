using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttemptText : MonoBehaviour {

    public int lvlCount, Attempts;

	void Start () {
        if (SPlayerPrefs.HasKey("Count"))
        {
            lvlCount = SPlayerPrefs.GetInt("Count");
        }
        else SPlayerPrefs.SetInt("Count", 0);

        if (SPlayerPrefs.HasKey("Attempts_" + lvlCount))
        {
            Attempts = SPlayerPrefs.GetInt("Attempts_" + lvlCount);
        }
        else SPlayerPrefs.SetInt("Attempts_" + lvlCount, 0);
        GetComponent<Text>().text = "Attempt " + SPlayerPrefs.GetInt("Attempts_" + lvlCount).ToString();
    }
	

	void FixedUpdate() {
        GetComponent<Text>().text = "Attempt " + SPlayerPrefs.GetInt("Attempts_" + lvlCount).ToString();
    }

}

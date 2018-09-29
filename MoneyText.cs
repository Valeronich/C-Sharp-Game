using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour {

    public int money = 0;

	void Start () {
        if (SPlayerPrefs.HasKey("Money"))
        {
            money = SPlayerPrefs.GetInt("Money");
        }
        else SPlayerPrefs.SetInt("Money", 0);
    }

	void FixedUpdate() {
        GetComponent<Text>().text = SPlayerPrefs.GetInt("Money").ToString();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlSelectInfo : MonoBehaviour {

    private GameObject jumps, attempts, percent, playOrBuy;
    private int lvlAttempts = 0, lvlJumps = 0;
    private static int lvlCount = 0, lvlBuyed = 0, money;
    private static int[] priceLvl = new int[] {0, 0, 50, 100, 200 };
    private float progress;

    private float Round(float x) {
        int y;
        y = (int)(x * 100);
        x = y;
        return x/100;
    }

    public static void BuyLevel(int cnt)
    {
        if( money >= priceLvl[cnt])
        {
            money -= priceLvl[cnt];
            SPlayerPrefs.SetInt("Money", money);
            SPlayerPrefs.SetInt("Buyed_" + lvlCount, 1);
            lvlBuyed = 1;
            SPlayerPrefs.Save();
        }

    }

	void Start () {
        jumps = GameObject.Find("Jumps");
        attempts = GameObject.Find("Attempts");
        percent = GameObject.Find("Percentage");
        playOrBuy = GameObject.Find("PlayOrBuy");
        //Min amount of jumps to complete the lvl
        lvlCount = SPlayerPrefs.GetInt("Count");
        //Первые две карты изначально открыты
        SPlayerPrefs.SetInt("Buyed_0", 1);
        SPlayerPrefs.SetInt("Buyed_1", 1);
        SPlayerPrefs.Save();

        if (SPlayerPrefs.HasKey("Jumps_" + lvlCount))
            lvlJumps = SPlayerPrefs.GetInt("Jumps_" + lvlCount);
        else SPlayerPrefs.SetInt("Jumps_" + lvlCount, 0);
        if (SPlayerPrefs.HasKey("Attempts_" + lvlCount))
            lvlAttempts = SPlayerPrefs.GetInt("Attempts_" + lvlCount);
        else SPlayerPrefs.SetInt("Attempts_" + lvlCount, 0);
        if (SPlayerPrefs.HasKey("Progress_" + lvlCount))
            progress = SPlayerPrefs.GetFloat("Progress_" + lvlCount);
        else SPlayerPrefs.SetFloat("Progress_" + lvlCount, 0);
        if (SPlayerPrefs.HasKey("Buyed_" + lvlCount))
            lvlBuyed = SPlayerPrefs.GetInt("Buyed_" + lvlCount);
        else SPlayerPrefs.SetInt("Buyed_" + lvlCount, 0);
        if (SPlayerPrefs.HasKey("Money"))
            money = SPlayerPrefs.GetInt("Money");
        else SPlayerPrefs.SetInt("Money", 0);
    }
	
	void Update () {        
        Debug.Log("lvl= " + lvlCount);
        jumps.GetComponent<Text>().text = "jumps: " + lvlJumps;
        attempts.GetComponent<Text>().text = "attempts: " + lvlAttempts;
        percent.GetComponent<Text>().text = Round(progress) + "%";
        if(lvlBuyed == 0) {
            playOrBuy.GetComponent<Text>().text = "Buy " + priceLvl[lvlCount];
        } else
            playOrBuy.GetComponent<Text>().text = "Play";
    }

}

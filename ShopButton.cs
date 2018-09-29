using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour {

    private const string STATEITEM = "StateItem";
    private GameObject textButton, tnt, visualStateSelected, visualStateLock;
    private int itemCount;
    private static bool flagOnceInitFirstItem = true;
    private static int money, direct;
    private static int[] priceItem = new int[] {0, 20, 50, 100, 150, 200, 300, 450, 500, 620, 700, 750, 800, 970, 1000, 1100, 1200, 1300, 1500, 1750, 2000, 2205, 2500, 2700, 3000, 3500, 3750, 4000, 5000, 10000 };
    //private string state = "init";

	void Start () {
        textButton = GameObject.Find("TextButton");
        tnt = GameObject.Find("TntButton");
        if (!SPlayerPrefs.HasKey("Money"))
            SPlayerPrefs.SetInt("Money", 0); 
        money = SPlayerPrefs.GetInt("Money");
        //SPlayerPrefs.SetString(STATEITEM + "1", "Buy");
        if (gameObject != tnt)
        {
            visualStateSelected = transform.GetChild(1).gameObject;
            visualStateLock = transform.GetChild(2).gameObject;

            itemCount = 0;
            while (gameObject.name != ("Item" + itemCount))
                if (itemCount++ > 31)
                    break;

            if (flagOnceInitFirstItem) {
                flagOnceInitFirstItem = false;
                if (!SPlayerPrefs.HasKey(STATEITEM + "0")) {
                    SPlayerPrefs.SetString(STATEITEM + "0", "Selected");
                    SPlayerPrefs.Save();
                }
            }

            if (!SPlayerPrefs.HasKey(STATEITEM + itemCount))
                SPlayerPrefs.SetString(STATEITEM + itemCount, "Buy");
            if (itemCount == 10)
                gameObject.GetComponent<Image>().color = Color.yellow;
        }
    }

    private void Update()
    {
        if (gameObject != tnt)
        {
            string state = SPlayerPrefs.GetString(STATEITEM + itemCount);
            switch (state)
            {
                case "Selected":
                    visualStateLock.SetActive(false);
                    visualStateSelected.SetActive(true);
                    break;
                case "Buyed":
                    visualStateLock.SetActive(false);
                    visualStateSelected.SetActive(false);
                    break;
                case "Buy":
                    visualStateSelected.SetActive(false);
                    visualStateLock.SetActive(true);
                    break;
            }
        }
    }

    private void OnMouseUpAsButton()
    {
        string state; 
        if (gameObject == tnt) {
            state = SPlayerPrefs.GetString(STATEITEM + direct);
            if (state == "Buy") {
                Debug.Log("pez " + direct);
                money = SPlayerPrefs.GetInt("Money");
                if(money >= priceItem[direct]) {
                    money -= priceItem[direct];
                    SPlayerPrefs.SetInt("Money", money);
                    SPlayerPrefs.SetString(STATEITEM + direct, "Buyed");
                    SPlayerPrefs.Save();
                    textButton.GetComponent<Text>().text = "Buyed";
                }
            }

        } else {
            //gameObject.GetComponent<Image>().color = Color.red;
            direct = itemCount;
            state = SPlayerPrefs.GetString(STATEITEM + itemCount);
            Debug.Log(state);
            if (state == "Buyed")
            {
                for (int i = 0; i < priceItem.Length; i++)
                {
                    if (SPlayerPrefs.GetString(STATEITEM + i) == "Selected")
                    {
                        SPlayerPrefs.SetString(STATEITEM + i, "Buyed");
                        break;
                    }
                }
                SPlayerPrefs.SetString(STATEITEM + itemCount, "Selected");
                SPlayerPrefs.SetInt("DigitStateItemIsSelected", itemCount);
                SPlayerPrefs.Save();
                textButton.GetComponent<Text>().text = "Selected";
            } else if(state == "Selected") {
                textButton.GetComponent<Text>().text = "Selected";
            } else if(state == "Buy") { 
                textButton.GetComponent<Text>().text = "Buy " + priceItem[itemCount];
            }
            Debug.Log("post" + state);
        }
        
    }


}

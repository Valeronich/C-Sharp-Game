using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsButton : MonoBehaviour {

    public GameObject window, inter, vendorText, ok, cancel;
    private string vendorCallText = "Don't have enough money? Would you like to get some cash?)", 
        vendorHaveNotJobText = "Sorry, I haven't jobs for you.", 
        vendorErrorText = "Ooh.. Something went wrong", 
        vendorSuccessText = "Good job!"; 
    void Start () {
        if (Advertisement.isSupported)
            Advertisement.Initialize("1744251", false);
        else
            Debug.Log("ads is not supported");
    }
	
	void Update () {
		
	}

    public void AgreeAds()
    {
        ShowAd();
        cancel.GetComponentInChildren<Text>().text = "OK";
        ok.SetActive(false);
        cancel.SetActive(true);
    }

    public void ExitWindowAds()
    {
        inter.SetActive(true);
        window.SetActive(false);
    }

    private void OnMouseUpAsButton()
    {
        if (Advertisement.IsReady("rewardedVideo")) {
            vendorText.GetComponent<Text>().text = vendorCallText;
            ok.GetComponentInChildren<Text>().text = "OK";
            cancel.GetComponentInChildren<Text>().text = "Cancel";
            ok.SetActive(true);
            cancel.SetActive(true);
        } else {
            vendorText.GetComponent<Text>().text = vendorHaveNotJobText;
            cancel.GetComponentInChildren<Text>().text = "OK";
            ok.SetActive(false);
            cancel.SetActive(true);
        }
        window.SetActive(true);
        inter.SetActive(false);
    }

    private void ShowAd()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        Advertisement.Show("rewardedVideo", options);
    }

    private void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Debug.Log("Video completed - Offer a reward to the player");
            vendorText.GetComponent<Text>().text = vendorSuccessText;
            SPlayerPrefs.SetInt("Money", SPlayerPrefs.GetInt("Money") + Random.Range(10, 90));
            SPlayerPrefs.Save();
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("Video was skipped - Do NOT reward the player");
            vendorText.GetComponent<Text>().text = vendorErrorText;

        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
            vendorText.GetComponent<Text>().text = vendorErrorText;
        }
    }

    void OnMouseEnter()
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    void OnMouseExit()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}

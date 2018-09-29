using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

// money в скрипте Player

public class Player : MonoBehaviour {

    public GameObject Respawn;
    public GameObject Player1;
    public GameObject dust;
    private GameObject Camera;
    public GameObject DiedText;
    public GameObject FinalMenu, Interface;
    public GameObject jumpText; //потом уберу
    private int jump = 0; // и это тоже

    public GameObject[] Clouds;
    private GameObject[] Coins;
	private bool OnceFuncDeathCall, OnceFuncWinCall = false;

    public ParticleSystem death;

    private int CloudNum, CloudRnd;
    private int money, TotalJumps;
    private int lvlCount = 0, lvlAttempts, lvlJumps, lvlCompleted;
    public int finalPosition;
    private string[] diedPhrase = new string[] { "YOU DIED", "TRY AGAIN", "DIED", "NOOOOOOOOOOO!!!" };
    private int countDied = 0;
    private static int countAds = 0;
    private static float lastTimeShowAds;

    public float force = 750f;
    public float speedX = 80f;

    private bool isGrounded = false;
    public bool JumpBool = false;
	public bool isDeathPlayer = false;
    public bool isPause = false;

    private Vector3 CloudPos;

    private Rigidbody2D rb;

    private void Awake()
    {
        // PLAYER PREFS INIT BEGIN
        if (SPlayerPrefs.HasKey("Count"))
            lvlCount= SPlayerPrefs.GetInt("Count");
        else SPlayerPrefs.SetInt("Count", 0);

        if (SPlayerPrefs.HasKey("Money"))
            money = SPlayerPrefs.GetInt("Money");
        else SPlayerPrefs.SetInt("Money", 0);

        if (SPlayerPrefs.HasKey("TotalJumps"))
            TotalJumps = SPlayerPrefs.GetInt("TotalJumps");
        else SPlayerPrefs.SetInt("TotalJumps", 0);

        if (SPlayerPrefs.HasKey("Attempts_" + lvlCount))
            lvlAttempts = SPlayerPrefs.GetInt("Attempts_" + lvlCount);
        else SPlayerPrefs.SetInt("Attempts_" + lvlCount, 0);

        if (SPlayerPrefs.HasKey("Jumps_" + lvlCount))
            lvlJumps = SPlayerPrefs.GetInt("Jumps_" + lvlCount);
        else SPlayerPrefs.SetInt("Jumps_" + lvlCount, 0);

        if (SPlayerPrefs.HasKey("Completed_" + lvlCount))
            lvlCompleted = SPlayerPrefs.GetInt("Completed_" + lvlCount);
        else SPlayerPrefs.SetInt("Completed_" + lvlCount, 0);

        // PLAYER PREFS INIT END
    }

    private void Start ()
    {
        lastTimeShowAds = Time.time;
        if (Advertisement.isSupported) 
            Advertisement.Initialize("1744251", false);
        else
            Debug.Log("ads is not supported");
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = Mathf.Abs(rb.gravityScale);
        Camera = GameObject.Find("Main Camera");
        Player1 = GameObject.Find("Player");
        dust = GameObject.Find("Dust");
        Coins = GameObject.FindGameObjectsWithTag("Coin");
    }

    private void FixedUpdate()
    {
        CheckGround();
        CharacterMove();

    }

    private void Update()
    {

    }

    private void Win()
    {
        CameraShake.Shake(5f, 0.35f, CameraShake.ShakeMode.XY);
        Interface.SetActive(false);
        FinalMenu.SetActive(true);
        SPlayerPrefs.SetInt("Completed_" + lvlCount, 1);
        SaveData();
        GameObject.Find("attempts").GetComponent<Text>().text = "Attempts: " + lvlAttempts;
        GameObject.Find("jumps").GetComponent<Text>().text = "Jumps: " + lvlJumps;
    }

    private void SaveData()
    {
        SPlayerPrefs.SetInt("Attempts_" + lvlCount, lvlAttempts);
        SPlayerPrefs.SetInt("Money", money);
        SPlayerPrefs.SetInt("TotalJumps", TotalJumps);
        SPlayerPrefs.SetInt("Jumps_" + lvlCount, lvlJumps);
        SPlayerPrefs.Save();
    }

    private void CharacterMove()
    {
        if(transform.position.x >= finalPosition)
        {
            if (!OnceFuncWinCall)
            {
                OnceFuncWinCall = true;
                speedX = 0;
                Win();
            }
        }
		else if (!isDeathPlayer) 
		{
			transform.Translate (speedX * Time.deltaTime, 0, 0);
			if (isGrounded && JumpBool && !isPause) // JUMP
			{
                if(rb.gravityScale >= 0)
				    rb.velocity = force * Vector2.up * Time.deltaTime;
                else
                    rb.velocity = force * Vector2.down * Time.deltaTime;
                TotalJumps++;
                lvlJumps++;
                jumpText.GetComponent<Text>().text = "jumps: " + ++jump; //временная
			}
		}
//        if ((JumpBool) && (DeathPlayer))
 //       {
//            RebirthFunc();
 //       }
    }

    private void CheckGround() // НА ЗЕМЛЕ ЛИ ИГРОК?
    {
        isGrounded = (rb.velocity.y == 0) ? true : false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "StartAntigravy")
            rb.gravityScale = -1 * Mathf.Abs(rb.gravityScale);
        else if(other.tag == "EndAntigravy")
            rb.gravityScale = Mathf.Abs(rb.gravityScale);

        if ((other.tag == "Obstacle") && (!isDeathPlayer))
        {
			if (!OnceFuncDeathCall) { 
				DeathFunc (); 
			}
        }
		if ((other.tag == "JumpButt") && (isDeathPlayer))
		{
			RebirthFunc ();
		}
        if (other.tag == "Coin")
        {
            other.gameObject.SetActive(false);
            money = SPlayerPrefs.GetInt("Money");
            money += 5;
            SPlayerPrefs.SetInt("Money", money);
        }
    }
  
  //  private void OnCollisionEnter2D(Collision2D other)
  //  {
  //      if ((other.tag == "Obstacle"))    
  //  }

    public void DeathFunc()
	{
        jump = 0;
		OnceFuncDeathCall = true;
		Instantiate(death, transform.position, transform.rotation);
		isDeathPlayer = true;
//		transform.Translate(0, 0, 0);
//		Camera.transform.Translate (0, 0, 0);
		dust.SetActive (false);
        DiedText.GetComponent<Text>().text = diedPhrase[countDied % diedPhrase.Length];
        countDied++;
        DiedText.SetActive(true);
        dust.SetActive(true);
        Player1.SetActive(false);
        CameraShake.Shake(0.5f, 0.35f, CameraShake.ShakeMode.XY);
        lvlAttempts++;
        SaveData();

        countAds++;
        if (Advertisement.IsReady() && countAds >= 12 && Time.time > lastTimeShowAds + 180 && isDeathPlayer) {
            Advertisement.Show();
            countAds = 0;
            lastTimeShowAds = Time.time;
        }
    }

	public void RebirthFunc()
	{
        rb.gravityScale = Mathf.Abs(rb.gravityScale);
        OnceFuncDeathCall = false;
		isDeathPlayer = false;
		Player1.transform.position = Respawn.transform.position;             //8.5f
		Camera.transform.position = new Vector3(Respawn.transform.position.x + 6.5f, Respawn.transform.position.y + 3.6f, -8);
        dust.SetActive(true);
        DiedText.SetActive(false);
        Player1.SetActive(true);
        for (int i = 0; i < Coins.Length; i++)
        {
            Coins[i].SetActive(true);
        }

    } 

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehav : MonoBehaviour {

	private bool isDeathPlayer, JumpBool;

    private float speedX;
    private Transform target;
    private GameObject Player;

    //private float PlayerX;
    private float PlayerY;
    //private float CameraX;
    private float CameraY;

    private GameObject RebirthFun;

    private void Awake()
    {
        // if (!target) target = FindObjectOfType<Player>().transform;
    }

    void Start () 
	{
        Player = GameObject.Find("Player");
        //      RebirthFun = gameObject.GetComponent("Player.cs");

	}
	
	void Update () 
	{
		
	}

    private void FixedUpdate()
    {
        speedX = Player.GetComponent<Player>().speedX;
        // Проверка смерти персонажа
        isDeathPlayer = Player.GetComponent<Player>().isDeathPlayer;
        JumpBool = Player.GetComponent<Player>().JumpBool;

        if (!isDeathPlayer)
        {
            transform.Translate(speedX * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Translate(0, 0, 0);
            if (JumpBool)
            {
                Player.GetComponent<Player>().RebirthFunc();
            }
        }

    }

    private void LateUpdate()
    {
        // Ориентация камеры по вертикали, вслед за игроком
        //PlayerX = Player.transform.position.x;
        PlayerY = Player.transform.position.y;
        //CameraX = transform.position.x;
        CameraY = transform.position.y;

        if (CameraY - PlayerY < 0)
        {
            transform.position = new Vector3(transform.position.x, PlayerY, transform.position.z);
        }
        else if (CameraY - PlayerY > 3.5f)
        {
            transform.position = new Vector3(transform.position.x, PlayerY + 3.5f, transform.position.z);
        }

    }
}

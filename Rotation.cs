using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

    private Sprite[] skins = new Sprite[30];
    private int cnt;
    private bool isGrounded = false;
    private float forcee = 3.75f; //3.145f
    private float angle;
    public Vector3 axis = Vector3.zero;
    private Rigidbody2D rb;

    void Start () {
        rb = gameObject.GetComponentInParent<Rigidbody2D>();
        if (!SPlayerPrefs.HasKey("DigitStateItemIsSelected"))
            SPlayerPrefs.SetInt("DigitStateItemIsSelected", 0);
        cnt = SPlayerPrefs.GetInt("DigitStateItemIsSelected");
        for(int i = 0; i < skins.Length; i++)
            skins[i] = Resources.Load<Sprite>("Sprites/skins/skin" + i);
        //cnt = 17;
        gameObject.GetComponent<SpriteRenderer>().sprite = skins[cnt];
    }
	
	void Update () {

    }

    private void FixedUpdate()
    {
        CharRotate();
    }

    private void CheckGround()
    {
        isGrounded = (rb.velocity.y == 0) ? true : false;
    }

    private void CharRotate()
    { 
        CheckGround();
        if (!isGrounded)
        {
            forcee = 3.75f; // Вот здесь меняем скорость докрута
            transform.Rotate(0, 0, -90 * Time.deltaTime * forcee);
        }
        else
        {
            forcee = 3.75f; // И вот здесь меняем скорость докрута
            angle = transform.rotation.z;
            transform.rotation.ToAngleAxis(out angle, out axis);
            
            if (angle >= 1 && angle <= 45)
            {
                transform.rotation *= Quaternion.AngleAxis(-forcee, Vector3.forward);
            }
            else if ((angle >= 45 && angle < 88) || (angle > 92 && angle <= 135)) 
            {
                transform.rotation *= Quaternion.AngleAxis(-forcee, Vector3.forward);
            }
            else if ((angle >= 135 && angle < 178) || (angle > 182 && angle <= 225))
            {
                transform.rotation *= Quaternion.AngleAxis(-forcee, Vector3.forward);
            }
            else if ((angle >= 225 && angle < 268) || (angle > 272 && angle <= 315))
            {
                transform.rotation *= Quaternion.AngleAxis(-forcee, Vector3.forward);
            }
            else if ((angle > 315 && angle < 358))
            {
                transform.rotation *= Quaternion.AngleAxis(-forcee, Vector3.forward);
            }
            else if (angle >= 362)
            {
                transform.rotation *= Quaternion.AngleAxis(-forcee, Vector3.forward);
            }
        }
    }

}

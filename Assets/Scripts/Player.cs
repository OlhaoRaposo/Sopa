using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController cc;
    public Transform groundCheck;
    
    //Private Var
    private float groundDistance = 0.06f;
    private float jumpHeight;
    private bool isGround;
    private float gravity = -19.20f;
    private float speed = 2;
    private bool isWalking;
    private bool IsCoroutineRunning = false;
    private bool isCheck;
    private bool isRuning = false;
    private bool stamina = true;
    private bool canRun = true;
    Vector3 velocity;
    void Update()
    {
        isGround = Physics.CheckSphere(groundCheck.position, groundDistance);
        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        cc.Move(move * speed * Time.deltaTime);


        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);

        if (Input.anyKey)
        {
            isWalking = true;
        }
        else
            isWalking = false;

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && stamina == true)
        {
            if (!isRuning)
                speed += 4;
            canRun = false;
            stamina = false;
            Invoke("StaminaMarker", 6f);
            isRuning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (isRuning)
                speed = 2;
            isRuning = false;

        }
    }
    
    void StaminaMarker()
    {
        speed = 2;

        if (stamina == false && canRun == false)
        {
            canRun = true;
            Invoke("StaminaMarker", 5f);
        }
        if (canRun == true)
        {
            stamina = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;
    private float ySpeed;
    public float sneakingSpeed;
    private CharacterController conn;
    public bool isGrounded;
    private int jumpCount; 


    // Start is called before the first frame update
    void Start()
    {
        conn = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalMove, 0, verticalMove);
        //moveDirection.Normalize();
        //float magnitude = moveDirection.magnitude;
        //magnitude = Mathf.Clamp01(magnitude);
        //transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
        //conn.SimpleMove(moveDirection * magnitude * speed);

        ySpeed += Physics.gravity.y * Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            ySpeed = -0.5f;
            isGrounded = false;
        }

        Vector3 vel = moveDirection;
        vel.y = ySpeed;
        //transform.Translate(vel * Time.deltaTime);
        conn.Move(vel * Time.deltaTime * speed);

        if(conn.isGrounded)
        {
            ySpeed = -0.5f;
            isGrounded = true;

            if(Input.GetButtonDown ("Jump"))
            {
                ySpeed = jumpSpeed;
                isGrounded = false;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 1f;
        }
        else
        {
            speed = 10;
        } // Wolniejsze chodzenie pod lewym shiftem



        if(moveDirection != Vector3.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
        }

        if (conn.isGrounded)
    {
        ySpeed = -0.5f;
        isGrounded = true;
        jumpCount = 0; // Zeruj liczbę wykonanych skoków, gdy postać jest na ziemi

        if (Input.GetButtonDown("Jump"))
        {
            ySpeed = jumpSpeed;
            isGrounded = false;
            jumpCount++; // Zwiększ liczbę wykonanych skoków po wykonaniu skoku
        }
    }
    else
    {
        if (Input.GetButtonDown("Jump") && jumpCount < 2) // Sprawdź czy zostały dostępne skoki
        {
            ySpeed = jumpSpeed;
            jumpCount++; // Zwiększ liczbę wykonanych skoków po wykonaniu skoku
        }
    }

    // ...
    }
}



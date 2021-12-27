using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    [SerializeField]
    private float  steerInput, accerlation, accerlationBack, decerlation, maxSpeed, backSpeed, boostSpeed;
    
    private float steerSpeed, speed;
    private bool isSpeed;
    private bool isBoosting = false;



    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CarMove();
        CarSteer();
        
    }

    void CarSteer()
    {
        steerSpeed = Input.GetAxis("Horizontal") * steerInput * Time.deltaTime;
        if (speed > 0.8f || speed < 0) 
        {
            transform.Rotate(0, 0, -steerSpeed);
        } else if(speed != 0)
        {
            transform.Rotate(0, 0, -steerSpeed/2);
        }
        

    }

    private void CarMove()
    {
        if (!isBoosting) 
        {
            if ((Input.GetKey(KeyCode.W)) && (speed < maxSpeed))
            {
                speed = speed + accerlation * Time.deltaTime;
                isSpeed = true;
            }

            else if ((Input.GetKey(KeyCode.S)) && (speed > -backSpeed) && isSpeed == false)
            {
                speed = speed - accerlationBack;
            }

            else
            {
                if (speed > decerlation * Time.deltaTime)
                    speed = speed - (decerlation + 1) * Time.deltaTime;
                else if (speed < -decerlation * Time.deltaTime)
                    speed = speed + (decerlation + 1) * Time.deltaTime;
                else
                {
                    speed = 0;
                    isSpeed = false;
                }


            }
            //transform.position = new Vector2(transform.position.x , transform.position.y + speed * Time.deltaTime);
            transform.position += transform.up * Time.deltaTime * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boost")
        {
            speed = boostSpeed;
        }
        if (collision.tag == "Slow")
        {
            speed = speed/2;
        }
    }

}

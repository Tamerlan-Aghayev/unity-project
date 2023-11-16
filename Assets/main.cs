using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
    [SerializeField]
    float speed;
    float horizontal;
    float vertical;
    [SerializeField]
    float jumpHeight;
    Vector3 movement;
    bool jump;
    float impactForceMin = 1f;
    float impactForceMax = 5f;
    private CharacterController character;
    private Material material;

 

    private void Start()

    {
        jump = false;
        speed = 2;
        jumpHeight = 2;
        material = GetComponent<Renderer>().material;
        character = GetComponent<CharacterController>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Jump();


    }
    private void FixedUpdate()
    {
        movement.x = horizontal*speed*Time.fixedDeltaTime;
        movement.z=vertical*speed*Time.fixedDeltaTime;
        if (jump)
        {
            movement.y = jumpHeight;
        }
        else
        {
            movement.y = -9.81f*Time.fixedDeltaTime;
        }
        character.Move(movement);

    }

 

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space)&&character.isGrounded)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("red"))
        {
            material.color = Color.red;
            speed = speed / 2f;

        }
        if (other.gameObject.CompareTag("blue"))
        {
            material.color = Color.blue;
            jumpHeight *= 2f;

        }
        if (other.gameObject.CompareTag("black"))
        {
            material.color = Color.black;
            jumpHeight = 0;
        }
        if (other.gameObject.CompareTag("green"))
        {
            material.color = Color.green;
            speed = speed * 2f;
        }
        if (other.gameObject.CompareTag("yellow"))
        {
            material.color = Color.yellow;

            speed = speed * (-1f);
        }

        Destroy(other.gameObject);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.name == "white")
        {
            hit.gameObject.GetComponent<Rigidbody>().AddForce(GetRandomForce());
        }
    }


    private Vector3 GetRandomForce()
    {
        float randomForceX = Random.Range(impactForceMin, impactForceMax);
        float randomForceY = Random.Range(impactForceMin, impactForceMax);
        float randomForceZ = Random.Range(impactForceMin, impactForceMax);

        return new Vector3(randomForceX, randomForceY, randomForceZ);
    }
}

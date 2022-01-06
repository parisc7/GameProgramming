using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementScrpt : MonoBehaviour
{
    public float MovementSpeed;
    public float jumpForce;

    Rigidbody2D rigBod;

    void Start()
    {
        rigBod = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        //movement
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        //jumping
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rigBod.velocity.y)< 0.001f)
        {
            rigBod.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
}
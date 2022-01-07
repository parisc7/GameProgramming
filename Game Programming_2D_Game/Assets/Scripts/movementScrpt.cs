using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementScrpt : MonoBehaviour
{
    public float MovementSpeed;
    public float jumpForce;


    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        //movement
        var movementHorizontal = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movementHorizontal, 0, 0) * Time.deltaTime * MovementSpeed;

        //jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) && Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

}
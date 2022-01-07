using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float MovementSpeed;
	public float jumpForce;
	//public Text messageText;


	private Rigidbody2D rb;

	
	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		//messageText.text = "";
	}

	void Update ()
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

	/*void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			
			other.gameObject.SetActive (false);
			count++;
			SetCountText ();
			if(count >= 8)
				winText.text = "You Win!";
		}
	}*/


}
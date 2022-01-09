﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float MovementSpeed;
	public float jumpForce;
	public Text countText;
	public GameObject completeLevelUI;
	public int maxHealth = 3;
	public int currentHealth;
	public HealthBar healthBar;
	public GameObject RespawnUI;

	private Rigidbody2D rb;
	private int count;
	private AudioSource asr;


	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		asr = GetComponent<AudioSource>();
		count = 0;
		SetCountText();
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
	}

	public void CompleteLevel()
	{
		completeLevelUI.SetActive(true);
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		healthBar.SetHealth(currentHealth);

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

	private void Flip()
	{
		

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag ("Coin"))
		{
			other.gameObject.SetActive (false);
			asr.Play();
			count = count + 100;
			SetCountText ();
			
			
			//if(count >= 8)
			//	winText.text = "You Win!";
		}
		else if (other.gameObject.CompareTag("Enemy"))
		{ 
			//asr.Play();
			//count = count - 500;
			TakeDamage(1);
			//SetCountText();
		}
		else if (other.gameObject.CompareTag("End"))
		{
			other.gameObject.SetActive(false);
			CompleteLevel();
		}
	}

	void SetCountText()
	{
		countText.text = "Score: " + count.ToString();

	}


	void Death()
	{
		currentHealth = 0;
		//isDead = true;
		Debug.Log("Player is Dead");
		StartCoroutine(RestartLevel());

	}


	IEnumerator RestartLevel()
	{
		//playerAudio.clip = deathClip;
		//playerAudio.Play();
		yield return new WaitForSeconds(2);
		Respawn();
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene(1);
	}


	public void Respawn()
	{
		RespawnUI.SetActive(true);
	}

	IEnumerator WinState()
	{
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene(2);
	}

}
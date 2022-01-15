using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour 
{
	//used for movement(in void Update()):
	public float MovementSpeed;
	public float jumpForce;
	private Rigidbody2D rb;

	public Text countText;
	public GameObject CompleteLevelUI;
	public int maxHealth = 3;
	public int currentHealth;
	public HealthBar healthBar;
	public GameObject RespawnUI;

	
	private int count;
	private AudioSource asr;

	bool isDead;                                                // Whether the player is dead.  
	bool damaged;                                              // True when the player gets damaged.

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		asr = GetComponent<AudioSource>();
		count = 0;
		//SetCountText();
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
	}

	public void CompleteLevel()
	{
		CompleteLevelUI.SetActive(true);
	}

	public void TakeDamage(int damage)
	{
		if (currentHealth > 0)
		{
			if (damage >= currentHealth)
			{
				damaged = true;
				Death();
				isDead = true;
			}
			else
			{
				damaged = true;
				currentHealth -= damage;
				healthBar.SetHealth(currentHealth);
			}
		}
	}

	void Update ()
	{
		if (currentHealth < 0)
		{
			currentHealth = 0;
		}

		//movement
		var movementHorizontal = Input.GetAxis("Horizontal");
		transform.position += new Vector3(movementHorizontal, 0, 0) * Time.deltaTime * MovementSpeed;

		//rotation
		if (!Mathf.Approximately(0, movementHorizontal))
		{
			transform.rotation = movementHorizontal > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
		}

		//jumping
		if (Input.GetKeyDown(KeyCode.W) && Mathf.Abs(rb.velocity.y) < 0.001f)
		{
			rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag ("Coin"))
		{
			other.gameObject.SetActive (false);
			asr.Play();
			count = count + 100;
			SetCountText ();
		}
		else if (other.gameObject.CompareTag("Enemy"))
		{ 
			//asr.Play();
			count = count - 100;
			TakeDamage(1);
			SetCountText();
		}
		else if (other.gameObject.CompareTag("FinishLevel"))
		{
			other.gameObject.SetActive(false);
			CompleteLevel();
		}
		else if (other.gameObject.CompareTag("GameWon"))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}

	void SetCountText()
	{
		countText.text = "Score: " + count.ToString();
	}


	void Death()
	{
		{ 
			currentHealth = 0;
			isDead = true;
			Debug.Log("Player is Dead");
			StartCoroutine(RestartLevel());
		}
	}


	IEnumerator RestartLevel()
	{
		//playerAudio.clip = deathClip;
		//playerAudio.Play();
		yield return new WaitForSeconds(1);
		Respawn();
	}


	public void Respawn()
	{
		RespawnUI.SetActive(true);
	}

}

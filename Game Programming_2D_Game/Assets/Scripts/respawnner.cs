using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class respawnner : MonoBehaviour
{
    public GameObject Player;
    public Transform SpawnPoint;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag =="Player")
        {
            //PlayerController.TakeDamage(1);
            Player.transform.position = SpawnPoint.transform.position;
        }
    }
}

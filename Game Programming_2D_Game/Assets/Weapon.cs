﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public int damage = 40;
    public GameObject impactEffect;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

        if(hitInfo)
        {
            Debug.Log(hitInfo.transform.name);
            //Enemy enemy = hitInfo.tranform.GetComponent<Enemy>();
          //  if (enemy != null)
          //  {
          //      enemy.TakeDamage();
          //  }

            Instantiate(impactEffect, hitInfo.point, Quaternion.identity);
        }
    }
}

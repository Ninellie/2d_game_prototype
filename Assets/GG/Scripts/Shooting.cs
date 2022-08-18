using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Diagnostics;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 200f;
    public float fireRate = 10f;
    public Vector2 mousePosition;
    public Vector2 direction;

    //Is the fire button pressed
    public bool isFire;

    public Stopwatch stopwatch = new();

    public float GetTotalSeconds()
    {
        TimeSpan ts = stopwatch.Elapsed;
        return (float)ts.TotalSeconds;
    }
    // Start is called before the first frame update
    void Start()
    {
        isFire = false;
        stopwatch.Start();
    }
    //If the reload has passed, then the shot is fired 
    public void Shoot()
    {
        if (GetTotalSeconds() >= (1f / fireRate))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            direction = Lib2DMethods.DirectionToTarget(mousePosition, firePoint.position);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb2D = bullet.GetComponent<Rigidbody2D>();
            rb2D.AddForce(direction.normalized * bulletForce, ForceMode2D.Impulse);
            stopwatch.Restart();
        }
    }
    public void OnFire()
    {
        isFire = true;
    }
    public void OnFireOff()
    {
        isFire = false;
    }
    private void Update()
    {
        //Shoot if the fire button is pressed
        if (isFire)
        {
            Shoot();
        }
    }
}

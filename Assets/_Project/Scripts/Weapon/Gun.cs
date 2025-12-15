using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject BulletPrefab;
    [SerializeField] float fireRate = 0.5f;

    private float _lastShootTime;

    private void Update()
    {
        if (Time.time - _lastShootTime > fireRate)
        {
            _lastShootTime = Time.time;
            Shoot();
        }
    }

    public void Shoot()
    {

    }
}
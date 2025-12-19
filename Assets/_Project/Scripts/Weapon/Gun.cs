using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject spawnPoint;

    protected override void Shoot()
    {
        GameObject Target = FindNearestEnemy();
        if (Target == null) return;

        Vector2 targetPos = Target.GetComponent<Rigidbody2D>().position;
        Vector2 muzzlePos = spawnPoint.transform.position;
        Vector2 direction = (targetPos - muzzlePos);
       
        //Vector2 spawnPosition = new Vector2(spawnPoint.transform.position.x,spawnPoint.transform.position.y) + direction;

        float spawnOffset = 0.5f;
        Vector2 spawnPosition = muzzlePos + direction * spawnOffset;

        GameObject cloneBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        if (bulletPrefab != null)
        {
            cloneBullet.gameObject.GetComponent<Bullet>().Shoot(direction);
        }
        else
        {
            Debug.Log("Non hai assegnato il Prefab del Bullet !!!");
            return;
        }

    }

}
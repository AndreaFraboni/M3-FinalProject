using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float fireRate = 0.5f;
    [SerializeField] protected float fireRange = 6.0f;

    protected EnemiesManager _enemiesRegister;

    protected float _lastShootTime;

    protected virtual void Awake()
    {
        if (_enemiesRegister == null)
        {
            _enemiesRegister = FindObjectOfType<EnemiesManager>();
            if (_enemiesRegister == null)
            {
                Debug.LogError("EnemiesManager NON è in scena !!!");
            }
        }
    }

    protected virtual void Shoot()
    {

    }

    protected virtual void Update()
    {
        if (Time.time - _lastShootTime > fireRate)
        {
            _lastShootTime = Time.time;
            Shoot();
        }
    }

    protected GameObject FindNearestEnemy()
    {
        GameObject NearstEnemyFounded = null;

        float nearstDistance = fireRange;

        foreach (EnemyController currentEnemy in _enemiesRegister.listEnemies)
        {
            float CurDistance = Vector2.Distance(transform.position, currentEnemy.transform.position);
            if (CurDistance < nearstDistance)
            {
                nearstDistance = CurDistance;
                NearstEnemyFounded = currentEnemy.gameObject;
            }
        }
        return NearstEnemyFounded;
    }



}

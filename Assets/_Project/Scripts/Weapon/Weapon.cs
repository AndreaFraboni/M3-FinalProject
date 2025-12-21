using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float _fireRate = 0.5f;
    [SerializeField] protected float _fireRange = 6.0f;
    [SerializeField] public string _weaponId = "Set Weapon Id here !!!";

    protected EnemiesManager _enemiesRegister;

    protected float _lastShootTime;

    [SerializeField] protected float _fireRateUpValue = 0.1f;
    [SerializeField] protected float _fireRangeUpValue = 0.5f;

    // setter    
    private void SetFireRate(float amount) => _fireRate = _fireRate - amount;
    private void SetFireRange(float amount) => _fireRange = _fireRange + amount;

    protected virtual void Awake()
    {
        if (_enemiesRegister == null)
        {
            _enemiesRegister = FindObjectOfType<EnemiesManager>();
            if (_enemiesRegister == null)
            {
                Debug.LogError("EnemiesManager NON sta in scena !!!");
            }
        }
    }

    protected virtual void Shoot()
    {

    }

    protected void Update()
    {
        if (Time.time - _lastShootTime > _fireRate)
        {
            _lastShootTime = Time.time;
            Shoot();
        }
    }

    protected GameObject FindNearestEnemy()
    {
        GameObject NearstEnemyFounded = null;

        float nearstDistance = _fireRange;

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

    public void UpdateFireParams()
    {
        SetFireRate(_fireRateUpValue);
        SetFireRange(_fireRangeUpValue);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int _currenthp = 100;
    [SerializeField] private int _maxHP = 100;
    [SerializeField] private int _lives = 3;

    private EnemyController _enemyController;
    private PlayerController _playerController;

    private void Awake()
    {
        _enemyController = GetComponentInParent<EnemyController>();
        _playerController = GetComponentInParent<PlayerController>();

        if (_enemyController == null && _playerController== null)
        {
            Debug.Log("LifeController: PROBLEMA !!!! NON TROVO nè PlayerController nè EnemyContoller !!!");
            return;
        }
    }


    // Getter
    public int GetHp() => _currenthp;
    public int GetMaxHp() => _maxHP;
    public int GetLives() => _lives;

    // Setter
    public int SetLives(int lives) => _lives = lives;
    public void SetHp(int hp)
    {
        hp = Mathf.Clamp(hp, 0, _maxHP);

        if (hp != _currenthp)
        {
            _currenthp = hp;

            if (_currenthp == 0)
            {
                _lives--;

                if (_lives == 0)
                {
                    Defeated();
                }
                else
                {
                    _currenthp = 100;
                }
            }
        }
    }

    // Functions
    public void AddHp(int amount) => SetHp(_currenthp + amount);

    public void TakeDamage(int damage)
    {
        AddHp(-damage);
    }

    public void TakeHealth(int amount)
    {
        AddHp(amount);
    }

    private void Defeated()
    {
        if (CompareTag("Enemy"))
        {            
            if (_enemyController != null) _enemyController.EnemyDeath();
            return;
        }

        if (CompareTag("Player"))
        {            
            if (_playerController != null) _playerController.PlayerDeath();
            return;
        }
        //Destroy(gameObject);
    }
}


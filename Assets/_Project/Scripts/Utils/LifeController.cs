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
        if (TryGetComponent<PlayerController>(out _playerController)) // if (objectToCheck.TryGetComponent<HingeJoint>(out HingeJoint hinge))
        {
            return;
        }
        else if (TryGetComponent<EnemyController>(out _enemyController))
        {
            return;
        }
        else
        {
            Debug.LogError("LifeController è montato su un oggetto che non è un Player o un Enemy !!!");
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
        if (this.CompareTag(Tags.Enemy))
        {
            if (_enemyController != null) _enemyController.EnemyDeath();
            return;
        }

        if (this.CompareTag(Tags.Player))
        {
            if (_playerController != null) _playerController.PlayerDeath();
            return;
        }
    }
}


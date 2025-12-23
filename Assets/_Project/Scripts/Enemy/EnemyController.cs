using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private AudioClip DeathSound;

    [SerializeField] private GameObject gameObjectToSpawn;

    private Rigidbody2D _rb;
    private CircleCollider2D _Collider2D;
    private EnemiesManager _enemiesManager;
    private EnemyAnimation _enemyAnimation;

    private bool isAlive = true;

    private AudioSource _audioSource;

    private Vector2 direction;

    // Getter
    public Vector2 GetDirection() => direction;

    private void Awake()
    {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        _Collider2D = GetComponent<CircleCollider2D>();

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (_target == null)
        {
            GameObject go = GameObject.FindGameObjectWithTag(Tags.Player);
            if (go != null)
            {
                _target = go.transform;
            }
        }
        _enemiesManager = FindObjectOfType<EnemiesManager>(); // prendo riferimento all'Enemies Manager in scena
        if (!_enemiesManager) Debug.LogError("TI SEI DIMENTICATO L'EnemiesManager IN SCENA !!!!");

        _enemyAnimation = GetComponentInChildren<EnemyAnimation>();
        if (!_enemyAnimation) Debug.LogError("NON HO TROVATO IL COMPONENTE EnemyAnimation !!!!");

    }

    private void OnEnable()
    {
        if (_enemiesManager != null)
        {
            _enemiesManager.RegistEnemy(this);
        }
        else
        {
            Debug.LogError("Manca l'EnemiesManager in scena!");
        }
    }

    private void Update()
    {
        if (_target == null)
        {
            direction = Vector2.zero;
            return;
        }

        // Calcoliamo direzione verso il target che è il Player !!!
        Vector2 targetPosition = _target.position;
        direction = (targetPosition - _rb.position).normalized;
    }

    private void FixedUpdate()
    {
        if (isAlive) EnemyMovement();
    }

    void EnemyMovement()
    {
        _rb.MovePosition(_rb.position + direction * (_speed * Time.deltaTime));
    }

    private void OnDisable()
    {
        if (_enemiesManager != null)
        {
            _enemiesManager.RemoveEnemy(this);
        }
    }

    public void EnemyDeath()
    {
        isAlive = false;

        if (DeathSound != null)
        {
            AudioSource.PlayClipAtPoint(DeathSound, transform.position);
        }

        if (_Collider2D != null) _Collider2D.enabled = false;
        if (_rb != null) _rb.simulated = false;

        _enemyAnimation.SetBoolParam("isDying", true);
    }

    public void DestroyGOEnemy()
    {
        if (gameObjectToSpawn != null)
        {
            Instantiate(gameObjectToSpawn, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag(Tags.Player))
            {
                _target.gameObject.GetComponent<PlayerController>().PlayerDeath();
                DestroyGOEnemy();
            }
        }
    }

}






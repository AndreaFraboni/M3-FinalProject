using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 2.0f;

    private Rigidbody2D _rb;
    private CircleCollider2D _Collider2D;
    private EnemiesManager _enemiesManager;
    private EnemyAnimation _enemyAnimation;

    private bool isAlive = true;

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

        if (_target == null)
        {
            GameObject go = GameObject.FindGameObjectWithTag(Tags.Player);
            if (go != null)
            {
                _target = go.transform;
            }
        }
        _enemiesManager = FindObjectOfType<EnemiesManager>();
        _enemyAnimation = FindObjectOfType<EnemyAnimation>();
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
            _speed = 0;
            return;
        }

        // Calcoliamo direzione verso il target !
        Vector2 targetPosition = _target.position;
        direction = (targetPosition - _rb.position).normalized;
        if (direction.sqrMagnitude > 1f) direction = direction.normalized;
    }

    private void FixedUpdate()
    {
        EnemyMovement();
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

        if (_Collider2D != null) _Collider2D.enabled = false;
        if (_rb != null) _rb.simulated = false;

        _enemyAnimation.SetBoolParam("isDying", true);

        //Destroy(gameObject);
    }

    public void DestroyGOEnemy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag(Tags.Player))
            {
                //_target.gameObject.GetComponent<PlayerController>().DestroyGOPlayer();
                _target.gameObject.GetComponent<PlayerController>().PlayerDeath();
                DestroyGOEnemy();
            }
        }
    }

}






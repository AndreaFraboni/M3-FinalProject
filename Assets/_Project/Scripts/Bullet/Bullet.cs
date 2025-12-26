using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage = 50;
    [SerializeField] private float _lifeSpan = 5f;

    public AudioSource _audioSource;
    public AudioClip impactSound;

    private Rigidbody2D _rb;

    public float _speed = 10f;
    private Vector2 _direction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start()
    {
        Destroy(gameObject, _lifeSpan); // autodistruzione dopo un tempo pari a _lifeSpan
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _direction * (_speed * Time.deltaTime));
    }

    public void Shoot(Vector2 dir)
    {
        float sqrMagnitude = dir.sqrMagnitude;
        if (sqrMagnitude > 1)
        {
            dir /= Mathf.Sqrt(sqrMagnitude);
        }
        _direction = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(Tags.Enemy))
        {
            if (impactSound != null)
            {
                AudioSource.PlayClipAtPoint(impactSound, transform.position, _audioSource.volume);
            }

            if (!collision.gameObject.CompareTag(Tags.TriggerGame))
            {
                Destroy(gameObject);
                return;
            }
        }

        if (collision.gameObject.TryGetComponent<LifeController>(out LifeController _LifeController)) // ref : if (objectToCheck.TryGetComponent<HingeJoint>(out HingeJoint hinge))
        {
            if (impactSound != null)
            {
                AudioSource.PlayClipAtPoint(impactSound, transform.position, _audioSource.volume);
            }
            _LifeController.TakeDamage(_damage);
            Destroy(gameObject);
        }
        else
        {
            if (impactSound != null)
            {
                AudioSource.PlayClipAtPoint(impactSound, transform.position, _audioSource.volume);
            }
            if (!collision.gameObject.CompareTag(Tags.TriggerGame))
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage = 50;
    [SerializeField] private float _lifeSpan = 5f;

    private Rigidbody2D _rb;

    public float _speed = 10f;
    private Vector2 _direction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag(Tags.Enemy)) // se l'oggetto colpito è un nemico lo devo danneggiare !!!
            {
                if (collision.gameObject.TryGetComponent<LifeController>(out LifeController _LifeController)) // ref : if (objectToCheck.TryGetComponent<HingeJoint>(out HingeJoint hinge))
                {
                    _LifeController.TakeDamage(_damage);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.LogError("ho avuto una collisione con qualcosa in cui Manca LifeController !!!");
                    return;
                }                   
            }
            else
            {
                Destroy(gameObject); // il proiettile si distrugge comunque quando impatta con qualsiasi oggetto in scena !
            }
        }
    }



}

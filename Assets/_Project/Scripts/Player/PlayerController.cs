using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;

    private float horizontal, vertical;

    private Vector2 direction;

    private Rigidbody2D _rb;
    private CircleCollider2D _Collider2D;

    private bool isAlive = true;

    // Getter
    public Vector2 GetDirection() => direction;

    private void Awake()
    {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        _Collider2D = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void CheckInput()
    {
        if (isAlive)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            direction = new Vector2(horizontal, vertical);

            if (direction.sqrMagnitude > 1f) direction = direction.normalized;
        }
    }

    void Move()
    {
        _rb.MovePosition(_rb.position + direction * (speed * Time.deltaTime));
    }

    public void DestroyGOPlayer()
    {
        Destroy(gameObject);
    }

    public void PlayerDeath()
    {
        isAlive = false;

        if (_Collider2D != null) _Collider2D.enabled = false;
        if (_rb != null) _rb.simulated = false;

        //_animParam.SetBoolParam("isDying", true);

        //Destroy(gameObject); // si distrugge il gameobject alla fine dell'animazione della morte con un Evento registrato all'ultimo frame dell'animazione della morte
    }

}

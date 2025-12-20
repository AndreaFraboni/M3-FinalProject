using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private Transform _weaponMountPoint;

    private float horizontal, vertical;

    private Vector2 direction;

    private Rigidbody2D _rb;
    private CircleCollider2D _Collider2D;
    private PlayerAnimation _PlayerAnimation;

    private Weapon _currentWeapon;
    private GameObject _gameObjectWeapon;

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

        _PlayerAnimation = GetComponent<PlayerAnimation>();
        if (!_PlayerAnimation) Debug.LogError("NON HO TROVATO IL COMPONENTE PlayerAnimation !!!!");
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
        _rb.MovePosition(_rb.position + direction * (_speed * Time.deltaTime));
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

        _PlayerAnimation.SetBoolParam("isDying", true);
        //Destroy(gameObject); // si distrugge il gameobject alla fine dell'animazione della morte con un Evento registrato all'ultimo frame dell'animazione della morte
    }

    public void MountWeapon(GameObject _weaponPrefab)
    {
        if (_weaponPrefab == null)
        {
            Debug.LogError("Errore la weaponPrefab risulta essere null !!!!");
            return;
        }

        Weapon _weapon = _weaponPrefab.GetComponent<Weapon>();
        if (_weapon == null)
        {
            Debug.LogError("Il weaponPrefab del pickup NON risulta essere una Weapon !!!!");
            return;
        }

        if (_currentWeapon != null && _currentWeapon.weaponId == _weapon.weaponId)
        {
            Debug.Log("Stiamo montando la stessa arma che abbiamo adesso !!!!");
            return;
        }
        else
        {
            if (_gameObjectWeapon != null) Destroy(_gameObjectWeapon);

            _gameObjectWeapon = Instantiate(_weaponPrefab);
            _gameObjectWeapon.transform.SetParent(_weaponMountPoint, false);

            _currentWeapon = _gameObjectWeapon.GetComponent<Weapon>();
        }
    }

}

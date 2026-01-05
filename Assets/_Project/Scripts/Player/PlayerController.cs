using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private Transform _weaponMountPoint;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip itemPickUpSound;
    [SerializeField] private AudioClip weaponPickUpSound;
    [SerializeField] private AudioClip coinPickUpSound;
    //[SerializeField] GameObject _initialWeaponPrefab;
    [SerializeField] int Coins = 0;

    private float horizontal, vertical;
    private Vector2 direction;

    private Rigidbody2D _rb;
    private CircleCollider2D _collider2D;
    private PlayerAnimation _playerAnimation;
    private Weapon _currentWeapon;
    private GameObject _gameObjectWeapon;
    private AudioSource _audioSource;

    private bool isAlive = true;

    // Getter
    public Vector2 GetDirection() => direction;

    private void Awake()
    {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        _collider2D = GetComponent<CircleCollider2D>();
        if (_collider2D == null) Debug.LogError("Non trovo il COLLIDER 2D !!!!!");

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        _playerAnimation = GetComponent<PlayerAnimation>();
        if (!_playerAnimation) Debug.LogError("NON HO TROVATO IL COMPONENTE PlayerAnimation !!!!");

        //if (_initialWeaponPrefab) MountWeapon(_initialWeaponPrefab); Player start without weapon !
    }

    private void Update()
    {
        if (isAlive) CheckInput();
    }

    private void FixedUpdate()
    {
        if (isAlive) Move();
    }

    void CheckInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector2(horizontal, vertical);

        if (direction.sqrMagnitude > 1f) direction = direction.normalized;
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

        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }

        if (_collider2D != null) _collider2D.enabled = false;
        if (_rb != null) _rb.simulated = false;

        _playerAnimation.SetBoolParam("isDying", true);
    }

    public void GetCoins()
    {
        if (coinPickUpSound != null)
        {
            _audioSource.clip = coinPickUpSound;
            _audioSource.Play();
        }
        Coins++;
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

        if (_currentWeapon != null && _currentWeapon._weaponId == _weapon._weaponId)
        {
            //Debug.Log("Stiamo montando la stessa arma che abbiamo adesso !!!!");
            if (itemPickUpSound != null)
            {
                _audioSource.clip = itemPickUpSound;
                _audioSource.Play();
            }
            _currentWeapon.UpdateFireParams();
            return;
        }
        else
        {
            if (weaponPickUpSound != null)
            {
                _audioSource.clip = weaponPickUpSound;
                _audioSource.Play();
            }

            if (_gameObjectWeapon != null) Destroy(_gameObjectWeapon);

            _gameObjectWeapon = Instantiate(_weaponPrefab);
            _gameObjectWeapon.transform.SetParent(_weaponMountPoint, false);

            _currentWeapon = _gameObjectWeapon.GetComponent<Weapon>();
        }
    }
}

using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum itemType
    {
        WEAPON,
        COIN
    }

    [SerializeField] GameObject _weaponPrefab;
    [SerializeField] GameObject _coinPrefab;

    [SerializeField] itemType typeItem;

    private PlayerController _playerController;

    private SpriteRenderer _spriteRenderer;
    private Animator _anim;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (!_spriteRenderer)
        {
            Debug.Log("NON HO TROVATO IL COMPONENTE SPRITE RENDERER !!!");
        }

        _anim = GetComponent<Animator>();
        if (!_anim)
        {
            Debug.Log("NON HO TROVATO L'Animator Controller !!!");
        }

    }

    private void OnEnable()
    {
        switch (typeItem)
        {
            case itemType.WEAPON:

                _spriteRenderer.sprite = _weaponPrefab.GetComponent<SpriteRenderer>().sprite;

                break;
            case itemType.COIN:

                _spriteRenderer.sprite = _coinPrefab.GetComponent<SpriteRenderer>().sprite;
                Animator _prefabAnim = _coinPrefab.GetComponent<Animator>();
                _anim.runtimeAnimatorController = _prefabAnim.runtimeAnimatorController;

                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return; // oggetto non valido

        if (!collision.CompareTag(Tags.Player)) return; // non è un player

        if (!collision.TryGetComponent<PlayerController>(out _playerController))
        {
            Debug.LogError("NON trovo il componente PlayerController sul Player !!!");
            return;
        }

        switch (typeItem)
        {
            case itemType.WEAPON:

                _playerController.MountWeapon(_weaponPrefab);
                Destroy(gameObject);

                break;
            case itemType.COIN:

                _playerController.GetCoins();
                Destroy(gameObject);

                break;

        }

    }
}

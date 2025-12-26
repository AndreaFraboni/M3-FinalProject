using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum itemType
    {
        WEAPON,
        COIN
    }

    [SerializeField] GameObject weaponPrefab;
    [SerializeField] GameObject coinPrefab;

    [SerializeField] itemType typeItem;

    private PlayerController _playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return; // oggetto non valido

        if (!collision.CompareTag(Tags.Player)) return; // non è un player

        switch (typeItem)
        {
            case itemType.WEAPON:

                if (collision.TryGetComponent<PlayerController>(out _playerController))
                {
                    _playerController.MountWeapon(weaponPrefab);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.LogError("Stavo per procedere al montaggio dell'arma ma NON trovo il componente PlayerController sul Player !!!");
                }

                break;
            case itemType.COIN:
                _playerController.GetCoins(coinPrefab);
                Destroy(gameObject);
                break;

        }


    }
}

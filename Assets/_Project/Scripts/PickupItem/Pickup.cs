using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;

    private PlayerController _playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return; // oggetto non valido

        if (!collision.CompareTag(Tags.Player)) return; // non è un player

        if (collision.TryGetComponent<PlayerController>(out _playerController))
        {
            _playerController.MountWeapon(weaponPrefab);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Stavo per procedere al montaggio dell'arma ma NON trovo il componente PlayerController sul Player !!!");
        }
    }
}

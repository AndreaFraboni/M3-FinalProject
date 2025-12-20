using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;

    private PlayerController _pc;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(Tags.Player)) return;

        _pc = collision.GetComponent<PlayerController>();

        if (_pc != null)
        {
            _pc.MountWeapon(weaponPrefab);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Stavo per procedere al montaggio dell'arma ma NON trovo il componente PlayerController sul Player !!!");
        }
    }
}

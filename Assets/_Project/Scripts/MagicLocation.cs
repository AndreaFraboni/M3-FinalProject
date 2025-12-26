using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Pickup;

public class MagicLocation : MonoBehaviour
{
    [SerializeField] private AudioClip _magicLocationSound;
    [SerializeField] private bool _areaActivated = false;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return; // oggetto non valido

        if (!collision.CompareTag(Tags.Player)) return; // non è un player

        if (!_areaActivated)
        {
            _areaActivated = true;
            if (_audioSource != null)
            {
                _audioSource.clip = _magicLocationSound;
                _audioSource.Play();
            }
        }
    }

}

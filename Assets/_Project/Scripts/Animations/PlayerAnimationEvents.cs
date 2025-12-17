using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private PlayerController _pc;

    private void Awake()
    {
        if (_pc == null) _pc = GetComponentInParent<PlayerController>();
    }

    public void DestroygameObject(string _state)
    {
        if (_state == "destroy") _pc.DestroyGOPlayer();
    }
}

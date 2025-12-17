using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    [SerializeField] private EnemyController _ec;

    private void Awake()
    {
        if (_ec == null) _ec = GetComponentInParent<EnemyController>();
    }

    public void DestroygameObject(string _state)
    {
        if (_state == "destroy") _ec.DestroyGOEnemy();
    }
}

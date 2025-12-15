using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private string _verticalSpeedParamName = "vSpeed";
    [SerializeField] private string _horizontalSpeedParamName = "hSpeed";

    private Animator _anim;
    private EnemyController _enemy;

    private bool isWalking = false;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _enemy = GetComponentInChildren<EnemyController>();
    }

    private void SetVerticalSpeedParam(float diry)
    {
        _anim.SetFloat(_verticalSpeedParamName, diry);
    }

    private void SetHorizontalSpeedParam(float dirx)
    {
        _anim.SetFloat(_horizontalSpeedParamName, dirx);
    }

    private void SetDirectionalSpeedParams(Vector2 direction)
    {
        SetHorizontalSpeedParam(direction.x);
        SetVerticalSpeedParam(direction.y);
    }
    private void SetBoolParam(string stateParam, bool value)
    {
        _anim.SetBool(stateParam, value);
    }

    private void Update()
    {
        Vector2 direction = _enemy.GetDirection();

        isWalking = direction != Vector2.zero;

        SetBoolParam("isWalking", isWalking);

        if (isWalking)
        {
            SetDirectionalSpeedParams(direction);
        }

    }

}

using UnityEngine;

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
        _enemy = GetComponentInParent<EnemyController>();
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
    public void SetBoolParam(string stateParam, bool value)
    {
        _anim.SetBool(stateParam, value);
    }

    private void Update()
    {
        if (!_enemy.isHit)
        {
            Vector2 direction = _enemy.GetDirection();
            isWalking = direction != Vector2.zero;
            SetBoolParam("isWalking", isWalking);
            if (isWalking)
            {
                SetDirectionalSpeedParams(direction);
            }
        }
        else
        {
            _enemy.isHit = false;
            _anim.SetTrigger("isHit");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    private CharacterAnimation _animation;
    private WeaponController _weapon;
    private bool _onCoolDown;

    void Start()
    {
        _weapon = GetComponentInChildren<WeaponController>();
        _weapon.gameObject.SetActive(false);
        _animation = GetComponent<CharacterAnimation>();
    }

    public void InitAttack()
    {
        if (_onCoolDown) return;
        _onCoolDown = true;
        _weapon.gameObject.SetActive(true);
        _animation.PlayAttack();
        Invoke(nameof(ResetCoolDown), 1f);
    }

    public void AttackNow()
    {
        _weapon.Attack();
    }

    private void ResetCoolDown()
    {
        _weapon.gameObject.SetActive(false);
        _onCoolDown = false;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack2 : MonoBehaviour
{

    private CharacterCollection2 _collection;
    private CharacterAnim _animation;
    [SerializeField] private WeaponController _weapon;
    private bool _onCoolDown;

    private int _userId;
        
    void Start()
    {
        _collection = GetComponent<CharacterCollection2>();
        //_weapon = GetComponentInChildren<WeaponController>();
        _weapon.gameObject.SetActive(false);
        _animation = GetComponent<CharacterAnim>();
        Invoke(nameof(GetUserId), .25f);

    }

    private void GetUserId()
    {
        _userId = _collection.UserId;
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
        _weapon.Attack(_userId);
    }

    private void ResetCoolDown()
    {
        _weapon.gameObject.SetActive(false);
        _onCoolDown = false;
    }


}

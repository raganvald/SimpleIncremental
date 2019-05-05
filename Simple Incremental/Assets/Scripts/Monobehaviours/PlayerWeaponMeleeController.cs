using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponMeleeController : MonoBehaviour
{
    public int damage = 0;
    [NonSerialized]
    public GameObject weapon = null;
    [SerializeField]
    LayerMask mask = new LayerMask();
    ContactFilter2D cf2d;
    Collider2D[] colliders;
    Animator anim;
    List<CharacterHealth> damagedCharacters = new List<CharacterHealth>();
    int meleeAttackingHash = Animator.StringToHash("MeleeAttacking");
    int meleeAttackHash = Animator.StringToHash("MeleeAttack");
    BoxCollider2D weaponCollider;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        cf2d = new ContactFilter2D();
        cf2d.layerMask = mask; 
        cf2d.useLayerMask = true;
        colliders = new Collider2D[10];
    }

    private void Update()
    {
        if (weapon == null)
            return;
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool(meleeAttackingHash, false);
        }
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
        {
            anim.SetBool(meleeAttackingHash, true);
        }
    }

    //Called from Animation so multiple hitts can occur when mouse is held down
    public void AttackClear()
    {
        damagedCharacters.Clear();
        Array.Clear(colliders, 0, colliders.Length);
    }
    //Called from Animation
    public void EnableWeapon()
    {
        weaponCollider = weapon.gameObject.GetComponent<BoxCollider2D>();
        weaponCollider.enabled = true;
        weaponCollider.OverlapCollider(cf2d, colliders);
        foreach (Collider2D col in colliders)
        {
            CharacterHealth ch = col?.GetComponent<CharacterHealth>();
            if (!damagedCharacters.Contains(ch) && ch != null)
            {
                ch?.TakeDamage(damage);
                damagedCharacters.Add(ch);
            }
        }
    }
    //Called from Animation
    public void DisableWeapon()
    {
        weaponCollider.enabled = false;
    }

}

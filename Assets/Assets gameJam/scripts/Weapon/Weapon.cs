using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{   
    [SerializeField] float offset;

    [SerializeField] GameObject attackSprite;
    [SerializeField] GameObject Effect;
    [SerializeField] Transform shotPoint;
    [SerializeField] Animator camAnim;
    
    [SerializeField] public float attackInterval;
    
    [SerializeField] public int damage;

    [SerializeField] private float knockback;

    WeaponAttackSprite attackScript; 

    private void Update()
    {
        // Handles the weapon rotation
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        attackScript = attackSprite.GetComponent<WeaponAttackSprite>();
        damage = attackScript.damage;
        knockback = attackScript.knockback;
        
    }
    public void WeaponAttack(){
        if (Effect){
            Instantiate(Effect, shotPoint.position, Quaternion.identity);
        }
        
        //camAnim.SetTrigger("shake");
        Instantiate(attackSprite, shotPoint.position, transform.rotation);
    }
}

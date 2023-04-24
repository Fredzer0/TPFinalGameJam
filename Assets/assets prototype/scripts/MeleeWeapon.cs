using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] float offset;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] int attackDamage = 1;
    [SerializeField] Transform attackPoint;
    [SerializeField] GameObject attackSpriteTest;

    private float timeBtwShots;
    [SerializeField] float startTimeBtwShots;

    public LayerMask enemyLayers;

    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Attack();
            }
        }
        else {
            timeBtwShots -= Time.deltaTime;
        }
       
    }


    void Attack()
    {
        Instantiate(attackSpriteTest, attackPoint.position, Quaternion.identity);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies){
            enemy.GetComponent<Lama>().TakeDamage(attackDamage);
        }
    }

}

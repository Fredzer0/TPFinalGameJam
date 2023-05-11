using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  
    private Rigidbody2D body;
    [SerializeField] private float detectionRange;
    [SerializeField] private float speed;
    [SerializeField] private int maxHP;

    private int currentHP;
    private string targetName = "Player";

    private Transform target;
    private bool is_argro = false;
    private int envLayer;
    private Animator anim;
    GameObject obj;

    private Vector2 playerDirection;
    private Vector2 facingDirection;

    [SerializeField] float offset;

    [SerializeField] GameObject projectile;
    [SerializeField] GameObject shotEffect;
    [SerializeField] Transform shotPoint;

    private float timeBtwShots;
    [SerializeField] float startTimeBtwShots;


    void Start()
    {
        envLayer = LayerMask.NameToLayer("Environment");
        body = GetComponent<Rigidbody2D>();
        obj  = GameObject.Find(targetName);
        if (obj != null)
        {
            target = obj.transform;
        }
        anim = GetComponent<Animator>();
        facingDirection = new Vector2();

        currentHP = maxHP;
    }

    void Update()
    {


        
         if (target != null)
        { 
            playerDirection = (target.position - transform.position).normalized;
            LayerMask mask = LayerMask.GetMask("Player", "Environment");
            var x = Physics2D.Raycast(this.transform.position, playerDirection, detectionRange, mask);

            if (x.collider != null && x.collider.gameObject.layer != envLayer)
            {
                Debug.DrawRay(this.transform.position, playerDirection * detectionRange, Color.blue);
                is_argro = true;
            }
            else
            {
                Debug.DrawRay(this.transform.position, playerDirection * detectionRange, Color.red);
                is_argro = false;
            }

            Vector3 difference =  - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        }
    }

    private void FixedUpdate()
    {
        if (is_argro)
        {
        // body.velocity = playerDirection *speed;
        
        facingDirection = playerDirection.normalized;
        anim.SetFloat("Horizontal", facingDirection.x );
        anim.SetFloat("Vertical", facingDirection.y );

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                //Instantiate(shotEffect, shotPoint.position, Quaternion.identity);
                //camAnim.SetTrigger("shake");
                Instantiate(projectile, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else {

            timeBtwShots -= Time.deltaTime;
        }



        }
        else
        {
        body.velocity = Vector2.zero;
        anim.SetFloat("Horizontal", (float)(facingDirection.x * 0.1) );
        anim.SetFloat("Vertical", (float)(facingDirection.y * 0.1) );
        }
    }


    private void OnCollisionEnter2D(Collision2D collision){
        
        //version cheap
        if (LayerMask.LayerToName(collision.gameObject.layer) == "PlayerAttack"){

            TakeDamage(1);
            
        }
    }

    public void TakeDamage(int damage){

        currentHP -= damage;
        Debug.Log("ow");
        if (currentHP < 1){
           Die();
        }
    }

    public void Die(){
         Destroy(this.gameObject);
    }

}

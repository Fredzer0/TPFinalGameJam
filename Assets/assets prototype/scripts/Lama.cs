using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lama : MonoBehaviour
{
  
    private Rigidbody2D body;
    [SerializeField] private float detectionRange;
    [SerializeField] private float speed;
    [SerializeField] private int maxHP;

    private int currentHP;
 
    private string targetName = "VillainPoulet";

    private Transform target;
    private bool is_argro = false;
    private int envLayer;
    private Animator anim;
    GameObject obj;

    private Vector2 playerDirection;
    private Vector2 facingDirection;

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
        }
    }

    private void FixedUpdate()
    {
        if (is_argro)
        {
        body.velocity = playerDirection *speed;
        
        facingDirection = playerDirection.normalized;
        anim.SetFloat("Horizontal", facingDirection.x );
        anim.SetFloat("Vertical", facingDirection.y );
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
        if (LayerMask.LayerToName(collision.gameObject.layer) == "PlayerProjectile"){
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage){

        currentHP -= damage;
        if (currentHP < 1){
           Die();
        }
    }

    public void Die(){
         Destroy(this.gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rig;
    private Collider2D col;
    private Animator anim;
    private Vector2 mouvementFleche;
    private Vector2 lastDirection;
    //private bool isDead = false;
    [SerializeField] public float vitesse;

    [SerializeField] private int maxHP = 3;
    public int currHP;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        mouvementFleche = Vector2.zero;
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        currHP = maxHP;

    }



    // Update is called once per frame
    void Update()
    {
        mouvementFleche.x = Input.GetAxisRaw("Horizontal");
        mouvementFleche.y = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButton(0))
        {
            Attack();
            
        }

    }


    void FixedUpdate()
    {
        float vel = rig.velocity.magnitude;

        if (vel >= 0.01)
        {
            lastDirection = rig.velocity;        
        }
        else if (lastDirection.magnitude > 0.1)
        {
            lastDirection = lastDirection.normalized * 0.05f;       
        }

        rig.AddForce(mouvementFleche.normalized * vitesse * rig.drag * rig.mass);
        anim.SetInteger("AnimState", 1);
        anim.SetTrigger("Grounded");
        if (vel <= 0.03){
            anim.SetInteger("AnimState", 0);
            anim.ResetTrigger("Grounded");
        }
    }

    void Attack(){

        anim.SetTrigger("Attack1");

        

    }



    void TakeDamage(int damage){

        currHP -= damage;

        //anim
        if (currHP <= 0){
            Death();
        }
    }


    public void Death(){
        Destroy(col);
        rig.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Death");
    }


}

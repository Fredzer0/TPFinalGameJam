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
    private float timeBtwAttacks;
    [SerializeField] float startTimeBtwAttacks;

    [SerializeField] private float iFrames;
    private float currIFrames;

    Weapon weaponScript;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        mouvementFleche = Vector2.zero;
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        currHP = maxHP;
        weaponScript = GetComponentInChildren<Weapon>();
    }



    // Update is called once per frame
    void Update()
    {
        mouvementFleche.x = Input.GetAxisRaw("Horizontal");
        mouvementFleche.y = Input.GetAxisRaw("Vertical");
        weaponScript = GetComponentInChildren<Weapon>();

        if (weaponScript != null){
            startTimeBtwAttacks = weaponScript.attackInterval;
    
            if (timeBtwAttacks <= 0)
            {
                if (Input.GetMouseButton(0))
                {
                    anim.SetTrigger("Sword");
                    timeBtwAttacks = startTimeBtwAttacks;
                }
            }
            else {
                timeBtwAttacks -= Time.deltaTime;
            }

       }

    }


    void FixedUpdate()
    {
        float vel = rig.velocity.magnitude;
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (vel >= 0.01)
        {
            lastDirection = rig.velocity;        
        }
        else if (lastDirection.magnitude > 0.1)
        {
            lastDirection = lastDirection.normalized * 0.05f;       
        }

        rig.AddForce(mouvementFleche.normalized * vitesse * rig.drag * rig.mass);
        anim.SetBool("Moving", true);
        anim.SetFloat("Horizontal", lastDirection.x);
        anim.SetFloat("Vertical", lastDirection.y);
        anim.SetFloat("AimHorizontal", difference.x);
        anim.SetFloat("AimVertical", difference.y);

        currIFrames -= Time.deltaTime; //tick down les iframes

        if (vel <= 0.03){
            anim.SetBool("Moving", false);
        }
    }

    void Attack(){
        
        weaponScript.WeaponAttack();
    }

    void TakeDamage(int damage){

        if (currIFrames <= 0 ){
            currHP -= damage;
            anim.SetTrigger("Damaged");
            currIFrames = iFrames; 
        }

  
        if (currHP <= 0){
            Death();
        }
    }


    public void Death(){
        Destroy(col);
        rig.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Death");
    }


    private void OnCollisionStay2D(Collision2D collision){
        if (Input.GetKeyDown(KeyCode.E)){
            if (LayerMask.LayerToName(collision.gameObject.layer) == "Weapon"){

                if (weaponScript != null){
                    Destroy(gameObject.transform.GetChild(0).gameObject);
                }
           

                collision.gameObject.transform.SetParent(transform);
                Transform weaponTransform = collision.gameObject.transform;
                weaponTransform.position = transform.position;
                weaponScript = collision.gameObject.GetComponent<Weapon>();
                weaponScript.enabled = true;
                SpriteRenderer spriteWeapon = collision.gameObject.GetComponent<SpriteRenderer>();
                spriteWeapon.enabled = false;
                Collider2D colliderWeapon = collision.gameObject.GetComponent<Collider2D>();
                colliderWeapon.enabled = false;
            }

        }
    }



}

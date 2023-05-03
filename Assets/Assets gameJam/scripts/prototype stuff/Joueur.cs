using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Joueur : MonoBehaviour
{
    private Rigidbody2D rig;
    private Collider2D col;
    private Animator anim;
    private Vector2 mouvementFleche;
    private Vector2 lastDirection;
    public float vitesse;
    private GameObject fondu;
    private bool isDead = false;
    private bool isVictoryAchive = false;

    private string currentweapon = "melee";

    [SerializeField] GameObject meleeWeapon;
    [SerializeField] GameObject rangedWeapon;
    [SerializeField] int maxHP;

    public int currHP;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        mouvementFleche = Vector2.zero;
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        fondu = GameObject.Find("fondue");
        currHP = maxHP;
    }


    void Update()
    {
        mouvementFleche.x = Input.GetAxisRaw("Horizontal");
        mouvementFleche.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Switch"))
        {
            if (currentweapon == "melee"){
                Destroy(gameObject.transform.GetChild(0).gameObject);
                var weapon = Instantiate(rangedWeapon, transform.position, transform.rotation);
                weapon.transform.parent = gameObject.transform;
                currentweapon = "ranged";
            }
            else{
                Destroy(gameObject.transform.GetChild(0).gameObject);
                var weapon = Instantiate(meleeWeapon, transform.position, transform.rotation);
                weapon.transform.parent = gameObject.transform;
                currentweapon = "melee";
            }
           
        }

    }

    // compensate for drag and mass
    private float calculate_force(float speed)
    {
       return vitesse * rig.drag * rig.mass;
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

        anim.SetFloat("Horizontal", lastDirection.x);
        anim.SetFloat("Vertical", lastDirection.y);
        if (isVictoryAchive) return;
        rig.AddForce(mouvementFleche.normalized * calculate_force(vitesse));
    }


    public void Death(){
        Destroy(col);
        rig.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Death");
    }


 
    private void OnCollisionEnter2D(Collision2D collision){
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Enemy")
        {
          
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "douzaine" && !isVictoryAchive)
        {
            StartCoroutine(victory_anim());
        }
    }

    IEnumerator victory_anim()
    {
        this.isVictoryAchive = true;
        float drag = rig.drag;
        rig.drag = 0;
        rig.velocity = Vector2.up * vitesse;
        yield return new WaitForSeconds(1 / vitesse);
        rig.drag = drag;
        yield return new WaitForSeconds(2f);
        rig.drag = 0;
        rig.velocity = Vector2.down * vitesse;
        yield return new WaitForSeconds(4 / vitesse);
        rig.drag = drag;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("endMenu");

    }
        
    private void RestartLevel()//accès par l'animation "death"
    {
        if (!isDead){
            isDead = true;
            StartCoroutine(fondu.GetComponent<fondue>().FounduSortie());
        }
    }

    public void TakeDamage(int amount){
        currHP -= amount;
        if(currHP <= 0){
            Death();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackSprite : MonoBehaviour
{
    private Rigidbody2D rig;
    [SerializeField] float speed;
    [SerializeField] bool melee;
    [SerializeField] float lifeTime;
    [SerializeField] public int damage;

    [SerializeField] public float knockback;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

    }


    void FixedUpdate()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0 && melee == true){
            Delete();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (melee == false){
            Delete();
            if (LayerMask.LayerToName(collision.gameObject.layer) == "Enemy"){
                //emmeteur? data de qui est toucher + damage de l'arme? 
            }
        }
 
    }

    void Delete(){
        Destroy(this.gameObject);
    }

}

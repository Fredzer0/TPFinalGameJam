using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rig;
    [SerializeField] float speed;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

    }

    private void OnCollisionEnter2D(Collision2D collision){
        
        Destroy(this.gameObject);
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Enemy"){
            //emmeteur? data de qui est toucher + damage de l'arme? 
        }
    }
}

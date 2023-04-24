using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementCochon : MonoBehaviour
{
    private Rigidbody2D rig;
    public GameObject waypoint;
    public float speed = 4f;

    private Animator anim;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        
        if (Vector2.Distance(waypoint.transform.position, transform.position) < 0.1f)
        {
            Destroy(this.gameObject);
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoint.transform.position, Time.deltaTime * speed);
        rig.velocity = Vector2.zero;

        Vector2 v = waypoint.transform.position - transform.position;

        v = v.normalized;
        anim.SetFloat("Horizontal", v.x);
        anim.SetFloat("Vertical", v.y);
    }
}

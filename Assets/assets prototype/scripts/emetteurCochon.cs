using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emetteurCochon : MonoBehaviour
{

    [SerializeField] private GameObject cochon;
    [SerializeField] private GameObject waypointw;
    [SerializeField] private float probability_spawn = 1f;
    [SerializeField] private float frequency = 3f;
    [SerializeField] private float cochonSpeed = 4f;

    void Start()
    {
        InvokeRepeating("CreateCochon", frequency, frequency);
    }


    void CreateCochon()
    {
        if (Random.value < probability_spawn)
        {
            GameObject ncochon =  Instantiate(cochon, transform.position, Quaternion.identity);
            movementCochon mc = ncochon.GetComponent<movementCochon>();
            mc.speed = cochonSpeed;
            mc.waypoint = waypointw;
        }
    }



}

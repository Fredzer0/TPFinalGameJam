using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StatsManager : MonoBehaviour
{

    public TMP_Text textHP;
    public int hp;

    // Start is called before the first frame update
    void Start()
    {
        hp = 0;
        textHP.text = "HP: " + hp.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        textHP.text = "HP: " + hp.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    // Start is called before the first frame update
    private StatsManager statsManager;
    private Player player;
    void Start()
    {
        statsManager = GameObject.Find("Canvas").GetComponent<StatsManager>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void update(){
        UpdateHP();
    }
    public void UpdateHP(){

        statsManager.hp = player.currHP;
    }
}

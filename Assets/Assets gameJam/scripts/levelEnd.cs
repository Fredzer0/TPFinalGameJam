using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class levelEnd : MonoBehaviour
{
    

    private bool levelCompleted = false;

    private void OnTriggerEnter2D(Collider2D collision){
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Player" && !levelCompleted ){
            Invoke("CompleteLevel", 1f);
            levelCompleted = true;
        }
    }

    private void CompleteLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}

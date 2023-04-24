using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fondue : MonoBehaviour 
{

    private SpriteRenderer rendu;
    [SerializeField] private float tauxfondu = 0.01f;
    void Start()
    {
        rendu = GetComponent<SpriteRenderer>();
        StartCoroutine(FounduEntree());
    }

    IEnumerator FounduEntree(){ //fade in
        Color couleurCourrante = Color.black;
        rendu.color = couleurCourrante;
        while(rendu.color.a > 0.01){
            yield return new WaitForEndOfFrame();
            couleurCourrante.a -= tauxfondu;
            rendu.color = couleurCourrante;
        } 
        couleurCourrante.a = 0.0f;
        rendu.color = couleurCourrante;
    }

    public IEnumerator FounduSortie(){ //fade out
    Color couleurCourrante = Color.black;
    couleurCourrante.a = 0.0f;
    rendu.color = couleurCourrante;
    while(rendu.color.a < 0.99f){
        yield return new WaitForEndOfFrame();
        couleurCourrante.a += tauxfondu;
        rendu.color = couleurCourrante;
    } 
    couleurCourrante.a = 1f;
    rendu.color = couleurCourrante;
    SceneManager.LoadScene("stage 1");
    }
}

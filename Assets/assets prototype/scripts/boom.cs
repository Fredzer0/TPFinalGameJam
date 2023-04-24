using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeleteThis", 1);
    }

    // Update is called once per frame
    private void DeleteThis(){
        Destroy(this.gameObject);
    }
}

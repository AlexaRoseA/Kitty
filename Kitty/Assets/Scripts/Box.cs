using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    // Start is called before the first frame update
    Animator ani;
    GameObject cat;

    private bool opened = false;

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenBox()
    {
        if(!opened)
        {
            ani.SetBool("Open", true);
            opened = true;
        }

    }
    
    void ChooseCat()
    {

    }
}

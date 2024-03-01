using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    // Start is called before the first frame update
    Animator ani;

    private bool opened = false;

    GameObject catInside = null;

    public bool Opened { get { return opened; } }

    public GameObject CatInside { get { return catInside; } set { catInside = value; } }

    private BoxManager boxMan;

    void Start()
    {
        ani = GetComponent<Animator>();    
        boxMan = GameObject.Find("Box Manager").GetComponent<BoxManager>();
    }

    public void OpenBox()
    {
        if(!opened)
        {
            ani.SetBool("Open", true);
            opened = true;
        }
    }

    public void CloseBox()
    {
        catInside.SetActive(true);
        boxMan.FreeToOpenNextBox = true;
        Destroy(this.gameObject);
    }

}

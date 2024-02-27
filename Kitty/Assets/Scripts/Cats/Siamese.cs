using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siamese : Cat
{
    Siamese(string catName, Type type, Profession pro) : base (catName, type, pro)
    {
        type = Type.Siamese;
    }

    public override void Lifecycle()
    {
        // lifecycle 
    }
}

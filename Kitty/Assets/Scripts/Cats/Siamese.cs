using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siamese : Cat
{
    Siamese(string catName, Profession pro) : base (catName, pro)
    {
        type = Type.Siamese;
    }

    public override void Lifecycle()
    {
        // lifecycle override
    }
}

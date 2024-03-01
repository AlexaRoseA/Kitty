using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orange : Cat
{
    private void Start()
    {
        type = Type.White;
        rarity = Rarity.Common;
    }
    Orange(string catName, Profession pro) : base(catName, pro)
    {
        Start();
    }

    public override void Lifecycle()
    {
        // lifecycle override
    }
}

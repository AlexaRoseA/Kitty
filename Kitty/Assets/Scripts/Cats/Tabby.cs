using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tabby : Cat
{
    private void Start()
    {
        type = Type.White;
        rarity = Rarity.Common;
    }
    Tabby(string catName, Profession pro) : base(catName, pro)
    {
        Start();
    }

    public override void Lifecycle()
    {
        // lifecycle override
    }
}

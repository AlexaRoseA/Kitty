using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHair : Cat
{
    private void Start()
    {
        type = Type.White;
        rarity = Rarity.Common;
    }
    LongHair(string catName, Profession pro) : base(catName, pro)
    {
        Start();
    }

    public override void Lifecycle()
    {
        // lifecycle override
    }
}

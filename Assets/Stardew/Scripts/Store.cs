using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : Interactable
{

    public override void Interact(Character character)
    {
        Trading trading = character.GetComponent<Trading>();

        if (trading == null) { return; }

        trading.BeginTrading();
    }
}

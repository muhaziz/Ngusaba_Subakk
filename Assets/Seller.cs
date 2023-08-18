using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seller : Interactable
{
    public override void Interact(Character character)
    {
        Selling selling = character.GetComponent<Selling>();

        if (selling == null) { return; }

        selling.BeginSelling();
    }
}

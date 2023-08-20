using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questinteract2 : Interactable
{
    public override void Interact(Character character)
    {

        diaolguequest dialogue = character.GetComponent<diaolguequest>();

        if (dialogue == null) { return; }

        dialogue.startQuest();

    }
}

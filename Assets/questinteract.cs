using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questinteract : Interactable
{
    public override void Interact(Character character)
    {

        diaolguequest dialogue = character.GetComponent<diaolguequest>();

        if (dialogue == null) { return; }

        dialogue.startQuest();

    }

}

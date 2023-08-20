using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questinteract : Interactable
{
    public override void Interact(Character character)
    {

        DialogueQuest2 dialogue = character.GetComponent<DialogueQuest2>();

        if (dialogue == null) { return; }

        dialogue.startQuest();

    }

}

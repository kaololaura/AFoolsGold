using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCConversation : MonoBehaviour
{

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            FindObjectOfType<DialogueTrigger>().TriggerDialogue();
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            DialogueManager.Instance.CloseDialogueWindow();
        }
    }
}

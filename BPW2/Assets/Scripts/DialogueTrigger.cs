using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dit script bepaalt wat de villager tegen je zegt

public class DialogueTrigger : MonoBehaviour
{
    [TextArea(3, 10)] public string startingDialogue;
    [TextArea(3, 10)] public string[] dialoguesWrong;
    [TextArea(3, 10)] public string[] dialoguesRight;

    public AudioSource NPCAudioSource;
    public AudioClip startDialogueClip;
    public AudioClip dialogueRightClip;
    public AudioClip dialogueWrongClip;

    bool isStartDialogue = true;

    public void TriggerDialogue()
    {
        if (isStartDialogue)
        {
            DialogueManager.Instance.StartDialogue(startingDialogue);
            TreasureManager.Instance.SpawnTreasure();
            PlayerScript.Instance.HasMap = true;
            UIManager.Instance.HUDText.text = "Find the treasure";
            BoatScript.Instance.AllowToLeave();
            NPCAudioSource.clip = startDialogueClip;
            NPCAudioSource.Play();
        }
        else
        {
            if(PlayerScript.Instance.HasTreasure == true)
            {
                DialogueManager.Instance.StartRandomDialogue(dialoguesRight);
                PlayerScript.Instance.HasTreasure = false;
                TreasureManager.Instance.SpawnTreasure();
                UIManager.Instance.HUDText.text = "Find the treasure";
                NPCAudioSource.clip = dialogueRightClip;
                NPCAudioSource.Play();
            }
            else
            {
                DialogueManager.Instance.StartRandomDialogue(dialoguesWrong);
                NPCAudioSource.clip = dialogueWrongClip;
                NPCAudioSource.Play();
            }
        }

        isStartDialogue = false;
    }
}

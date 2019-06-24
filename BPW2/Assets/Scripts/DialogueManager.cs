using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    //Dit script zorgt dat de dialogue werkt

    public static DialogueManager Instance;

    private Queue<string> sentences;
    public GameObject DialogueBox;
    public TextMeshProUGUI DialogueText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(string dialogue)
    {
        EnableDialogueWindow(dialogue);
    }

    public void StartRandomDialogue(string[] dialogues)
    {
        EnableDialogueWindow(dialogues[Random.Range(0, dialogues.Length)]);
    }

    void EnableDialogueWindow(string _text)
    {
        DialogueBox.SetActive(true);
        DialogueText.text = _text;
    }

    public void CloseDialogueWindow()
    {
        DialogueBox.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Dit script roept onder andere functies aan uit andere scripten
    //Verder zorgt het ervoor dat de speler de map kan openen en dat het spel sluit wanneer je op escape drukt

    public static PlayerScript Instance;
    public AudioSource MapAudio;
    public AudioClip mapAudioClip;
    public bool HasMap = false;
    public bool HasTreasure = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Update()
    {
        HandleInput();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && HasMap)
        {
            UIManager.Instance.ToggleCanvas();
            MapAudio.clip = mapAudioClip;
            MapAudio.Play();

        }

        ShovelScript.Instance.DigAnimation();
        ShovelScript.Instance.OnTreasureFound();
    }
}

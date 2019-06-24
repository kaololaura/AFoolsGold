using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelScript : MonoBehaviour
{

    //Dit script zorgt ervoor dat de speler kan graven en checkt ook meteen of deze dat in de buurt van de schat doet,
    //zo ja, dan gaat de boolean TreasureFound op true en speelt de schatkistanimatie af

    public static ShovelScript Instance;
    Animator anim;
    public AudioSource ShovelAudio;
    public AudioSource TreasureAudio;
    public AudioClip treasureFoundClip;
    public bool digging;
    public bool TreasureFound = false;
    public Transform TreasureChestTransform;
    public GameObject TreasureChest;
    public float TreasureChestMoveup;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
    }


    private void Start()
    {
        anim = GetComponent<Animator>();
        ShovelAudio = gameObject.GetComponent<AudioSource>();
        ShovelAudio.Play();
        ShovelAudio.Pause();

    }

    public void DigAnimation()
    {
        if(UIManager.Instance.CurrentState > UIManager.TutorialState.Walk || UIManager.Instance.IsTutorialLevel == false)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                digging = true;
                ShovelAudio.UnPause();
            }

            else
            {
                digging = false;
                ShovelAudio.Pause();
            }

            anim.SetBool("MakeDig", digging);
        }
    }

    public void OnTriggerStay(Collider col)
    {
        if (digging == true)
        {
            
            if (col.gameObject.tag == "TreasureChest")
            {
                TreasureFound = true;
                TreasureAudio.clip = treasureFoundClip;
                TreasureAudio.Play();
            }
        }
    }

    public void OnTreasureFound()
    {
        if(TreasureChest != null)
        {
            Animator _chestAnim = TreasureChest.GetComponent<Animator>();
            _chestAnim.SetBool("ChestFound", TreasureFound);
        }
    }
}

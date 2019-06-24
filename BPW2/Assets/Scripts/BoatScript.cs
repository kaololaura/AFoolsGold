using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Dit script checkt of de speler kan vertrekken met de boot
//Het zorgt er ook voor dat zolang de speler in de boot zit, deze niet kan bewegen

public class BoatScript : MonoBehaviour
{
    public static BoatScript Instance;
    public AudioSource NPCAudio;
    public AudioClip NPCleavingAudio;

    public enum State { Intro, CanWalk, Outro }
    public State CurrentState = State.Intro;

    public Transform DockTransform;
    public Transform SeatTransform;

    bool isAllowedToLeave = false;
    public GameObject LeaveText;

    [TextArea(3, 10)] public string endDialogue;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        LeaveText.SetActive(false);
        PlayerScript.Instance.GetComponent<CharacterController>().enabled = false;
    }

    private void Update()
    {
        if (CurrentState != State.CanWalk)
        {
            KeepPlayerInBoat();
        }
    }

    public void AllowToLeave()
    {
        LeaveText.SetActive(true);
        isAllowedToLeave = true;
    }

    public void OnIntroFinished()
    {
        PlayerScript.Instance.transform.parent = null;
        PlayerScript.Instance.transform.position = DockTransform.position;
        PlayerScript.Instance.transform.rotation = DockTransform.rotation;

        PlayerScript.Instance.GetComponent<CharacterController>().enabled = true;

        UIManager.Instance.HUDText.gameObject.SetActive(true);
        CurrentState = State.CanWalk;
    }

    void KeepPlayerInBoat()
    {
        PlayerScript.Instance.transform.parent = transform;
        PlayerScript.Instance.gameObject.transform.position = SeatTransform.transform.position;
        PlayerScript.Instance.gameObject.transform.rotation = SeatTransform.transform.rotation;
    }

    void LeaveIsland()
    {
        KeepPlayerInBoat();
        DialogueManager.Instance.StartDialogue(endDialogue);
        CurrentState = State.Outro;
        GetComponent<Animator>().SetBool("LeaveIsland", true);
        isAllowedToLeave = false;
        LeaveText.SetActive(false);
        UIManager.Instance.Invoke("StartFadeOut", 2);
        NPCAudio.clip = NPCleavingAudio;
        NPCAudio.Play();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E) && isAllowedToLeave)
            {
                LeaveIsland();
            }
        }
    }
}

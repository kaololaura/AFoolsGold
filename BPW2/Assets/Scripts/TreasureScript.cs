using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TreasureScript : MonoBehaviour
{

    //Dit script zorgt ervoor dat de speler de schat kan oppakken en dan in zijn 'inventory' heeft

    public AudioSource TreasureAudio;
    public AudioClip hasTreasureClip;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && ShovelScript.Instance.TreasureFound)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(UIManager.Instance.IsTutorialLevel)
                {
                    if(UIManager.Instance.CurrentState == UIManager.TutorialState.Done)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                }
                else
                {
                    PlayerScript.Instance.HasTreasure = true;
                    TreasureAudio.clip = hasTreasureClip;
                    TreasureAudio.Play();
                    ShovelScript.Instance.TreasureFound = false;
                    Destroy(transform.parent.gameObject);
                    UIManager.Instance.HUDText.text = "Bring back the treasure";
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Dit script zorgt voor de UI, met name die in de tutorialscene

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public enum TutorialState { Intro, Walk, Dig, Map, DigChest, Done }
    public TutorialState CurrentState = TutorialState.Intro;

    public GameObject Canvas;
    public TextMeshProUGUI HUDText;
    public Image FadeImage;
    public AnimationCurve FadeCurve;

    public bool IsTutorialLevel = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (FadeImage != null)
        {
            FadeImage.color = new Color(0, 0, 0, 0);
        }

        if (IsTutorialLevel == true)
        {
            StartCoroutine(PlayTutorial());
        }

        else
        {
            HUDText.gameObject.SetActive(false);
            HUDText.text = "Talk to the villager";
        }

    }

    private void Start()
    {
        Canvas.SetActive(false);
    }

    public void ToggleCanvas()
    {
        Canvas.SetActive(Canvas.activeInHierarchy ? false : true);
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float lerpTime = 0;
        while (lerpTime < 1)
        {
            lerpTime += Time.deltaTime / 3;

            float lerpKey = FadeCurve.Evaluate(lerpTime);
            FadeImage.color = new Color(0, 0, 0, lerpKey);
            Application.Quit();
            yield return null;
        }

        yield return null;
    }

    IEnumerator PlayTutorial()
    {
        HUDText.text = "";
        yield return new WaitForSeconds(1);

        HUDText.text = "Welcome to 'A Fool's Gold'!";

        yield return new WaitForSeconds(3);
        HUDText.text = "";
        yield return new WaitForSeconds(1);

        HUDText.text = "I will give you a quick tutorial of how everything works.";

        yield return new WaitForSeconds(3);
        HUDText.text = "";
        yield return new WaitForSeconds(1);

        HUDText.text = "Use WASD to walk around";

        bool hasWalked = false;
        while (hasWalked == false)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                hasWalked = true;
            }
            yield return null;
        }

        CurrentState = TutorialState.Dig;
        HUDText.text = "";
        yield return new WaitForSeconds(3);
        HUDText.text = "Very good, now try to dig by pressing the left mouse button.";

        bool hasDigged = false;
        while (hasDigged == false)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                hasDigged = true;
            }
            yield return null;
        }

        CurrentState = TutorialState.Map;
        HUDText.text = "";
        yield return new WaitForSeconds(3);
        HUDText.text = "I'm digging it! Now how about we go find a treasure chest?";
        yield return new WaitForSeconds(4);

        HUDText.text = "Press TAB to open up your treasure map!";
        PlayerScript.Instance.HasMap = true;

        bool hasOpenedMap = false;
        while (hasOpenedMap == false)
        {
            if (Input.GetKey(KeyCode.Tab))
            {
                TreasureManager.Instance.SpawnTreasure();
                hasOpenedMap = true;
            }
            yield return null;
        }

        CurrentState = TutorialState.DigChest;
        HUDText.text = "";
        yield return new WaitForSeconds(3);
        HUDText.text = "You see that red X on the map? That's where the treasure is hidden! Let's dig it up!";

        while (ShovelScript.Instance.TreasureFound == false)
        {
            yield return null;
        }

        HUDText.text = "You found it! Good job!";
        yield return new WaitForSeconds(6);
        HUDText.text = "This is the end of the tutorial. Once you pick up the chest, the game will be loaded, have fun!";
        CurrentState = TutorialState.Done;

        yield return null;
    }

}

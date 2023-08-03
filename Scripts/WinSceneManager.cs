using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinSceneManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI winText;

    [SerializeField] GameObject pointerObj;

    readonly string[] displayText =
    {
        "You won! Got 0 EXP and 0 D$",
        "Thanks for playing! This battle was based off an animation by Mehmet Salih Koten (Mehmet SK on YouTube). Go check out their video!",
        "Thank you Mehmet for making an amazing animation, and thank you OMOCAT and Toby Fox for making amazing games.",
        "I'm assuming you've already played OMORI, so do yourself a favour and go play deltarune if you haven't already. Amazing game, and (in my opinion) one of the best soundtracks of all time (not just of video games).",
        "Goodbye now.",
        "(Waiting for something to happen?)"
    };

    int currentDisplayText = 0;

    bool allowZPress = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerPrefs.SetInt("won", 1);
        if (!GlobalData.instance.gotHit)
        {
            PlayerPrefs.SetInt("nohit", 1);
            StartCoroutine(AnimateText(displayText[currentDisplayText] + "............. and you didn't get hit a single time! Wow, that's impressive."));
        }
        else
        {
            StartCoroutine(AnimateText(displayText[currentDisplayText]));
        }
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && allowZPress)
        {
            allowZPress = false;
            currentDisplayText++;
            pointerObj.SetActive(false);
            StartCoroutine(AnimateText(displayText[currentDisplayText]));

            if (currentDisplayText == 1)
            {
                AudioManager.instance.PlayAudio("cyber");
            }
        }
    }

    IEnumerator AnimateText(string theText)
    {
        winText.text = string.Empty;
        foreach (char c in theText.ToCharArray())
        {
            winText.text += c;
            yield return new WaitForSeconds(0.025f);
        }
        allowZPress = true;
        pointerObj.SetActive(true);

        if (currentDisplayText == 5)
        {
            AudioManager.instance.StopAudio("cyber");
            SceneManager.LoadScene("TitleScene");
        }
    }
}

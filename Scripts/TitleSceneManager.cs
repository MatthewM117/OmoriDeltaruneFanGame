using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] GameObject star1;
    [SerializeField] GameObject star2;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playButton.Select();
        AudioManager.instance.PlayAudio("title");

        if (PlayerPrefs.GetInt("won") == 1)
        {
            star1.SetActive(true);
        }

        if (PlayerPrefs.GetInt("nohit") == 1)
        {
            star2.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playButton.Select();
        }
    }

    public void StartGame()
    {
        GlobalData.instance.is2Player = false;
        AudioManager.instance.StopAudio("title");
        SceneManager.LoadScene("SampleScene");
    }

    public void StartCoop()
    {
        GlobalData.instance.is2Player = true;
        AudioManager.instance.StopAudio("title");
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

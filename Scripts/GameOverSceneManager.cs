using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverSceneManager : MonoBehaviour
{
    [SerializeField] Button yesButton;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        yesButton.Select();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            yesButton.Select();
        }
    }

    public void OnYes()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnNo()
    {
        SceneManager.LoadScene("TitleScene");
    }
}

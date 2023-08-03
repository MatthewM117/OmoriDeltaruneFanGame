using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelect : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] GameObject pointer;
    [SerializeField] string buttonName;

    GameManager gm;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        pointer.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        pointer.SetActive(true);
        AudioManager.instance.PlayAudio("sys_move");

        switch (buttonName)
        {
            case "calmdown":
                gm.SetSkillDescriptionText("CALM DOWN", "Removes EMOTIONS and heals some HEART.", "0");
                break;
            case "encore":
                gm.SetSkillDescriptionText("ENCORE", "Your JUICE will not fall for 3 turns.", "0");
                break;
            case "cherish":
                gm.SetSkillDescriptionText("CHERISH", "Heal your wounds and come back stronger.", "0");
                break;
            case "allegro":
                gm.SetSkillDescriptionText("ALLEGRO", "Attacks 3 times.", "19");
                break;
        }
    }
}

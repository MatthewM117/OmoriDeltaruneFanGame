using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GOButtonSelect : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] GameObject pointer;

    public void OnDeselect(BaseEventData eventData)
    {
        pointer.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        pointer.SetActive(true);
        AudioManager.instance.PlayAudio("sys_move");
    }
}

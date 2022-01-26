using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [Header("Debug only")]
    [SerializeField] Button[] allButtons;

    private void Start()
    {
        allButtons = FindObjectsOfType<Button>();
    }

    public void SetButtons(bool state)
    {
        foreach(Button button in allButtons)
        {
            button.interactable = state;
        }
    }
}

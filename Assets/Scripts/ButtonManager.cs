using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] Button nextTurnButton;
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

    public void SetNextTurnButton(bool state)
    {
        nextTurnButton.interactable = state;
    }
}

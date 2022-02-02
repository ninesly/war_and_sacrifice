using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] Button nextTurnButton;
    [SerializeField] Button proceedButton;
    [SerializeField] Button[] drawCardsButtons;

    [SerializeField] Button[] buttonsToDisable;

    void Start()
    {
        //buttonsToDisable = FindObjectsOfType<Button>();
    }

    public void SetButtons(bool state)
    {
        foreach(Button button in buttonsToDisable)
        {
            button.interactable = state;
        }
    }

    public void SetNextTurnButton(bool state)
    {
        nextTurnButton.interactable = state;
    }

    public void SetProceedButton(bool state)
    {
        proceedButton.interactable = state;
    }

    public void SetDrawCardsButton(bool state)
    {
        foreach (Button button in drawCardsButtons)
        {
            button.interactable = state;
        }
    }
}

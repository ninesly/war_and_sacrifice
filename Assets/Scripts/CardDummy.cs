using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CardDummy : MonoBehaviour
{
    TMP_Text textComponent;
    CardContainer cardContainer;
    Card cardSO;

    void Start()
    {
        textComponent = GetComponentInChildren<TMP_Text>();

        cardContainer = GetComponentInParent<CardContainer>();
        
    }

    public void SetCardSO_Dummy(Card cardSO)
    {
        this.cardSO = cardSO;
        UpdateCardInfo();
        
    }

    private void UpdateCardInfo()
    {
        textComponent.text = cardSO.cardName;
        // other info like image etc.
    }

    /*void Update()
    {
        if (!cardContainer || !cardContainer.GetFirstCardSOFromContainer())
        {
            return;
        }

        if (!textComponent)
        {
            Debug.LogError(gameObject.name + ": There is no text component");
            return;
        }
        */
}

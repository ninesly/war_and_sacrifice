using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardDummy : MonoBehaviour
{
    TMP_Text textComponent;
    CardContainer cardContainer;

    void Start()
    {
        textComponent = GetComponentInChildren<TMP_Text>();
        textComponent.text = "X";

        cardContainer = GetComponentInParent<CardContainer>();
        
    }
    void Update()
    {
        if (!cardContainer || !cardContainer.GetFirstCardSOFromContainer()) return;
        textComponent.text = cardContainer.GetFirstCardSOFromContainer().cardName;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CardDummy : MonoBehaviour
{
    [SerializeField] Sprite faceDown;
    TMP_Text textComponent;
    Card cardSO;
    

    void Start()
    {
        textComponent = GetComponentInChildren<TMP_Text>();        
    }

    public void SetCardSO_OnDummy(Card cardSO)
    {
        this.cardSO = cardSO;
        UpdateCardInfo();       
    }

    void UpdateCardInfo()
    {
        textComponent.text = cardSO.cardName;
        // other info like image etc.
    }

    public void SetFaceDown()
    {
        GetComponent<Image>().sprite = faceDown;
        textComponent.text = "";
    }

}

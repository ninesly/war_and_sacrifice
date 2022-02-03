using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardContainer : MonoBehaviour
{
    [SerializeField] TurnManager.Users user;
    [SerializeField] List<Card> cardsSOInContainer = new List<Card>();
    [SerializeField] CardDummy cardDummy;
    [SerializeField] TMP_Text cardsInContainer_TextComponent;
    [SerializeField] string cardsInContainer_TextContent = "Cards:";
    [SerializeField] bool faceUpCards = false;

    bool isEmpty = false;

    void Start()
    {
        if (!cardDummy) Debug.LogError("This Card Container " + gameObject.name + " doesn't have Card Dummy!");
        if(!cardsInContainer_TextComponent) Debug.LogWarning("This Card Container " + gameObject.name + " doesn't have text module");

        CheckIfEmpty();
    }

    void CheckIfEmpty()
    {
        int cardsInContainer = cardsSOInContainer.Count;
        if (cardsInContainer_TextComponent) cardsInContainer_TextComponent.text = cardsInContainer_TextContent + cardsInContainer.ToString();


        if (cardsInContainer == 0)
        {
            isEmpty = true;
            cardDummy.gameObject.SetActive(false);
            return;
        }

        isEmpty = false;
        cardDummy.gameObject.SetActive(true);
        ChangeCoverImage();        
    }

    void ChangeCoverImage()
    {    
        if (faceUpCards)
        {
            var lastElement = cardsSOInContainer.Count-1;
            cardDummy.SetCardSO_OnDummy(cardsSOInContainer[lastElement]);
            return;
        }
        cardDummy.SetFaceDown();
    }

    public void AddCardSOToContainer(Card cardSO)
    {
        cardsSOInContainer.Add(cardSO);
        CheckIfEmpty();
    }

    public void RemoveCardSOFromContainer(Card cardSO)
    {
        cardsSOInContainer.Remove(cardSO);
        CheckIfEmpty();
    }

    public void RemoveAllCardsSOFromContainer()
    {
        cardsSOInContainer.Clear();
        CheckIfEmpty();
    }

    public Card GetFirstCardSOFromContainer()
    {
        if (isEmpty) return null;
        
        var currentCardSO = cardsSOInContainer[0]; // 0 is "upper" card
        cardsSOInContainer.RemoveAt(0);
        CheckIfEmpty();

        return currentCardSO;
    }

    public List<Card> GetAllCardsSOInContainer()
    {
        return cardsSOInContainer;
    }

    public int GetNumberOfAllCardsSO()
    {
        return cardsSOInContainer.Count;
    }

    public void ShuffleCardsInContainer()
    {
        int original = cardsSOInContainer.Count;

        while (original > 1)
        {
            original--;
            int random = UnityEngine.Random.Range(0, original + 1);
            Card temp = cardsSOInContainer[random];
            cardsSOInContainer[random] = cardsSOInContainer[original];
            cardsSOInContainer[original] = temp;
        }
    }   

    public TurnManager.Users GetUserOfContainer()
    {
        return user;
    }
    
    public void SetGridLayout(bool state)
    {
        GetComponent<GridLayout>().enabled = state;
    }

}

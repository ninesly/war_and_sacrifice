using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardContainer : MonoBehaviour
{
    [SerializeField] TurnManager.Users user;
    [SerializeField] List<Card> cardsSOInContainer = new List<Card>();
    [SerializeField] Image coverImage;
    [SerializeField] bool faceUpCards = false;

    bool isEmpty = false;

    void Start()
    {
        if (!coverImage) Debug.LogError("This Card Container " + gameObject.name + " doesn't have cover image!");

        CheckIfEmpty();
    }

    void CheckIfEmpty()
    {
        if (cardsSOInContainer.Count == 0)
        {
            isEmpty = true;
            coverImage.gameObject.SetActive(false);
            return;
        }

        isEmpty = false;
        coverImage.gameObject.SetActive(true);
        ChangeCoverImage();        
    }

    void ChangeCoverImage()
    {    
        if (faceUpCards)
        {
            var lastElement = cardsSOInContainer.Count-1;
            coverImage.GetComponent<CardSOManager>().SetCardSO(cardsSOInContainer[lastElement], user);
        }   
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

    public Card GetCardSOFromContainer()
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


}

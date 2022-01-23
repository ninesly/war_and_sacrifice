using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardContainer : MonoBehaviour
{
    [SerializeField] List<Card> cardsSOInContainer = new List<Card>();
    [SerializeField] Image coverImage;
    [SerializeField] bool faceUpCards = false;

    bool isEmpty = false;
    int currentCardIndex = 0;
    

    private void Start()
    {
        if (!coverImage) Debug.LogError("This Card Container " + gameObject.name + " doesn't have cover image!");

        CheckIfEmpty();
    }

    private void CheckIfEmpty()
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

    private void ChangeCoverImage()
    {    
        if (faceUpCards)
        {
            var lastElement = cardsSOInContainer.Count-1;
            coverImage.GetComponent<CardManager>().SetCardSO(cardsSOInContainer[lastElement]);
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

        var currentCardSO = cardsSOInContainer[currentCardIndex];
        cardsSOInContainer.RemoveAt(currentCardIndex);
        currentCardIndex++;
        if (currentCardIndex >= cardsSOInContainer.Count) currentCardIndex = 0;
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
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour
{
    [SerializeField] Color highlightColor;
    Color defaultColor;

    GameObject canvas;
    GameObject startParent;
    public GameObject dropZone;

    Vector2 startPosition;

    bool isOverDropzone = false;
    bool isDragging = false;
    bool isOccupied = false;
    bool isInteractable;
  

    GameSession gameSession;
    Card cardSO;

    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Main Canva");
    }

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        cardSO = GetComponent<CardManager>().GetCardSO();
    }

    void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(canvas.transform, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Drop Zone"))
        {
            //isInteractable = true;
            isOverDropzone = true;
            dropZone = collision.gameObject;
            ShowHighlight(collision);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isOverDropzone) HideHighlight(collision);
        isOverDropzone = false;
        dropZone = null;
    }

    private void ShowHighlight(Collision2D collision)
    {
        defaultColor = collision.gameObject.GetComponent<Image>().color;
        collision.gameObject.GetComponent<Image>().color = highlightColor;
    }

    private void HideHighlight(Collision2D collision)
    {
        collision.gameObject.GetComponent<Image>().color = defaultColor;
    }

    private void AddingCardSOToCantainer()
    {
        var cardContainer = dropZone.GetComponent<CardContainer>();
        if (!cardContainer) return;

        cardContainer.AddCardSOToContainer(cardSO);
        Destroy(gameObject);
    }

    // -------------------------------------------------------------------------------------------------- PUBLIC METHODS

    public void StartDrag() // by Event Trigger
    {
        //if (!isInteractable) return;

        startPosition = transform.position;
        startParent = transform.parent.gameObject;
        isDragging = true;        
    }

    public void StopDrag() // by Event Trigger
    {
        isDragging = false;
        isOccupied = dropZone.gameObject.GetComponent<CountCardsInField>().CheckIfFieldLimitReached();


        if (isOverDropzone && !isOccupied)
        {
            // succesful drag
            ChangePlaceOfCard();
            return;
        }

        //unsuccesful drag
        TakeCardBack();
    }

    private void TakeCardBack()
    {
        transform.position = startPosition;
        transform.SetParent(startParent.transform, false);
    }

    private void ChangePlaceOfCard()
    {
        transform.SetParent(dropZone.transform, false);
        transform.localPosition = Vector3.zero;
        AddingCardSOToCantainer();
    }

    public GameObject GetCanvas()
    {
        return canvas;
    }
  
}

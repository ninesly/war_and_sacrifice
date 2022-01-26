using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour
{
    [SerializeField][Range(-1f, 1f)] float colorValueModificator = -0.06f;
    Color defaultColor;

    GameObject canvas;
    GameObject startParent;
    GameObject dropZone;

    Vector2 startPosition;

    bool isDragging = false;
    bool isOccupied = false;

  

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
        if (collision.gameObject.CompareTag("Drop Zone") && isDragging)
        {
            dropZone = collision.gameObject;
            ShowHighlight(dropZone);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        HideHighlight(dropZone);
        dropZone = null;
    }

    private void ShowHighlight(GameObject dropZone)
    {
        if (!dropZone) return;

        defaultColor = dropZone.GetComponent<Image>().color;

        float h, s, v; 
        Color.RGBToHSV(defaultColor, out h, out s, out v);
        v += colorValueModificator;

        dropZone.GetComponent<Image>().color = Color.HSVToRGB(h, s, v);
    }

    private void HideHighlight(GameObject dropZone)
    {
        if (!dropZone) return;

        dropZone.GetComponent<Image>().color = defaultColor;
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
        startPosition = transform.position;
        startParent = transform.parent.gameObject;
        isDragging = true;        
    }

    public void StopDrag() // by Event Trigger
    {
        isDragging = false;     

        if (!dropZone)
        {
            //unsuccesful 1
            TakeCardBack();
            return;
        }

        isOccupied = dropZone.gameObject.GetComponent<CountCardsInField>().CheckIfFieldLimitReached();

        if (isOccupied)
        {
            //unsuccesful 2
            TakeCardBack();
            return;
        }

        // succesful 
        ChangePlaceOfCard();
        HideHighlight(dropZone);
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

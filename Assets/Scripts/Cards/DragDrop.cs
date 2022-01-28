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

    MoveCards moveCards;
    CardObjectManager cardManager;

    GameSession.Users user;
    Vector2 startPosition;
    bool isDragging = false;
   
    

    void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Main Canva");
    }

    void Start()
    {
        moveCards = GetComponent<MoveCards>();
        cardManager = GetComponent<CardObjectManager>();
        user = cardManager.GetUserOfCard();
    }

    void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(canvas.transform, true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Drop Zone") && isDragging)
        {
            dropZone = collision.gameObject;
            ShowHighlight(dropZone);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        HideHighlight(dropZone);
        dropZone = null;
    }

    void ShowHighlight(GameObject dropZone)
    {
        if (!dropZone) return;

        defaultColor = dropZone.GetComponent<Image>().color;

        float h, s, v; 
        Color.RGBToHSV(defaultColor, out h, out s, out v);
        v += colorValueModificator;

        dropZone.GetComponent<Image>().color = Color.HSVToRGB(h, s, v);
    }

    void HideHighlight(GameObject dropZone)
    {
        if (!dropZone) return;

        dropZone.GetComponent<Image>().color = defaultColor;
    }

    void TakeCardBack()
    {
        transform.position = startPosition;
        transform.SetParent(startParent.transform, false);
    }

    // -------------------------------------------------------------------------------------------------- PUBLIC METHODS

    public void StartDrag() // by Event Trigger
    {
        if (cardManager.GetActualUser() != user) return;

        startPosition = transform.position;
        startParent = transform.parent.gameObject;
        isDragging = true;        
    }

    public void StopDrag() // by Event Trigger
    {
        if (cardManager.GetActualUser() != user) return;

        isDragging = false;     

        if (!dropZone)
        {
            //unsuccesful drag
            TakeCardBack();
            return;
        }

        // succesful drag - card is above proper Field
        // now we check if placing a card was succesful
        Field fieldOfDropZone = dropZone.GetComponent<Field>();
        if (!moveCards.AttemptToChangePlaceOfCard(fieldOfDropZone))
        {
            //unsuccesful placing
            TakeCardBack();
            return;
        }

        // succesful placing 
        // placing itself will be handled by MoveCards component
        HideHighlight(dropZone);  
    }

    public GameObject GetCanvas()
    {
        return canvas;
    }
    
  
}

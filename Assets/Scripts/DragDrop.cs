using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    GameObject canvas;
    GameObject startParent;
    GameObject dropZone;
    Vector2 startPosition;
    bool isOverDropzone;
    bool isDragging = false;
    public bool isInteractable;

    GameSession gameSession;

    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Main Canva");
    }

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
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

        Debug.Log("Collision with " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Drop Zone"))
        {
            isOverDropzone = true;
            dropZone = collision.gameObject;
            isInteractable = false;
        }

        if (collision.gameObject.CompareTag("Player Field"))
        {
            isInteractable = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        isOverDropzone = false;
        dropZone = null;

    }

    public void StartDrag()
    {
        if (!isInteractable) return;

        startPosition = transform.position;
        startParent = transform.parent.gameObject;
        isDragging = true;        
    }

    public void StopDrag()
    {
        isDragging = false;
        if (isOverDropzone)
        { // succesful drag, card changes the place
            transform.SetParent(dropZone.transform, false);
            transform.localPosition = Vector3.zero;
            gameSession.AddPlayerCardsInField(-1);
            return;
        }

        //unsuccesful drag, card goes back to original place
        transform.position = startPosition;
        transform.SetParent(startParent.transform, false);
    }

    public GameObject GetCanvas()
    {
        return canvas;
    }
  
}

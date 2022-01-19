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

    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Main Canva");
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

        isOverDropzone = true;
        dropZone = collision.gameObject;

    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        isOverDropzone = false;
        dropZone = null;

    }

    public void StartDrag()
    {
        startPosition = transform.position;
        startParent = transform.parent.gameObject;
        isDragging = true;        
    }

    public void StopDrag()
    {
        isDragging = false;
        if (isOverDropzone)
        {
            transform.SetParent(dropZone.transform, false);
            return;
        }

        transform.position = startPosition;
        transform.SetParent(startParent.transform, false);
    }

    public GameObject GetCanvas()
    {
        return canvas;
    }
  
}

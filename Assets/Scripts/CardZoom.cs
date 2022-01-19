using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardZoom : MonoBehaviour
{
    [SerializeField] float zoomScale = 2f;

    GameObject canvas;
    GameObject zoomedCard;
    RectTransform rect;

    bool isClone = false;

    void Awake()
    {        
        canvas = GetComponent<DragDrop>().GetCanvas();
    }

    public void OnHoverEnter()
    {
        if (isClone) { return; }

        rect = GetComponent<RectTransform>();
        var position = canvas.GetComponent<CanvasSavedObjects>().GetZoomedCardPosition();
        var scale = new Vector3(zoomScale, zoomScale);

        zoomedCard = Instantiate(gameObject, position, Quaternion.identity);
        zoomedCard.GetComponent<CardZoom>().SetAsClone();
        zoomedCard.transform.SetParent(canvas.transform, true);
        zoomedCard.GetComponent<BoxCollider2D>().enabled = false;
        zoomedCard.GetComponent<RectTransform>().sizeDelta = rect.sizeDelta;
        zoomedCard.GetComponent<RectTransform>().localScale = scale;

    }

    public void OnHoverExit()
    {
        Destroy(zoomedCard);
    }

    public void SetAsClone()
    {
        isClone = true;
    }
}

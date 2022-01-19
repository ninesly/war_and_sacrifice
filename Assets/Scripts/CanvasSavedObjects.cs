using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSavedObjects : MonoBehaviour
{
    [SerializeField] GameObject zoomedCardPosition;

    public Vector2 GetZoomedCardPosition()
    {
        return zoomedCardPosition.transform.position;
    }
}

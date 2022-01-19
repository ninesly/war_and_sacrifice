using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    [SerializeField] GameObject PlayerArea;
    [SerializeField] GameObject EnemyArea;
    [SerializeField] GameObject Card;

    public void OnClick()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject pickedCard = Instantiate(Card, Vector3.zero, Quaternion.identity);
            pickedCard.transform.SetParent(PlayerArea.transform, false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardSOManger : MonoBehaviour
{
    [SerializeField] Card cardSO;
    [Header("Debug Only")]
    [SerializeField] GameSession.Users userOfCard;
    TMP_Text cardText;
}

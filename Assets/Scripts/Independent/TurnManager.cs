using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public enum Users { Player, Enemy, None };

    [SerializeField] TMP_Text currentPhaseText;
    [SerializeField] ButtonManager buttonManager;
    [SerializeField] Users startingUser;
    [Tooltip("If true, it will override 'startingUser' selection")]
    [SerializeField] bool randomStartingUser;
    [SerializeField] int numberOfPlayer = 2;
    [SerializeField] int numberOfPhases = 2;

    [Header("Debug Only")]
    [SerializeField] int playerTurn = 0;
    [SerializeField] int enemyTurn = 0;
    [SerializeField] int currentGameRound = 0;
    [SerializeField] Users whoIsPlaying;
    [SerializeField] Users lastUser;
    [SerializeField] int phase = 0;    
    
    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        if (randomStartingUser)
        {
            var coinThrow = Random.Range(0, 2);
            startingUser = (Users)coinThrow;
        }
        whoIsPlaying = startingUser;
        TurnCounter(whoIsPlaying);

        currentPhaseText.text = "Starting round. " + whoIsPlaying + " turn";
        buttonManager.SetProceedButton(false);
    }

    public void NextTurn()
    {
        Debug.Log("Judge: " + whoIsPlaying + " finished turn.");
        lastUser = whoIsPlaying;
        Proceed();     

    }

    void NextPhase()
    {
        buttonManager.SetProceedButton(false);

        if (phase == 1)
        {
            // not interactable phase
            whoIsPlaying = Users.None;
            SetPlayersButton();

            if (CheckIfDuelIsPossible())
            {
                currentPhaseText.text = "Duel phase";
                DuelPhase();
                buttonManager.SetProceedButton(true);
                return;
            }
            currentPhaseText.text = "No duel";
            buttonManager.SetProceedButton(true);
        }
        else if (phase == 2) 
        {
            DecidingWhoIsNowPlaying();            
            SetPlayersButton(); // if player - enabling buttons
            TurnCounter(whoIsPlaying);
            currentPhaseText.text = currentGameRound + " round. " + whoIsPlaying + " turn";
        }
    }

    void DecidingWhoIsNowPlaying()
    {
        int nextUser_Int = (int)lastUser + 1;
        if (nextUser_Int >= numberOfPlayer) nextUser_Int = 0;
        whoIsPlaying = (Users)nextUser_Int;

        Debug.Log("Judge: New turn. " + whoIsPlaying + " is playing.");
    }

    private void SetPlayersButton()
    {
        if (whoIsPlaying == Users.Player)
        {
            buttonManager.SetButtons(true);
            return;
        }
        buttonManager.SetButtons(false);
    }

    void TurnCounter(Users actualUser)
    {
        if ((int)actualUser == 0)
        {
            playerTurn++;
        }
        else
        {
            enemyTurn++;
        }

        RoundCounter();
    }

    void RoundCounter()
    {
        float roundProgression = (playerTurn + enemyTurn) / 2;
        currentGameRound = Mathf.CeilToInt(roundProgression);            
    }   

    bool CheckIfDuelIsPossible()
    {
        DuelField[] duelfields = FindObjectsOfType<DuelField>();

        if (!duelfields[0].CheckIfFieldLimitReached() || !duelfields[1].CheckIfFieldLimitReached())
        {
            Debug.Log("Judge: No duel. " + duelfields[0].gameObject.name + ": " + duelfields[0].CheckIfFieldLimitReached() + duelfields[1].gameObject.name + ": " + duelfields[1].CheckIfFieldLimitReached());
            return false;
        }

        Debug.Log("Judge: DUEL TIME!");
        return true;        
    }

    void DuelPhase()
    {
        DuelField[] duelfields = FindObjectsOfType<DuelField>();

        foreach (DuelField duelField in duelfields)
        {            
            var card = duelField.GetComponentInChildren<CardSOManager>();
            Debug.Log(card.gameObject.name + " is trying to trigger ability");

            /*if (card)
            {
                card.CM_TriggerAbility();
                Debug.Log("Trigger Ability on " + card.gameObject.name);
            }*/
        }
    }






    // Get & Set 
    public void OnClick()
    {
        NextTurn();
    }

    public void Proceed()
    {
        phase++;
        if (phase > numberOfPhases) phase = 1; 
        NextPhase();
    }

    public int GetCurrentGameRound()
    {
        return currentGameRound;
    }

    public Users GetActualUser()
    {
        return whoIsPlaying;
    }

    public void SetUserAsReady(bool state)
    {
        buttonManager.SetNextTurnButton(state);
    }
}

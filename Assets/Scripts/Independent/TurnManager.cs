using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* Gameplay is divided into Rounds, Round Phases, Turns and Duel Subphases
 * 
 * Round - it's a whole gameplay loop. So it consist one turn of each user and 
 * non interactible phase inbetween.
 * Rounds are continuously counted.
 * Starting round is an exception as it consist only starting user turn.
 * 
 * Round phases - there are two types: User Phase (Turns) and Duel Phase:
 * 
 * 1. User Phases - it's a whole interactible loop from the moment when User has 
 * ability to make actions, until it triggers "Next turn" method. 
 * Each round consist two User Phases - one Player Turn and one Enemy Turn. 
 * Turns for each User are continuously counted.
 *      
 * 2. Duel phase - divided into subphases. Non interactible phase that contains 
 * the order in which the duel is played.
 * Each round consist two Duel phases (exactly the same)
 * 
 * Subphase - smallest unit of gameplay
 * Only Duel Phase divides into subphases. 
 * Duel Subphases: Bench Abilities, Defender defensive Abilities, Attacker Abilities,
 * Defender offensive Abilities.
 * 
 * Whole gameplay loop:
 * 
 * Starting Round (0) [setup of the starting user]
 *      RoundPhase 1 - User Phase
 *          Starting Player Turn (1)
 *          <NEXT TURN METHOD>
 *      RoundPhase 2 - Duel Phase
 *          Duel Subphase 1 - Bench Abilities
 *          Duel Subphase 2 - Defender defensive Abilities
 *          Duel Subphase 3 - Attacker Abilities
 *          Duel Subphase 4 - Defender offensive Abilities
 * Round (1)
 *      RoundPhase 1 - User Phase
 *          Enemy Turn (1)
 *          <NEXT TURN METHOD>
 *      RoundPhase 2 - Duel Phase
 *          Duel Subphases
 *          [...]
 *      RoundPhase 1 - User Phase
 *          Player Turn (2)
 *          <NEXT TURN METHOD>
 *      RoundPhase 2 - Duel Phase
 *          Duel Subphases
 *          [...]
 * Round (2)
 *      RoundPhase 1 - User Phase
 *          Enemy Turn (2)
 *          <NEXT TURN METHOD>
 *      RoundPhase 2 - Duel Phase
 *          Duel Subphases
 *          [...]
 *      RoundPhase 1 - User Phase
 *          Player Turn (3)
 *          <NEXT TURN METHOD>
 *      RoundPhase 2 - Duel Phase
 *          Duel Subphases
 *          [...]      
 * etc.
*/
public class TurnManager : MonoBehaviour
{
    public enum Users { Player, Enemy, None }; // "None" has to be always last option

    [SerializeField] TMP_Text currentPhaseText;
    [SerializeField] ButtonManager buttonManager;
    [SerializeField] Users startingUser;
    [Tooltip("If true, it will override 'startingUser' selection")]
    [SerializeField] bool randomStartingUser;
    [SerializeField] int numberOfPlayer = 2;
    [SerializeField] int numberOfRoundPhases = 2;

    [Header("Debug Only")]
    [SerializeField] int playerTurn = 0;
    [SerializeField] int enemyTurn = 0;
    [SerializeField] float currentGameRound = 0.5f;
    [SerializeField] Users whoIsPlaying;
    [SerializeField] Users lastUser;
    [SerializeField] int roundPhase = 1;
    [SerializeField] int duelPhase = 0;    

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        if (randomStartingUser) // choose random starting player  
        {
            var coinThrow = Random.Range(0, numberOfPlayer); 
            startingUser = (Users)coinThrow;
        } 

        whoIsPlaying = startingUser;
        TurnCounter(whoIsPlaying);

        currentPhaseText.text = "Starting round. " + whoIsPlaying + " turn";

        // helping tool for debugging. Delete or comment out later
        buttonManager.SetProceedButton(false);
    }

    public void NextTurn() // triggered by button pushed by Player (OnClick) or by AI script
    {
        // Debug.Log("Judge: " + whoIsPlaying + " finished turn.");

        // we need to have lastUser variable because of "None" user and
        // way how next player is calculated in DecidedWhoIsNowPlaying method
        lastUser = whoIsPlaying;

        // thats a basic tool to separate whole gameplay into the phases
        // if you want to the bug simply put buttonManager.SetProceedButton(true);
        // instead of Proceed();
        Proceed();
    }

    void NextRoundPhase()
    {
        buttonManager.SetProceedButton(false);

        if (roundPhase == 1) // User Phase (interactable)
        {         
            DecidingWhoIsNowPlaying(); // checking if it's Player or Enemy Turn
            TurnCounter(whoIsPlaying);
            SetPlayersButton(); // if player - enabling buttons
            
            currentPhaseText.text = Mathf.FloorToInt(currentGameRound) + " round. " + whoIsPlaying + " turn";
        }
        else if (roundPhase == 2) // Duel Phase (not interactable)
        {
            whoIsPlaying = Users.None; // because it's not interactable phase
            SetPlayersButton(); // disable player buttons


            if (CheckIfDuelIsPossible())
            {
                currentPhaseText.text = "Duel phase";
                DuelPhase();
            }
            else
            {
                currentPhaseText.text = "No duel";
            }
            RoundCounter();
            Proceed();            
        }
    }

    void DecidingWhoIsNowPlaying()
    {
        int nextUser_Int = (int)lastUser + 1;
        if (nextUser_Int >= numberOfPlayer) nextUser_Int = 0;
        whoIsPlaying = (Users)nextUser_Int;
    }

    void SetPlayersButton()
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
    }

    void RoundCounter()
    {
        currentGameRound += 0.5f;         
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
            //Debug.Log(card.gameObject.name + " is trying to trigger ability");

            if (card)
            {
                card.CM_TriggerAbility();
                Debug.Log(card.gameObject.name + " succesfully triggered its ability!");

            }
            else
            {
                Debug.LogError("There is no Card Object in Dueal Field");
            }            
        }

        Debug.Log("Judge: DuelPhase finished!");
    }

    // Get & Set 
    public void OnClick()
    {
        NextTurn();
    }

    public void Proceed()
    {
        roundPhase++;
        if (roundPhase > numberOfRoundPhases) roundPhase = 1; 
        NextRoundPhase();
    }

    public float GetCurrentGameRound()
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

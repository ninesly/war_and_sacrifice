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
    public enum Users { Player, Enemy };
    public enum DuelSubphases { Offensive, Defensive, Bench }

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
    [SerializeField] Users previousUser;
    [SerializeField] int roundPhase = 1;
    [SerializeField] int duelPhase = 0;

    DuelField[] duelfields;

    AI aiScript;

    void Start()
    {
        aiScript = FindObjectOfType<AI>();

        SetDuelFields();
        StartGame();
    }

    void SetDuelFields()
    {
        duelfields = FindObjectsOfType<DuelField>();
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
    } // this method communicate with ButtonManager script

    public void NextTurn() // triggered by button pushed by Player (OnClick) or by AI script
    {
         Debug.Log("Judge: " + whoIsPlaying + " finished turn.");

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

            if (aiScript) aiScript.Act();
        }
    }

    void DecidingWhoIsNowPlaying()
    {
        previousUser = whoIsPlaying; // storing previous user for Duel purposes
        // this method is universal and will work if there would be more users in future
        int nextUser_Int = (int)whoIsPlaying + 1;
        if (nextUser_Int >= numberOfPlayer) nextUser_Int = 0;
        whoIsPlaying = (Users)nextUser_Int;
    }

    void SetPlayersButton()
    {
        // enabling and diabling buttons depeneds on current phase
        // buttons are enabled only in interactible phase of Player

        if (whoIsPlaying == Users.Player)
        {
            buttonManager.SetButtons(true);
            return;
        }
        buttonManager.SetButtons(false);
    } // this method communicate with ButtonManager script

    void TurnCounter(Users actualUser)
    {
        // this methods works only with two players

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
        // it adds 0.5 because it's triggered after every Duel Phase
        currentGameRound += 0.5f;         
    }   

    bool CheckIfDuelIsPossible()
    {
        // checks if there is cards in each Duel Field
        // not universal, works only with two users

        if (!duelfields[0].CheckIfFieldLimitReached() || !duelfields[1].CheckIfFieldLimitReached())
        {
            Debug.Log("Judge: No duel. " + duelfields[0].gameObject.name + ": " + duelfields[0].CheckIfFieldLimitReached() + duelfields[1].gameObject.name + ": " + duelfields[1].CheckIfFieldLimitReached());
            return false;
        }

        Debug.Log("Judge: DUEL TIME!");
        return true;        
    } // this method communicate with DuelField script

    void DuelPhase()
    {
        // setting fighters
        CardSOManager attacker = FindFighterOfUser(whoIsPlaying);
        CardSOManager defender = FindFighterOfUser(previousUser);

        // Duel Subphase 1 - Bench Abilities

        // Duel Subphase 2 - Attacker Abilities
        RunAbility(attacker, DuelSubphases.Offensive);

        // Duel Subphase 3 - Defender defensive Abilities
        RunAbility(defender, DuelSubphases.Defensive);


        Debug.Log("Judge: Duel Phase finished!");
    }

    CardSOManager FindFighterOfUser(Users user)
    {
        foreach (DuelField duelField in duelfields)
        {
            var card = duelField.GetComponentInChildren<CardSOManager>();
            if (!card) Debug.LogError("There is no Card Object in Duel Field"); // that shouldnt happen, but let's have it just in case

            if (card.GetUserOfCard() == user) return card;
        }
        return null;
    }

    void RunAbility(CardSOManager fighter, DuelSubphases subphase)
    {
        fighter.CM_TriggerAbility(subphase);
        Debug.Log(fighter.gameObject.name + " succesfully triggered its ability!");
    } // this method communicate with CardSOManager script !!!!!!!!!!!!!!

    // Buttons and Triggers
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

    // Get & Set 

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

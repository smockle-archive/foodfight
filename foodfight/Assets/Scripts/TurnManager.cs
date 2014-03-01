using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour {

    public UnitScript[] playerTeam;
    public UnitScript[] enemyTeam;

    public Turn turn;

	// Use this for initialization
	void Start () {
        turn = Turn.PLAYER;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Effectively Update(), but Update() happens too often. 
    /// </summary>
    public void Handle()
    {
        if (isTurnOver()) nextTurn();
    }

    bool isTurnOver()
    {
        UnitScript[] teamToCheck = (turn == Turn.PLAYER) ? playerTeam : enemyTeam; //this needs to change if we end up having a third team (like FE does with allied units sometimes).
        
        foreach (UnitScript u in teamToCheck)
        {
            if (u.canMove) return false;
        }
        return true; 
    }

    void nextTurn()
    {
        //restore all moved/used units to their prior colors.
        Recolor();
        //swap turns
        turn = (turn == Turn.PLAYER) ? Turn.ENEMY : Turn.PLAYER;
        if (turn == Turn.PLAYER) nextRound(); //this should always be true. if we have first changed turns, and now are on the (primary) player's turn again, then we have completed a round.
        //maybe do some graphical stuff here.
    }

    void Recolor()
    {
        UnitScript[] teamToCheck = (turn == Turn.PLAYER) ? playerTeam : enemyTeam;
        TeamColorScript tcs = GameObject.FindObjectOfType<TeamColorScript>();

        foreach (UnitScript u in teamToCheck)
        {
            u.canMove = true;
        }

        if (tcs != null) tcs.Render();
        else Debug.Log("ERROR: No instance of TeamColorScript could be found. Either it got dissociated from the Scripts object in a git push/pull/sync somehow, or someone deleted it. "
            + "It is also theoretically possible that someone added another instance of TeamColorScript, since this method looks for a single object.");
    }

    void nextRound()
    {

    }

    public enum Turn
    {
        PLAYER, ENEMY
    }
}

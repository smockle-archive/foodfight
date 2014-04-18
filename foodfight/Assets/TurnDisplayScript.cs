using UnityEngine;
using System.Collections;

public class TurnDisplayScript : MonoBehaviour {

	public GUIText text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updateTurn(int turn) {
		const int playerTurn = 0;
		const int enemyTurn = 1;
		text.text = (turn == playerTurn ? "Blue's turn" : "Red's turn");
	}
}

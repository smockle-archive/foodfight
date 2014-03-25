using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GridElementScript))]
public class UnitScript : MonoBehaviour {

    // maybe change these back to standard scope instead of public? 
    // don't probably need to see these attributes once the game works...
    public int health;
    public int attack;

    public int move;
    public int attackr; //attack range (move range not included. default is 1.)

    public UnitScript defender;

    public bool canMove = true;

    void Start()
    {
        move = 2;
        attackr = 1;
    }

    public void Attack (UnitScript defender)
    {
        if(canAttack(defender)) defender.health -= this.attack;
		Debug.Log (defender.health);
		isGameOver ();
    }
    public void Move (int x, int y) //move to grid position (x,y)
    {
        if (canMove && Mathf.Abs(this.GetComponent<GridElementScript>().x - x) + Mathf.Abs(this.GetComponent<GridElementScript>().y - y) <= move)
        {
            this.GetComponent<GridElementScript>().x = x;
            this.GetComponent<GridElementScript>().y = y;
            canMove = false;
            // FIXME: coloring may also need to be handled by the turn manager
            this.gameObject.renderer.material.color = Color.gray;
            GameObject.FindObjectOfType<TurnManager>().Handle();
        }
    }

    bool canAttack(UnitScript defender)
    {
        //TODO: FILL THIS IN
        if (defender == null) return false;
        if (this.CompareTag(defender.tag)) return false;
        return true; 
    }

    void Update()
    {
        
        if (this.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

	bool isGameOver(){
		bool playerWon = true;
		bool playerLost = true;
		UnitScript[] allUnits = GameObject.FindObjectsOfType<UnitScript>();
		
		foreach (UnitScript u in allUnits)
		{
			if(u.tag == "Unit - Player" && u.health > 0){
				playerLost = false;
			}
			if(u.tag == "Unit - Enemy" && u.health > 0){
				playerWon = false;
			}
		}
		
		if (playerWon) {
			Application.LoadLevel ("menuScene");
			Debug.Log ("Won");
		}
		
		if (playerLost) {
			
			Application.LoadLevel ("menuScene");
			Debug.Log("Lost");		
		}
		return (playerWon || playerLost);
	}

    //void OnMouseUp()
    //{
    //    Debug.Log("Mouse up");
    //}
}

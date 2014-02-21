using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GridElementScript))]
public class UnitScript : MonoBehaviour {

    // maybe change these back to standard scope instead of public? 
    // don't probably need to see these attributes once the game works...
    public int health;
    public int attack;
    public int move;

    public UnitScript defender;

    bool canMove = true;

    void Attack (UnitScript defender)
    {
        if(canAttack(defender)) defender.health -= this.attack;
    }
    void Move (int x, int y) //move to grid position (x,y)
    {
        if (canMove && Mathf.Abs(this.GetComponent<GridElementScript>().x - x) + Mathf.Abs(this.GetComponent<GridElementScript>().y - y) <= move)
        {
            this.GetComponent<GridElementScript>().x = x;
            this.GetComponent<GridElementScript>().y = y;
        }
    }

    bool canAttack(UnitScript defender)
    {
        //TODO: FILL THIS IN
        if (defender == null) return false;
        return true; 
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            CursorScript c = GameObject.FindObjectOfType<CursorScript>();
            int x = c.x;
            int y = c.y;
            
            if (
                (this.GetComponent<GridElementScript>().x == x) //we're within the x-bounds of the unit
                &&
                (this.GetComponent<GridElementScript>().y == y) //we're within the y-bounds of the unit
            )
            {
                if (canAttack(this.defender))
                {
                    this.Attack(this.defender);
                    Debug.Log(this.defender.name + " took " + this.attack + " damage! Its health is now " + this.defender.health + ".");
                }
            }
        }
        if (this.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

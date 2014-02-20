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
        return true; 
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;

            //Debug.Log("We're at least firing this damn thing! x: " + x + ", y: " + y);
            //Debug.Log(this.name + " is at x: " + Camera.main.WorldToScreenPoint(this.transform.position).x + ", y: " + Camera.main.WorldToScreenPoint(this.transform.position).y);

            if (
                (Camera.main.WorldToScreenPoint(this.transform.position).x <= x) && (Camera.main.WorldToScreenPoint(this.transform.position).x + 200 >= x) //we're within the x-bounds of the unit
                &&
                (Camera.main.WorldToScreenPoint(this.transform.position).y <= y) && (Camera.main.WorldToScreenPoint(this.transform.position).y + 200 >= y) //we're within the y-bounds of the unit
            )
            {
                this.Attack(this.defender);
                Debug.Log(this.defender.name + " took " + this.attack + " damage! Its health is now " + this.defender.health + ".");
            }
        }
    }
}

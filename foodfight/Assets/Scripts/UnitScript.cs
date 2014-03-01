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
    bool isSelected = false;

    public int range
    {
        get
        {
            return move + attackr;
        }
    }

    void Start()
    {
        move = 2;
        attackr = 1;
    }

    void Attack (UnitScript defender)
    {
        if(canAttack(defender)) defender.health -= this.attack;
    }
    public void Move (int x, int y) //move to grid position (x,y)
    {
        if (canMove && Mathf.Abs(this.GetComponent<GridElementScript>().x - x) + Mathf.Abs(this.GetComponent<GridElementScript>().y - y) <= move)
        {
            this.GetComponent<GridElementScript>().x = x;
            this.GetComponent<GridElementScript>().y = y;
            canMove = false;
            // FIXME: this may need to be handled by the turn manager
            this.gameObject.renderer.material.color = Color.gray;
            GameObject.FindObjectOfType<TurnManager>().Handle();
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
        
        if (this.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    //void OnMouseUp()
    //{
    //    Debug.Log("Mouse up");
    //}
}

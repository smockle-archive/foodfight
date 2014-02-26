using UnityEngine;
using System.Collections;

public class CursorScript : GridElementScript {

    public Transform highlightSquare;
    public Color moveRangeColor;
    public Color attackRangeColor;

    UnitScript selected;
    bool existRangeObjects = false;

    void Start()
    {
        highlightSquare.tag = "Range";
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            SelectUnit();
        }

        if (selected != null && !existRangeObjects) DrawRanges();
        if (selected == null && existRangeObjects) DestroyRanges();
	}

    /// <summary>
    /// Selects a unit if the cursor is over a unit. If it's not over a unit, it deselects.
    /// </summary>
    void SelectUnit()
    {
        if (this.gameObject.transform.parent.gameObject.GetComponent<Grid>().isLegalBoardLocation(this.x, this.y))
        {
            if (selected == null)
            {
                //FIXME:
                //If the game runs slowly, we should fix this. I mostly just wanted to get something working first, but I know the solution (check Grid.cs).
                foreach (UnitScript u in FindObjectsOfType<UnitScript>())
                {
                    GridElementScript g = u.gameObject.GetComponent<GridElementScript>();
                    if (g.x == this.x && g.y == this.y)
                    {
                        selected = u;
                    }
                }
            }
            //if (canAttack(this.defender))
            //{
            //    this.Attack(this.defender);
            //    Debug.Log(this.defender.name + " took " + this.attack + " damage! Its health is now " + this.defender.health + ".");
            //}
        }
        else selected = null;
    }

    /// <summary>
    /// Draw the selected unit's move range, as well as attack range outside of that.
    /// </summary>
    void DrawRanges()
    {
        Debug.Log("Creating ranges for " + selected.gameObject.name + "...");

        existRangeObjects = true;
        GridElementScript g = selected.gameObject.GetComponent<GridElementScript>();

        Debug.Log("Coordinates of " + selected.gameObject.name + ": (" + g.x + "," + g.y + ")");
        DrawAttackRange(g);
        DrawMoveRange(g);
    }

    void DrawMoveRange(GridElementScript g)
    {

    }

    void DrawAttackRange(GridElementScript g)
    {
        highlightSquare.renderer.material.color = attackRangeColor;
        for(int y = g.y - (selected.move + selected.attackr); y < g.y + (selected.move + selected.attackr); y++){
            if (!this.gameObject.transform.parent.gameObject.GetComponent<Grid>().isLegalBoardLocation(0, y)) continue;
            Debug.Log(y);
            for(int x = g.x - (selected.move + selected.attackr - Mathf.Abs(y)); x <= g.x + (selected.move + selected.attackr - Mathf.Abs(y)); x++){
                Debug.Log("(" + x + "," + y + ")");
                if (!this.gameObject.transform.parent.gameObject.GetComponent<Grid>().isLegalBoardLocation(x, y)) continue;
                highlightSquare.GetComponent<GridElementScript>().x = x;
                highlightSquare.GetComponent<GridElementScript>().y = y;
                Instantiate(highlightSquare, Camera.main.GridToWorldPoint(new Vector3(x, y, 5), this.gameObject.transform.parent.gameObject.GetComponent<Grid>()), new Quaternion());
            }
        }
    }

    void DestroyRanges()
    {
        Debug.Log("Destroying ranges...");
        existRangeObjects = false;
        foreach (var r in GameObject.FindGameObjectsWithTag("Range"))
        {
            Destroy(r);
        }
    }
}

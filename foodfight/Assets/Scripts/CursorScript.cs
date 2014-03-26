using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CursorScript : GridElementScript {

    public Transform highlightSquare;
    public Color moveRangeColor;
    public Color attackRangeColor;

    UnitScript selected;
    Grid grid;
    GridElementScript[] moveRange;
    GridElementScript[] attackRange;
    GridElementScript clicked;
    GridElementScript attacked;
    TurnManager tm;

    void Start()
    {
        highlightSquare.tag = "Range";
        grid = this.gameObject.transform.parent.gameObject.GetComponent<Grid>();
        tm = GameObject.FindObjectOfType<TurnManager>();
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            moveRange = GameObject.Find("MoveRange").GetComponentsInChildren<GridElementScript>();
            attackRange = GameObject.Find("AttackRange").GetComponentsInChildren<GridElementScript>();

            clicked = null;
            attacked = null;
			

            foreach (GridElementScript ge in moveRange)
            {
                if (ge.x == this.x && ge.y == this.y)
                {
                    clicked = ge;
                }
            }

//			if (selected != null && this.x == selected.x && this.y == selected.y) {
//				clicked = 
//			}

            foreach (GridElementScript ge in attackRange)
            {
                if (ge.x == this.x && ge.y == this.y)
                {
                    foreach (UnitScript u in GameObject.FindObjectsOfType<UnitScript>())
                    {
                        GridElementScript g = u.gameObject.GetComponent<GridElementScript>();
                        if (g.x == this.x && g.y == this.y)
                        {
                            attacked = g;
                        }
                    }
                }
            }

            if (selected != null
                && attacked != null)
            {
                selected.Attack(attacked.gameObject.GetComponent<UnitScript>());
                DestroyRanges();
                selected = null;
            }

            else if (selected != null
                && clicked != null) // eventually, we'll also want to check if we're allowed to move this unit (e.g. it's moved already or is on the wrong team)
            {
                selected.Move(clicked.x, clicked.y);
                grid.Render(selected.gameObject.GetComponent<GridElementScript>());
                DestroyRanges();
                DrawAttackRange(selected.gameObject.GetComponent<GridElementScript>());
            }
            else
            {
                SelectUnit();

                if (selected != null)
                {
                    DestroyRanges();
                    //DrawRanges();
                    DrawMoveRange(selected.gameObject.GetComponent<GridElementScript>());
                }
                else DestroyRanges();
            }
        }
	}

    /// <summary>
    /// Selects a unit if the cursor is over a unit. If it's not over a unit, it deselects.
    /// </summary>
    void SelectUnit()
    {
        if (grid.isLegalBoardLocation(this.x, this.y))
        {
            selected = null;
            //FIXME:
            //If the game runs slowly, we should fix this. I mostly just wanted to get something working first, but I know the solution (check Grid.cs).
            foreach (UnitScript u in FindObjectsOfType<UnitScript>())
            {
                if (!u.canMove) continue;

                GridElementScript g = u.gameObject.GetComponent<GridElementScript>();
                if  (
                        g.x == this.x && g.y == this.y &&
                        (
                            (g.gameObject.tag == "Unit - Player" && tm.turn == TurnManager.Turn.PLAYER)
                            ||
                            (g.gameObject.tag == "Unit - Enemy" && tm.turn == TurnManager.Turn.ENEMY)
                        )
                    )
                {
                    selected = u;
                }
            }
        }
        else selected = null;
    }

    /// <summary>
    /// <para>Depricated. Use DrawAttackRange() and DrawMoveRange() individually.</para>
    /// Draws the selected unit's move range, as well as attack range outside of that.
    /// </summary>
    void DrawRanges()
    {
        GridElementScript g = selected.gameObject.GetComponent<GridElementScript>();

        DrawAttackRange(g);
        DrawMoveRange(g);
    }

    void DrawMoveRange(GridElementScript e)
    {
        int usedMoves = 0;
        bool skip = false;
        GridElementScript[] gridElements = GameObject.FindObjectsOfType<GridElementScript>();

        for (int y = e.y - selected.move; y <= e.y + selected.move; y++)
        {
            if (!grid.isLegalBoardLocation(0, y)) continue;

            usedMoves = Mathf.Abs(e.y - y);

            for (int x = e.x - (selected.move - usedMoves); x <= e.x + (selected.move - usedMoves); x++)
            {
                skip = false;
                foreach (GridElementScript ge in gridElements)
                {
                    if (!ge.CompareTag("Range") && ge.x == x && ge.y == y && !(e.y == y && e.x == x))
                    {
                        skip = true;
                    }
                }
                if (skip) continue;
                if (!grid.isLegalBoardLocation(x, y)) continue;
                
                highlightSquare.GetComponent<GridElementScript>().x = x;
                highlightSquare.GetComponent<GridElementScript>().y = y;
                Transform clone = (Transform)Instantiate(highlightSquare, Camera.main.GridToWorldPoint(new Vector3(x, y, 10), grid), new Quaternion());
                clone.renderer.material.color = moveRangeColor;
                clone.transform.parent = GameObject.Find("MoveRange").gameObject.transform;
            }
        }
		foreach (GridElementScript u in GameObject.Find("MoveRange").GetComponentsInChildren<GridElementScript>()) {
			Debug.Log(u.x + ", " + u.y);
		}
    }

    void DrawAttackRange(GridElementScript e)
    {
        int usedMoves = 0;

        for(int y = e.y - selected.attackr; y <= e.y + selected.attackr; y++){
            if (!grid.isLegalBoardLocation(0, y)) continue;

            usedMoves = Mathf.Abs(e.y - y);

            for (int x = e.x - (selected.attackr - usedMoves); x <= e.x + (selected.attackr - usedMoves); x++)
            {
                if (!grid.isLegalBoardLocation(x, y)) continue;

                highlightSquare.GetComponent<GridElementScript>().x = x;
                highlightSquare.GetComponent<GridElementScript>().y = y;
                Transform clone = (Transform)Instantiate(highlightSquare, Camera.main.GridToWorldPoint(new Vector3(x, y, 10), grid), new Quaternion());
                clone.renderer.material.color = attackRangeColor;
                clone.transform.parent = GameObject.Find("AttackRange").gameObject.transform;
            }
        }
    }

    /// <summary>
    /// Destroys any graphical objects that describe the attack range or move range of any unit.
    /// </summary>
    void DestroyRanges()
    {
        foreach (var r in GameObject.FindGameObjectsWithTag("Range"))
        {
            Destroy(r);
        }
    }
}

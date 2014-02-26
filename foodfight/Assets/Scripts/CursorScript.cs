using UnityEngine;
using System.Collections;

public class CursorScript : GridElementScript {

    public Transform highlightSquare;
    public Color moveRangeColor;
    public Color attackRangeColor;

    UnitScript selected;
    bool existRangeObjects = false;
    Grid grid;

    void Start()
    {
        highlightSquare.tag = "Range";
        grid = this.gameObject.transform.parent.gameObject.GetComponent<Grid>();
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            SelectUnit();

            if (selected != null)
            {
                DestroyRanges();
                DrawRanges();
            }
            else DestroyRanges();
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
                GridElementScript g = u.gameObject.GetComponent<GridElementScript>();
                if (g.x == this.x && g.y == this.y)
                {
                    selected = u;
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

        existRangeObjects = true;
        GridElementScript g = selected.gameObject.GetComponent<GridElementScript>();

        DrawAttackRange(g);
        DrawMoveRange(g);
    }

    void DrawMoveRange(GridElementScript e)
    {
        int usedMoves = 0;

        for (int y = e.y - selected.move; y <= e.y + selected.move; y++)
        {
            if (!grid.isLegalBoardLocation(0, y)) continue;

            usedMoves = Mathf.Abs(e.y - y);

            for (int x = e.x - (selected.move - usedMoves); x <= e.x + (selected.move - usedMoves); x++)
            {
                if (!grid.isLegalBoardLocation(x, y)) continue;

                highlightSquare.GetComponent<GridElementScript>().x = x;
                highlightSquare.GetComponent<GridElementScript>().y = y;
                Transform clone = (Transform)Instantiate(highlightSquare, Camera.main.GridToWorldPoint(new Vector3(x, y, 5), grid), new Quaternion());
                clone.renderer.material.color = moveRangeColor;
                clone.transform.parent = GameObject.Find("AttackRange").gameObject.transform;
            }
        }
    }

    void DrawAttackRange(GridElementScript e)
    {
        int usedMoves = 0;

        for(int y = e.y - selected.range; y <= e.y + selected.range; y++){
            if (!grid.isLegalBoardLocation(0, y)) continue;

            usedMoves = Mathf.Abs(e.y - y);

            for(int x = e.x - (selected.range - usedMoves); x <= e.x + (selected.range - usedMoves); x++){
                if (!grid.isLegalBoardLocation(x, y)) continue;

                highlightSquare.GetComponent<GridElementScript>().x = x;
                highlightSquare.GetComponent<GridElementScript>().y = y;
                Transform clone = (Transform)Instantiate(highlightSquare, Camera.main.GridToWorldPoint(new Vector3(x, y, 5), grid), new Quaternion());
                clone.renderer.material.color = attackRangeColor;
                clone.transform.parent = GameObject.Find("AttackRange").gameObject.transform;
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

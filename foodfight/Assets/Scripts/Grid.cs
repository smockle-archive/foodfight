using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    public int width;
    public int height;
    public int tileWidth;
    public int tileHeight;

    CursorScript cursor;

    // GridElementScript[][] mapClone; // so we don't have to foreach and search through all the GameObjects every time, as that is costly. use if noticeable lag occurs.

	// Use this for initialization
	void Start () {
        foreach (var e in GameObject.FindObjectsOfType<GridElementScript>())
        {
            if (!isLegalBoardLocation(e.x, e.y))
            {
                Debug.Log("ERROR: Illegal board position (" + e.x + "," + e.y + ") of unit " + e.gameObject.name);
                e.x = 0;
                e.y = 0;
            }
            e.gameObject.transform.position = Camera.main.GridToWorldPoint(new Vector3(e.x, e.y, e.gameObject.transform.position.z), this);
        }

        cursor = GameObject.FindObjectOfType<CursorScript>();
        if(cursor == null) Debug.Log("ERROR: Either no CursorScript object defined in scene, or more than one CursorScript object defined in scene.");
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 mouse = Input.mousePosition;
        Vector3 mouseOnGrid = Camera.main.ScreenToGridPoint((int)mouse.x, (int)mouse.y, this);

        if (
            mouseOnGrid.x != cursor.x
            ||
            mouseOnGrid.y != cursor.y
            )
        {
            if (!isLegalBoardLocation((int)mouseOnGrid.x, (int)mouseOnGrid.y))
            {
                cursor.x = -1;
                cursor.y = -1;
                cursor.gameObject.transform.position = Camera.main.GridToWorldPoint(-1, -1, this);
            }
            else
            {
                cursor.x = (int)mouseOnGrid.x;
                cursor.y = (int)mouseOnGrid.y;
                cursor.gameObject.transform.position = Camera.main.GridToWorldPoint(mouseOnGrid, this) + new Vector3(0, 0, 1); //to stay above the map
            }
        }
	}


    public bool isLegalBoardLocation(int x, int y)
    {
        return (x >= 0 && x < width) && (y >= 0 && y < height);
    }


}

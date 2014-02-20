using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    public int width;
    public int height;
    public int tileWidth;
    public int tileHeight;


	// Use this for initialization
	void Start () {
        foreach (var e in GameObject.FindObjectsOfType<GridElementScript>())
        {
            e.gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(e.x * tileWidth, e.y * tileHeight, e.gameObject.transform.position.z)) + new Vector3(1, 1);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    bool isLegalBoardLocation(int x, int y)
    {
        return (x >= 0 && x < width) && (y >= 0 && y < height);
    }


}

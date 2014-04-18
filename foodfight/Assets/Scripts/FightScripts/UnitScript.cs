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

    public int crit; //always defaults to 0! only one class should use this stat!

    public UnitScript defender;

    public bool canMove = true;

	public GUIText healthBar;
	public Vector3 offset = Vector3.up;    // Units in world space to offset; 1 unit above object by default
	Camera cam;


    void Start()
    {
		cam = Camera.main;
        //move = 2;
        //attackr = 1;
        //crit = 0;
    }

    public void Attack (UnitScript defender)
    {
        if(canAttack(defender)) defender.health -= (Random.Range(1, 100) <= this.crit) ? this.attack * 2 : this.attack;
        Debug.Log(defender.health);
        GameObject.FindObjectOfType<TurnManager>().Handle();
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

		
		GUI.color = Color.black;
		healthBar.text = "Health: " + this.health;
		healthBar.transform.position = cam.WorldToViewportPoint(this.transform.position + offset);


    }

	void OnGUI(){	
		//Debug.Log (this.transform.position.x);
		//Debug.Log (this.transform.position.y);
		//GUI.color = Color.black;
		//GUIStyle firstStyle = new GUIStyle();
		//firstStyle.padding = new RectOffset((int) this.transform.position.x, 0, (int) this.transform.position.y, 0);
		//GUILayout.BeginHorizontal (firstStyle);
		//GUILayout.Label("Health: " + this.health);		
		//GUILayout.EndHorizontal();

	}

    //void OnMouseUp()
    //{
    //    Debug.Log("Mouse up");
    //}
}

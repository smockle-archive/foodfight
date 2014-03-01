using UnityEngine;
using System.Collections;

public class TeamColorScript : MonoBehaviour {

    Color color;

	// Use this for initialization
	void Start () {

        foreach (var unitScript in GameObject.FindObjectsOfType<UnitScript>())
        {
            switch (unitScript.gameObject.tag)
            {
                case "Unit - Player":
                    color.r = .5f;
                    color.g = .5f;
                    color.b = 1;
                    color.a = 1;
                    break;
                case "Unit - Enemy":
                    color.r = 1;
                    color.g = .5f;
                    color.b = .5f;
                    color.a = 1;
                    break;
                default:
                    Debug.Log("ERROR: Object <" + unitScript.gameObject.name + "> has no affiliated team.");
                    color = Color.gray;
                    break;
            }
            unitScript.gameObject.renderer.material.color = color;
        }
	}

    public void Render()
    {
        Start();
    }
	
}

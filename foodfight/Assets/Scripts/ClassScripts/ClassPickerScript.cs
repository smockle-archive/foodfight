using UnityEngine;
using System.Collections;

public class ClassPickerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnGUI()
    {
        GUI.Box(new Rect(400, 10, 650, 400), "Choose an ethnic food class!");
        if (GUI.Button(new Rect(450, 50, 150, 100), Classes.America.adj))
        {
            PlayerPrefs.SetString("Class", Classes.America.name);
            Application.LoadLevel(2);
        }
        if (GUI.Button(new Rect(650, 50, 150, 100), Classes.Japan.adj))
        {
            PlayerPrefs.SetString("Class", Classes.Japan.name);
            Application.LoadLevel(2);
        }
        if (GUI.Button(new Rect(850, 50, 150, 100), Classes.France.adj))
        {
            PlayerPrefs.SetString("Class", Classes.France.name);
            Application.LoadLevel(2);
        }
    }
}

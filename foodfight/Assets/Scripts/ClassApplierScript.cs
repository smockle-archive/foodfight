using UnityEngine;
using System.Collections;

public class ClassApplierScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Class c;

        if(PlayerPrefs.GetString("Class").Equals(Classes.America.name)) {
            c = Classes.America;
        }
        else if(PlayerPrefs.GetString("Class").Equals(Classes.Japan.name)) {
            c = Classes.Japan;
        }
        else if(PlayerPrefs.GetString("Class").Equals(Classes.France.name)) {
            c = Classes.France;
        }
        else c = new Class ("Default", "Defaultian", 0, 0, 0, 0, 0);


        foreach (var unitScript in GameObject.FindObjectsOfType<UnitScript>())
        {
            if (unitScript.gameObject.tag == "Unit - Player")
            {
                unitScript.attack += c.a;
                unitScript.health += c.h;
                unitScript.move += c.m;
                unitScript.attackr += c.r;
                unitScript.crit += c.c;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class MenuButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit));
			if (hit.transform != null) {
				Debug.Log("Hit " + hit.transform.gameObject.name);
			}
			if(hit.transform.gameObject.name == "quit"){
			   Application.Quit();
			}
			if(hit.transform.gameObject.name == "new"){
				Application.LoadLevel ("testScene");
			}
		}
	}
}

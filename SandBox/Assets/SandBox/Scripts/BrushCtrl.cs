using UnityEngine;
using System.Collections;

public class BrushCtrl : MonoBehaviour {

    public TrashUI trashUI;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name);
		if(other.transform.name.ToLower().IndexOf("paper") != -1)
       	 trashUI.IncreaseShootNum();
    }

    void OnTriggerExit(Collider other)
    {
		if(other.transform.name.ToLower().IndexOf("paper") != -1)
        	trashUI.DecreaseShootNum();
    }
}

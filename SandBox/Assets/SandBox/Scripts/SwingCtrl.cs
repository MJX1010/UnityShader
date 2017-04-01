using UnityEngine;
using System.Collections;

public class SwingCtrl : MonoBehaviour {
    public Transform chair;

	// Use this for initialization
	void Start () {
	
	}
	
    void OnTriggerExit(Collider other)
    {
        if(other.transform == chair)
        {
            this.GetComponent<AudioSource>().Play();
        }
    }
}

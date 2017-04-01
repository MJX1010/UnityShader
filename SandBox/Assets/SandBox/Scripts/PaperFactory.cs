using UnityEngine;
using System.Collections;

public class PaperFactory : MonoBehaviour {

    public GameObject first;
    public Transform paper;

    private GameObject last;
    private bool forbidCreate;
	// Use this for initialization
	void Start () {
        last = first;
        forbidCreate = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject == last && !forbidCreate)
        {
            forbidCreate = true;
            Invoke("CreatePaper", 5f);
        }
    }

    void CreatePaper()
    {
        forbidCreate = false;
        last = GameObject.Instantiate(paper.gameObject, this.transform.position, this.transform.rotation) as GameObject;
    }
}

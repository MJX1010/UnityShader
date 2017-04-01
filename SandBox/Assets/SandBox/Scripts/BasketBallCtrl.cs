using UnityEngine;
using System.Collections;

public class BasketBallCtrl : MonoBehaviour {

    public Transform basketBall;
    private Rigidbody basketballRigidbody;
    // Use this for initialization
    void Start ()
    {
        basketballRigidbody = basketBall.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.name == "Palm_Left" || other.transform.name == "Palm_Right")
        {
            Debug.Log("jump!! basketball");
            Vector3 velocity = basketballRigidbody.velocity;
            basketballRigidbody.velocity = new Vector3(velocity.x, -Mathf.Abs(velocity.y) - 0.08f, velocity.z);
        }
    }
}

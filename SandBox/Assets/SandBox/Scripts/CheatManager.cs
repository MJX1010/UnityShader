using UnityEngine;
using System.Collections;
using Fingo;

public class CheatManager : MonoBehaviour {

    public Transform[] cheatObj = new Transform[9];
	// Use this for initialization
	void Start () {
        FingoGestureEvent.OnFingoGraspingPalmInfo += OnHandGrasping;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnHandGrasping(HandType handType, RigidHand myHand)
    {
        if(Input.GetKey((KeyCode.Q)))
        {
            GiveObjToHand(0, myHand);
        }
        else if(Input.GetKey(KeyCode.W))
        {
            GiveObjToHand(1, myHand);
        }
        else if(Input.GetKey(KeyCode.E))
        {
            GiveObjToHand(2, myHand);
        }
        else if(Input.GetKey(KeyCode.R))
        {
            GiveObjToHand(3, myHand);
        }
        else if(Input.GetKey(KeyCode.T))
        {
            GiveObjToHand(4, myHand);
        }
        else if(Input.GetKey(KeyCode.Y))
        {
            GiveObjToHand(5, myHand);
        }
        else if (Input.GetKey(KeyCode.U))
        {
            GiveObjToHand(6, myHand);
        }
        else if (Input.GetKey(KeyCode.I))
        {
            GiveObjToHand(7, myHand);
        }
        else if (Input.GetKey(KeyCode.O))
        {
            GiveObjToHand(8, myHand);
        }
    }

    void GiveObjToHand(int id, RigidHand myHand)
    {
        if (cheatObj[id] == null)
            return;

        RigidObject temp = cheatObj[id].GetComponent<RigidObject>();
        if(temp != null)
        {
            if (temp.GetComponent<ExplodeObj>() != null)
                if (temp.GetComponent<ExplodeObj>().ResetToHand(myHand))
                    return;

            myHand.GraspGameObjectInHand(temp);
        }
    }
}

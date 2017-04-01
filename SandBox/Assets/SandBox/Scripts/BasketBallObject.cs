using UnityEngine;
using System.Collections;

public class BasketBallObject : RigidObject {
    public GameObject fakeRHand;
    public GameObject fakeLHand;

	// Use this for initialization
	void Start () {
        fakeRHand.SetActive(false);
        fakeLHand.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
	
	}

    public override void GraspStart(RigidHand myHand)
    {
        base.GraspStart(myHand);
        if(myHand.meshHand.handType == Fingo.HandType.Right)
        {
            fakeRHand.SetActive(true);
        }
        else if(myHand.meshHand.handType == Fingo.HandType.Left)
        {
            fakeLHand.SetActive(true);
        }
    }

    public override void GraspEnd()
    {
        base.GraspEnd();
        fakeRHand.SetActive(false);
        fakeLHand.SetActive(false);
    }
}

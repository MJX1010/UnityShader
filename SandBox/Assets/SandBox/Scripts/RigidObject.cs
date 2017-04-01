using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RigidObject : MonoBehaviour {

	public bool isHideInGrasp;
	public Transform rightHoldPoistion;
	public Transform leftHoldPoistion;

    public bool isGrasped { get; set; }

    private Vector3 thisFramePosition;
    private Quaternion thisFrameRotation;
    private float thisFrameTimeStamp;

    private Vector3 lastFramePosition;
    private Quaternion lastFrameRotation;
    private float lastFrameTimeStamp;

	private Rigidbody rbody;
    private RigidHand myHand;

    protected Transform parentBackup;

    void Awake()
	{
        parentBackup = this.transform.parent;

		rbody = GetComponent<Rigidbody>();
        isGrasped = false;
	}

    void Update()
    {
        if(isGrasped)
        {
            lastFramePosition = thisFramePosition;
            lastFrameRotation = thisFrameRotation;
            lastFrameTimeStamp = thisFrameTimeStamp;

            thisFramePosition = this.transform.position;
            thisFrameRotation = this.transform.rotation;
            thisFrameTimeStamp = Time.time;
        }
    }

    public void HandGrasp(RigidHand myHand)
    {
        if (isGrasped)
            return;

        isGrasped = true;
        this.myHand = myHand;

        if (isHideInGrasp)
        {
            myHand.handRenderer.enabled = false;
        }

        GraspStart(myHand);
    }

    public void HandRelease()
    {
        if (!isGrasped)
            return;

        Debug.Log("Release " + this.transform.name);

        isGrasped = false;
        myHand.handRenderer.enabled = true;

        GraspEnd();
        Parabolic();
    }

    public virtual void GraspStart(RigidHand myHand )
	{
        this.transform.parent = myHand.transform;
		rbody.velocity = Vector3.zero;
		rbody.isKinematic = true;

        if (myHand.meshHand.handType == Fingo.HandType.Right)
        {
            if(rightHoldPoistion != null)
            {
                this.transform.localRotation = rightHoldPoistion.localRotation;
                this.transform.position = myHand.transform.position + (this.transform.position - rightHoldPoistion.position);
            }
        }
        else
        {
            if(leftHoldPoistion != null)
            {
                this.transform.localRotation = leftHoldPoistion.localRotation;
                this.transform.position = myHand.transform.position + (this.transform.position - leftHoldPoistion.position);
            }
        }

        thisFramePosition = this.transform.position;
        thisFrameRotation = this.transform.rotation;
        thisFrameTimeStamp = Time.time;
    }

    public virtual void GraspEnd()
	{
        this.transform.SetParent(parentBackup);
        rbody.isKinematic = false;
    }


	public virtual void Parabolic()
	{
		if (thisFrameTimeStamp - lastFrameTimeStamp == 0)
			return;

        float timeDelta = thisFrameTimeStamp - lastFrameTimeStamp;
		rbody.velocity = (thisFramePosition - lastFramePosition) / timeDelta;
		float angle = 0.0F;
		Vector3 axis = Vector3.zero;
		Quaternion delta = Quaternion.Inverse(lastFrameRotation) * thisFrameRotation;
		delta.ToAngleAxis(out angle, out axis);
		rbody.AddRelativeTorque(axis.normalized * angle / timeDelta);
	}
}

using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ComputerPowerPlugCtrl : RigidObject {

    public ElectricSource electricSource;
    public Transform screen;
    public Collider graspBoder;     //trigger should in true staus.

    private bool isPowerOn;
    private bool isOpenScreen;

    private RigidHand myHand;
    private Rigidbody rbody;
    private Vector3 originLocalScale;

    void Start()
    {
        isPowerOn = false;
        TurnScreenOpenOrClose(false);
        rbody = this.transform.GetComponent<Rigidbody>();
        originLocalScale = this.transform.localScale;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == electricSource.transform && !isPowerOn)
        {
            rbody.isKinematic = true;
            isPowerOn = true;

            if (myHand)
            {
                electricSource.SetUp(this, () => { myHand.ReleaseGameObject(); });
            }
            else
            {
                electricSource.SetUp(this, null);
            }

            Debug.Log("plug in");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other == graspBoder)
        {
            if (myHand)
            {
                myHand.ReleaseGameObject();
                this.transform.localScale = originLocalScale;
            }
        }
    }

    void TurnScreenOpenOrClose(bool isOpen)
    {
        this.isOpenScreen = isOpen;
        screen.gameObject.SetActive(isOpen);
    }

    public void OnPowerButtonClick()
    {
        if(isPowerOn)
            TurnScreenOpenOrClose(!isOpenScreen);
    }

    public override void GraspStart(RigidHand myHand)
    {
        base.GraspStart(myHand);

        this.transform.DOKill();
        this.myHand = myHand;
        isPowerOn = false;

        TurnScreenOpenOrClose(false);
        electricSource.HighLight();
    }

    public override void GraspEnd()
    {
        if(!isPowerOn)
        {
            this.GetComponent<Collider>().isTrigger = false;
            base.GraspEnd();
        }

        this.myHand = null;
        electricSource.DisHighLight();
    }

    public override void Parabolic()
    {
        if (!isPowerOn)
            base.GraspEnd();
    }
}

using NetMQ.zmq;
using UnityEngine;
using System.Collections;

public class ColaPotCtrl :  RigidObject
{
    public Transform pullTab;
    public ColaBubble bubble;
    public float pullTabOpenAngleX;
    public float pullTabOpenSpeed;

    private bool doOpen;
    private bool isOpened;

    private bool hasGushAtFirst;
    private bool isBubbleShowed;
    private bool isLiguidShowed;
    private Vector3 pullTabOriginalAngle;
    private Vector3 pullTabRotAngle;

    private void Start()
    {
        doOpen = false;
        isOpened = false;
        hasGushAtFirst = false;

        pullTabOriginalAngle = pullTab.localRotation.eulerAngles;
        pullTabRotAngle = 
            new Vector3(pullTabOriginalAngle.x + pullTabOpenAngleX,pullTabOriginalAngle.y,pullTabOriginalAngle.z);

        BubbleShowOut(false);
        BubbleLiquidShow(true);

        bubble.BubblePourHandler += OnBubblePour;
        bubble.BubbleStillHandler += OnBubbleStill;
    }

    private void Update()
    {
        if (!bubble.IsEmpty)
        {
            if (isOpened)
            {
                if (!hasGushAtFirst) 
                {
                    StartCoroutine("WaitBubbleGushAtFirst");
                }
                
            }
            else
            {
                PullTabOpen();
            }
        }
        else
        {
            if (isLiguidShowed)
            {
                BubbleShowOut(false);
                BubbleLiquidShow(false);
            }
        }
    }

    private void OnEnable()
    {
        BubbleShowOut(true);
    }

    private void OnDisable()
    {
        BubbleShowOut(false);
    }

    public override void GraspStart(RigidHand myHand)
    {
        base.GraspStart(myHand);
        doOpen = true;
    }

    public override void GraspEnd() 
    {
        base.GraspEnd();
        doOpen = false;
    }

    private void PullTabOpen()
    {
        if (doOpen)
        {
            pullTab.localRotation = Quaternion.RotateTowards(pullTab.localRotation,
                Quaternion.Euler(pullTabRotAngle),Time.deltaTime*pullTabOpenSpeed);
            if (pullTab.localRotation == Quaternion.Euler(pullTabRotAngle))
            {
                //TODO 播放拉环开启音频
                isOpened = true;
            }
        }
    }

    private void ResetPullTab()
    {
        pullTab.localRotation = Quaternion.Euler(pullTabOriginalAngle);
        doOpen = false;
        isOpened = false;
        BubbleShowOut(false);
        BubbleLiquidShow(true);
    }

    private void BubbleShowOut(bool isShow)
    {
        this.isBubbleShowed = isShow;
        if (isShow)
        {
            bubble.EmitBubble();
        }
        else
        {
            bubble.StopBubble();
        }
    }

    private void BubbleLiquidShow(bool isShow)
    {
        this.isLiguidShowed = isShow;
        if (bubble)
            bubble.ShowBubbleLiquid(isShow);
    }

    private IEnumerator WaitBubbleGushAtFirst()
    {
        if (!isBubbleShowed) {
            BubbleShowOut(true);
            yield return new  WaitForSecondsRealtime(1f);
            hasGushAtFirst = true;
            BubbleShowOut(false);
            if(bubble)
               bubble.UpdatePS();
        }
    }

    private void OnBubblePour()
    {
        if (hasGushAtFirst)
        {
            BubbleShowOut(true);
        }
    }

    private void OnBubbleStill()
    {
        if (hasGushAtFirst) 
        {
            BubbleShowOut(false);
        }
    }
}

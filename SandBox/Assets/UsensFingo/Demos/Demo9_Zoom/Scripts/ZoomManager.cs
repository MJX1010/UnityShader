/*************************************************************************\
*                           USENS CONFIDENTIAL                            *
* _______________________________________________________________________ *
*                                                                         *
* [2014] - [2017] USENS Incorporated                                      *
* All Rights Reserved.                                                    *
*                                                                         *
* NOTICE:  All information contained herein is, and remains               *
* the property of uSens Incorporated and its suppliers,                   *
* if any.  The intellectual and technical concepts contained              *
* herein are proprietary to uSens Incorporated                            *
* and its suppliers and may be covered by U.S. and Foreign Patents,       *
* patents in process, and are protected by trade secret or copyright law. *
* Dissemination of this information or reproduction of this material      *
* is strictly forbidden unless prior written permission is obtained       *
* from uSens Incorporated.                                                *
*                                                                         *
\*************************************************************************/

using UnityEngine;
using System.Collections;
using Fingo;

/// <summary>
/// Use two hands pinch gesture to drag object nearer or farther.
/// </summary>
public class ZoomManager : MonoBehaviour 
{
    private Hand leftHand;
    private Hand rightHand;

    private bool currentFrameHandTracked = false;
    private bool lastFrameHandTracked = false;

    private float currentFrameDistance;
    private float lastFrameDistance;
    private float displacement;

    public float zoomGestureThreshold = 0.04f;

    public delegate void ZoomEventHandler(float displacement);
    public static event ZoomEventHandler ZoomEvent;

    public delegate void EnterZoomEventHandler();
    public static event EnterZoomEventHandler EnterZoomEvent;

    public delegate void QuitZoomEventHandler();
    public static event QuitZoomEventHandler QuitZoomEvent;

    void OnEnable()
    {
    }
	
	void Update () 
    {
        leftHand = FingoMain.Instance.GetHand(HandType.Left);
        rightHand = FingoMain.Instance.GetHand(HandType.Right);
        
        CalculateRelativeDistance();
	}

    void CalculateRelativeDistance()
    {
        if (DetermineZoomGesture(leftHand) && DetermineZoomGesture(rightHand))
        {
            currentFrameHandTracked = true;
            currentFrameDistance = rightHand.GetJointPosition(JointIndex.IndexProximal).x - leftHand.GetJointPosition(JointIndex.IndexProximal).x;
        }
        else
        {
            currentFrameHandTracked = false;
            displacement = 0.0f;
        }

        if (currentFrameHandTracked && lastFrameHandTracked)
        {
            displacement = currentFrameDistance - lastFrameDistance;

            if (ZoomEvent != null)
            {
                ZoomEvent(displacement);
            }
        }

        lastFrameDistance = currentFrameDistance;
        lastFrameHandTracked = currentFrameHandTracked;
    }

    bool DetermineZoomGesture(Hand hand)
    {
        if (hand.IsDetected() && Vector3.Distance(hand.GetTipPosition(TipIndex.ThumbTip), hand.GetTipPosition(TipIndex.IndexTip)) < zoomGestureThreshold)
        {
            if (EnterZoomEvent != null)
            {
                EnterZoomEvent();
            }
            return true;
        }
        else
        {
            if (QuitZoomEvent != null)
            {
                QuitZoomEvent();
            }
        }
        return false;
    }
}

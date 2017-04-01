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
/// Trigger the swiping events if the hand is swiping.
/// Note that the swiping action is tightly related to the center of the display screen. 
/// </summary>
public class SwipeHand : MonoBehaviour 
{
    /// <summary>
    /// The hand type of the tracked hand.
    /// </summary>
    public HandType handType;

    private Hand hand;

    public GameObject centerPoint;
    /// <summary>
    /// The offset on the left side of the screen center.
    /// </summary>
    public float leftOffset = 100f;
    /// <summary>
    /// The offset on the right side of the screen center.
    /// </summary>
    public float rightOffset = 100f;
    /// <summary>
    /// The offset on the up side of the screen center.
    /// </summary>
    public float upOffset = 80f;
    /// <summary>
    /// The offset on the down side of the screen center.
    /// </summary>
    public float downOffset = 80f;

    /// <summary>
    /// The maximum time to detect a swipe.
    /// </summary>
    public float swipeTimeThreshold = 3f;

    private float timeLeftSide;
    private float timeRightSide;
    private float timeUpSide;
    private float timeDownSide;

    private Vector3 center;
    private Vector3 handPos;
    
    private float leftMarkPoint;
    private float rightMarkPoint;
    private float upMarkPoint;
    private float downMarkPoint;

    /// <summary>
    /// Swipe event.
    /// </summary>
    public delegate void SwipeEventHandler(HandType handType, SwipeType swipeType);
    public static event SwipeEventHandler SwipeEvent;

   
    void OnEnable()
    {
        hand = new Hand();
    }

    void Update()
    {
        hand = FingoMain.Instance.GetHand(handType);

        if (hand.IsDetected())
        {

            RecordTime();
        }

        else
        {
            ClearHorizontalTimeRecord();
            ClearVerticalTimeRecord();
        }

        DetermineSwipe();
    }

    void RecordTime()
    {
        handPos = Camera.main.WorldToScreenPoint(hand.GetPalmPosition());
        center = Camera.main.WorldToScreenPoint(centerPoint.transform.position);

        leftMarkPoint = center.x - leftOffset;
        rightMarkPoint = center.x + rightOffset;

        downMarkPoint = center.y - downOffset;
        upMarkPoint = center.y + upOffset;

        if (handPos.x > rightMarkPoint)
        {
            if (hand.GetGestureName() == GestureName.Grab || hand.GetGestureName() == GestureName.Palm)
            {
                timeRightSide = Time.time;
            }
            else
            {
                timeRightSide = 0f;
            }
        }

        if (handPos.x < leftMarkPoint)
        {
            if (hand.GetGestureName() == GestureName.Grab || hand.GetGestureName() == GestureName.Palm)
            {
                timeLeftSide = Time.time;
            }
            else
            {
                timeLeftSide = 0f;
            }
        }

        if (handPos.y > upMarkPoint)
        {
            if (hand.GetGestureName() == GestureName.Grab || hand.GetGestureName() == GestureName.Palm)
            {
                timeUpSide = Time.time;
            }
            else
            {
                timeUpSide = 0f;
            }
        }

        if (handPos.y < downMarkPoint)
        {
            if (hand.GetGestureName() == GestureName.Grab || hand.GetGestureName() == GestureName.Palm)
            {
                timeDownSide = Time.time;
            }
            else
            {
                timeDownSide = 0f;
            }
        }
    }

    void DetermineSwipe()
    {
        if (timeRightSide > 0.0f && timeLeftSide > 0.0f)
        {
            if (timeRightSide > timeLeftSide && (timeRightSide - timeLeftSide) < swipeTimeThreshold)
            {
                if (SwipeEvent != null)
                {
                    SwipeEvent(this.handType, SwipeType.Right);
                }

                ClearHorizontalTimeRecord();
            }

            if (timeLeftSide > timeRightSide && (timeLeftSide - timeRightSide) < swipeTimeThreshold)
            {
                if (SwipeEvent != null)
                {
                    SwipeEvent(this.handType, SwipeType.Left);
                }

                ClearHorizontalTimeRecord();
            }           
        }

        if (timeUpSide > 0.0f && timeDownSide > 0.0f)
        {         
            if (timeUpSide > timeDownSide && (timeUpSide - timeDownSide) < swipeTimeThreshold)
            {                    
                if (SwipeEvent != null)
                {
                    SwipeEvent(this.handType, SwipeType.Up);
                }
                    
                ClearVerticalTimeRecord();
            }

            if (timeDownSide > timeUpSide && (timeDownSide - timeUpSide) < swipeTimeThreshold)
            {                   
                if (SwipeEvent != null)
                {
                    SwipeEvent(this.handType, SwipeType.Down);
                }
                    
                ClearVerticalTimeRecord();
            }
        }                    
    }

    void ClearHorizontalTimeRecord()
    {
        timeRightSide = 0.0f;
        timeLeftSide = 0.0f;
    }

    void ClearVerticalTimeRecord()
    {
        timeUpSide = 0.0f;
        timeDownSide = 0.0f;
    }
}

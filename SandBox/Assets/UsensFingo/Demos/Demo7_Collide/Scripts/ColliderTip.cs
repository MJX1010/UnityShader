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
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Fingo;

/// <summary>
/// This class controls a collider that triggers clicking events. 
/// The tracked tip or joint can be modified in method TipPositionTracking.
/// </summary>
public class ColliderTip : MonoBehaviour 
{
    /// <summary>
    /// The hand type of the tracked hand.
    /// </summary>
    public HandType handType;

    private Hand hand;

    private Vector3 currentTipPos;
    private Vector3 lastTipPos;
    private Vector3 displacement;

    private GestureName currentGesture;

    private float collideCounter;
    /// <summary>
    /// The minimum frame interval between two collision events, in second.
    /// </summary>
    public float collideInterval = 0.5f;

    private float collideSpeed;
    /// <summary>
    /// The minimum collision speed to trigger a collision event.
    /// </summary>
    public float collideSpeedThreshold = 0.2f;

    private bool currentFrameFingersTracked;
    private bool lastFrameFingersTracked;

    private bool isCollidingForward;
    private bool isCollidingGesture;

    void OnTriggerEnter(Collider other)
    {
        FingoStandardButton button = other.gameObject.GetComponent<FingoStandardButton>();

        if (button != null && button.OnEnter != null)
        {
            button.OnEnter.Invoke();
        }

        if (button != null && button.OnCollide != null)
        {
            if (isCollidingForward && isCollidingGesture && collideCounter > collideInterval)
            {
                button.OnCollide.Invoke();
                collideCounter = 0;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        FingoStandardButton button = other.gameObject.GetComponent<FingoStandardButton>();

        if (button != null && button.OnExit != null)
        {
            button.OnExit.Invoke();
        }
    }

    void OnEnable()
    {
        hand = new Hand();
    }

    void Update()
    {
        hand = FingoMain.Instance.GetHand(handType);

        collideCounter += Time.deltaTime;

        TipPositionTracking();

        ClickSpeedTracking();

        ClickGestureTracking();
    }

    void TipPositionTracking()
    {
        if (hand.IsDetected())
        {
            currentFrameFingersTracked = true;
            currentTipPos = hand.GetTipPosition(TipIndex.IndexTip);
        }
        else
        {
            currentFrameFingersTracked = false;
            currentTipPos = Vector3.zero;
        }

        this.transform.localPosition = currentTipPos;
    }

    void ClickSpeedTracking()
    {
        if (currentFrameFingersTracked && lastFrameFingersTracked)
        {
            displacement = currentTipPos - lastTipPos;
        }

        collideSpeed = displacement.z / Time.deltaTime;

        if (collideSpeed > collideSpeedThreshold)
        {
            isCollidingForward = true;
        }
        else
        {
            isCollidingForward = false;
        }

        lastFrameFingersTracked = currentFrameFingersTracked;
        lastTipPos = currentTipPos;
    }

    void ClickGestureTracking()
    {
        if (hand.IsDetected())
        {
            currentGesture = hand.GetGestureName();
        }
        else
        {
            currentGesture = GestureName.None;
        }

        if (currentGesture == GestureName.Point || currentGesture == GestureName.ShootEm || 
            currentGesture == GestureName.Peace || currentGesture == GestureName.MiddleFinger)
        {
            isCollidingGesture = true;
        }
        else
        {
            isCollidingGesture = false;
        }
    }
}

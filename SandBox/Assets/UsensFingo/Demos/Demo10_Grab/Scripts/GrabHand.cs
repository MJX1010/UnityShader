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

public class GrabHand : MonoBehaviour
{
    /// <summary>
    /// The hand type of the tracked hand.
    /// </summary>
    public HandType handType;

    private Hand hand;

    /// <summary>
    /// If the distance between the middle finger tip and the middle finger proximal joint 
    /// is smaller than this value, start grabbing.
    /// </summary>
    public float grabStartThreshold = 0.04f;
    /// <summary>
    /// If the distance between the grabbable object and the hand is smaller than this value,
    /// the object is able to be grabbed.
    /// </summary>
    public float interactionDistanceThreshold = 0.07f;
    /// <summary>
    /// If the distance between the middle finger and the middle finger proximal joint is larger than this value, stop pinching.
    /// </summary>
    public float grabEndThreshold = 0.07f;
    /// <summary>
    /// If the distance between the middle finger and the middle finger proximal joint 10 frames before is larger than this value, 
    /// the hand is able to grab, or the hand is disable to grab. 
    /// </summary>
    public float grabEnableThreshold = 0.05f;

    private Vector3 middleTipPos;
    private Vector3 middleProximalPos;             

    private float currentFingerDist;
    private float previousFingerDist;

    private Vector3 currentPalmPos;
    private Vector3 previousPalmPos;
    private float[] distArray = new float[10];
    private Vector3[] indexPos = new Vector3[10];

    private int grabFrameInterval = 9;

    private GrabbaleObject grabbedObject = null; //!< The object which is grabbed in hand.
    private GrabbaleObject reachableObject = null;
    private Transform grabbedObjectParent = null;

    private Vector3 speed;

    void OnEnable()
    {
        hand = new Hand();
    }

    void Update()
    {
        hand = FingoMain.Instance.GetHand(handType);

        if (hand.IsDetected())
        {
            LocateGrabPoint();

            GrabDistance();

            GrabObject();
        }
    }

    void LocateGrabPoint()
    {
        middleTipPos = hand.GetTipPosition(TipIndex.MiddleTip);
        middleProximalPos = hand.GetJointPosition(JointIndex.MiddleProximal);

        this.transform.localPosition = Vector3.Lerp(middleTipPos, middleProximalPos, .3f);
        this.transform.localRotation = hand.GetJointLocalRotation(JointIndex.WristJoint);
    }

    void GrabDistance()
    {
        currentFingerDist = Vector3.Distance(middleTipPos, middleProximalPos);
        currentPalmPos = hand.GetPalmPosition();

        for (int i = 0; i < grabFrameInterval; i++)
        {
            distArray[i] = distArray[i + 1];
            indexPos[i] = indexPos[i + 1];
        }

        previousFingerDist = distArray[0];
        previousPalmPos = indexPos[0];

        distArray[9] = currentFingerDist;
        indexPos[9] = currentPalmPos;

        speed = (currentPalmPos - previousPalmPos) / (Time.deltaTime * 10);
    }

    void GrabObject()
    {
        Vector3 middlePoint = this.transform.position;
        // Iterate the grabbable objects, find the closest one in range and start to grab it.
        foreach (GrabbaleObject obj in GrabbaleObject.instances)
        {
            if (Vector3.Distance(middlePoint, obj.transform.position) < interactionDistanceThreshold && previousFingerDist > grabEnableThreshold)
            {
                reachableObject = obj;
                if (grabbedObject == null)
                {
                    reachableObject.OnGetInReach.Invoke();
                    if (currentFingerDist < grabStartThreshold)
                    {
                        grabbedObject = obj;
                        grabbedObject.OnGrab.Invoke();
                        grabbedObjectParent = grabbedObject.transform.parent;
                        grabbedObject.transform.parent = this.transform;
                        grabbedObject.transform.localPosition = Vector3.zero;
                        break;
                    }
                }
            }
            else
            {
                if (grabbedObject != null) {
                    if (currentFingerDist > grabEndThreshold)
                    {
                        grabbedObject.transform.parent = grabbedObjectParent;
                        grabbedObject.OnRelease.Invoke();
                        grabbedObject.OnThrow.Invoke(this.speed);
                        grabbedObject = null;
                    }
                }
                else
                {
                    if(reachableObject == obj)
                    {
                        reachableObject.OnOutOfReach.Invoke();
                        reachableObject = null;
                    }
                }
            }
        }
    }
}

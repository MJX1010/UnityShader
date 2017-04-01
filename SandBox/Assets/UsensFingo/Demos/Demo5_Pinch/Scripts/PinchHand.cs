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
/// Make the hand able to pinch objects.
/// </summary>
public class PinchHand : MonoBehaviour 
{
    public MeshHand meshHand;
    /// <summary>
    /// The hand type of the tracked hand.
    /// </summary>
    public HandType handType;

    private Hand hand;

    /// <summary>
    /// If the distance between two fingers is smaller than this value, start pinching.
    /// </summary>
    public float pinchStartThreshold = 0.04f;
    /// <summary>
    /// If the distance between the pinchable object and the hand is smaller than this value,
    /// the object can be pinched.
    /// </summary>
    public float interactionDistanceThreshold = 0.07f;
    /// <summary>
    /// If the distance between two fingers is larger than this value, stop pinching.
    /// </summary>
    public float pinchEndThreshold = 0.07f;
    /// <summary>
    /// If the distance between two fingers 10 frames before is larger than this value, the hand is able to pinch,
    /// or the hand is disable to pinch.
    /// </summary>
    public float pinchEnableThreshold = 0.05f;

    private Vector3 thumbTipPos;        
    private Vector3 indexTipPos;        

    private Vector3 meshHandThumbTipPos;        
    private Vector3 meshHandIndexTipPos;        

    private float currentFingerDist;
    private float previousFingerDist;

    private Vector3 currentIndexPos;
    private Vector3 previousIndexPos;
    private float[] distArray = new float[10];
    private Vector3[] indexPos = new Vector3[10];

    private int pinchFrameInterval = 9;

    private PinchableObject pinchedObject = null; //!< The object which is pinched in hand.
    private Transform pinchedObjectParent = null;

    private Vector3 speed;

    void OnEnable()
    {
        hand = new Hand();
    }
	
	void Update () 
    {
        hand = FingoMain.Instance.GetHand(handType);

        if (hand.IsDetected())
        {
            LocatePinchPoint();

            PinchDistance();

            PinchObject();
        }
	}
    /*---------------------------------------------------------------------------------------------------*/
    void LocatePinchPoint()
    {
        thumbTipPos = hand.GetTipPosition(TipIndex.ThumbTip);
        indexTipPos = hand.GetTipPosition(TipIndex.IndexTip);

        meshHandThumbTipPos = meshHand.thumbTip.position;
        meshHandIndexTipPos = meshHand.indexTip.position;

        this.transform.position = Vector3.Lerp(meshHandThumbTipPos, meshHandIndexTipPos, .5f);
        this.transform.localRotation = hand.GetJointLocalRotation(JointIndex.WristJoint);
    }
    /*---------------------------------------------------------------------------------------------------*/
    void PinchDistance()
    {
        currentFingerDist = Vector3.Distance(thumbTipPos, indexTipPos);
        currentIndexPos = hand.GetJointPosition(JointIndex.IndexProximal);

        for (int i = 0; i < pinchFrameInterval; i++)
        {
            distArray[i] = distArray[i + 1];
            indexPos[i] = indexPos[i + 1];
        }

        previousFingerDist = distArray[0];
        previousIndexPos = indexPos[0];

        distArray[9] = currentFingerDist;
        indexPos[9] = currentIndexPos;

        speed = (currentIndexPos - previousIndexPos) / (Time.deltaTime * 10);
    }
    /*---------------------------------------------------------------------------------------------------*/
    void PinchObject()
    {
        if (currentFingerDist < pinchStartThreshold)
        {
            if (pinchedObject == null)
            {
                Vector3 middlePoint = this.transform.position;
                /// Iterate the cubes, find the closest one in range and start to pinch
                foreach (PinchableObject obj in PinchableObject.instances)
                {
                    if (Vector3.Distance(middlePoint, obj.transform.position) < interactionDistanceThreshold && previousFingerDist > pinchEnableThreshold)
                    {
                        pinchedObject = obj;
                        pinchedObject.OnPickUp.Invoke();

                        pinchedObjectParent = pinchedObject.transform.parent;
                        pinchedObject.transform.parent = this.transform;
                        pinchedObject.transform.localPosition = Vector3.zero;

                        break;
                    }
                }
            }
        }

        else
        {
            if (currentFingerDist > pinchEndThreshold)
            {
                if (pinchedObject != null)
                {
                    pinchedObject.transform.parent = pinchedObjectParent;
                    pinchedObject.OnRelease.Invoke();
                    pinchedObject.OnThrow.Invoke(this.speed);
                    pinchedObject = null;
                }
            }
        }
    }
    /*---------------------------------------------------------------------------------------------------*/
}

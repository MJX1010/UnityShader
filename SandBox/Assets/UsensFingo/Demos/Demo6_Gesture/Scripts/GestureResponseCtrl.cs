/*************************************************************************\
*                           USENS CONFIDENTIAL                            *
* _______________________________________________________________________ *
*                                                                         *
* [2015] - [2017] USENS Incorporated                                      *
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
using System.Collections.Generic;
using UnityEngine.Events;
using Fingo;

public class GestureResponseCtrl : MonoBehaviour
{

    public UnityEvent OnRightHandOkayGesture;
    public UnityEvent OnLeftHandOkayGesture;

    public UnityEvent OnRightHandPeaceSignGesture;
    public UnityEvent OnLeftHandPeaceSignGesture;

    public UnityEvent OnRightHandFistGesture;
    public UnityEvent OnLeftHandFistGesture;

	public UnityEvent OnRightHandPalmGesture;
	public UnityEvent OnLeftHandPalmGesture;

    void OnEnable()
    {
		/*
        if (OnRightHandOkayGesture == null)
        {
            OnRightHandOkayGesture = new UnityEvent();
        }

        if (OnRightHandPeaceSignGesture == null)
        {
            OnRightHandPeaceSignGesture = new UnityEvent();
        }

		if (OnRightHandFistGesture == null)
        {
			OnRightHandFistGesture = new UnityEvent();
        }
*/
        GestureManager.GestureEvent += ColorChange;
    }

    public void ColorChange(HandType handType, GestureName gestureType)
    {
        if (handType == HandType.Right)
        {
            if (gestureType == GestureName.Okay)
            {
                OnRightHandOkayGesture.Invoke();
            }
			else if (gestureType == GestureName.Peace)
            {
                OnRightHandPeaceSignGesture.Invoke();
            }
			else if (gestureType == GestureName.Fist)
            {
				OnRightHandFistGesture.Invoke();
			}
			else if(gestureType == GestureName.Palm)
			{
				OnRightHandPalmGesture.Invoke();
			}
        }
        else
        {
            if (gestureType == GestureName.Okay)
            {
                OnLeftHandOkayGesture.Invoke();
            }
			else if (gestureType == GestureName.Peace)
            {
                OnLeftHandPeaceSignGesture.Invoke();
            }
			else if (gestureType == GestureName.Fist)
            {
				OnLeftHandFistGesture.Invoke();
			}
			else if(gestureType == GestureName.Palm)
			{
				OnLeftHandPalmGesture.Invoke();
			}
        }
    }
}

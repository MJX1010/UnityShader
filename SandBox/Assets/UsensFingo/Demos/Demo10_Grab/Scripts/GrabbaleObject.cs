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
using System.Collections.Generic;
using UnityEngine.Events;

public class GrabbaleObject : MonoBehaviour
{
    public static List<GrabbaleObject> instances = new List<GrabbaleObject>();

    public UnityEvent OnGetInReach;
    public UnityEvent OnOutOfReach;
    public UnityEvent OnGrab;
    public UnityEvent OnRelease;
    public SpeedEvent OnThrow;

    void OnEnable()
    {
        instances.Add(this);

        if (OnGetInReach == null)
        {
            OnGetInReach = new UnityEvent();
        }

        if (OnOutOfReach == null)
        {
            OnOutOfReach = new UnityEvent();
        }

        if (OnGrab == null)
        {
            OnGrab = new UnityEvent();
        }

        if (OnRelease == null)
        {
            OnRelease = new UnityEvent();
        }

        if (OnThrow == null)
        {
            OnThrow = new SpeedEvent();
        }
    }

    void OnDisable()
    {
        instances.Remove(this);
    }
}

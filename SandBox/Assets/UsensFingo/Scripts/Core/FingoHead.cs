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

namespace Fingo
{
    /// <summary>
    /// FingoHead is a script controlling the transform of the game object
    /// which contain a Camera component to move according to the data from
    /// Fingo positional tracking data.
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class FingoHead : MonoBehaviour
    {
        [Tooltip("The moving scale of head movement.")]
        public float HeadMovementScale = 100.0f; //!< The moving scale of head movement.
        [Tooltip("The button triggering the reset of positional tracking.")]
        public KeyCode ResetHeadTrackingBtn; //!< The button triggering the reset of positional tracking.

        private Head head;
        private bool isHeadTracked = false;
        private bool enableHeadTracking = false;

        void Update()
        {
            if (enableHeadTracking)
            {
                headTrackingInitialize();
                head = FingoMain.Instance.GetHead();
                transform.localPosition = head.GetPosition() * HeadMovementScale;
                transform.localRotation = head.GetRotation();
            }
        }

        void headTrackingInitialize()
        {
            if (!isHeadTracked)
            {
                if (Input.GetKeyDown(ResetHeadTrackingBtn))
                {
                    FingoMain.Instance.ResetHeadTracking();
                    isHeadTracked = true;
                }
            }
        }
    }
}
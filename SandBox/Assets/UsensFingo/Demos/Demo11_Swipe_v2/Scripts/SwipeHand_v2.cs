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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fingo;

public class SwipeHand_v2 : MonoBehaviour
{
    /// <summary>
    /// The hand type of the tracked hand.
    /// </summary>
    public HandType handType;
    private Hand hand;
	public float width=0.05f;  // half hand width
	public float height=0.03f; // half hand height
    private Vector3 pre_position = Vector3.zero; // hand position in last frame
	private bool first_frame = true;
	public delegate void SwipeEventHandler_v2(Vector3 pos, float dis,float hand_width,float hand_height, SwipeType swipeType);
    public static event SwipeEventHandler_v2 SwipeEvent;

    void OnEnable()
    {
        hand = new Hand();
    }

    void Update()
    {
        hand = FingoMain.Instance.GetHand(handType);

        if (hand.IsDetected())
        {
            transform.position = hand.GetPalmPosition();

			/* discart the first frame after hand is detected */
			if (!first_frame) {
				Vector3 velocity = transform.position - pre_position;
				BroadCastPosition (velocity);
			} else {
				first_frame = false;
			}
            pre_position = transform.position;
        }
        else
        {
			first_frame = true;
            pre_position = Vector3.zero;
        }
    }

	/* broadcast swipe event */
    void BroadCastPosition(Vector3 vel)
    {
        float x_abs = Mathf.Abs(vel.x);
        float y_abs = Mathf.Abs(vel.y);
        if (x_abs > y_abs)
        {
            if (vel.x > 0)
            {
				SwipeEvent(transform.position, x_abs, width, height, SwipeType.Right);
            }
            else
            {
				SwipeEvent(transform.position, x_abs, width, height, SwipeType.Left);
            }
        }
        else
        {
            if (vel.y > 0)
            {
				SwipeEvent(transform.position, y_abs, width, height, SwipeType.Up);
            }
            else
            {
				SwipeEvent(transform.position, y_abs, width, height, SwipeType.Down);
            }
        }

    }
}

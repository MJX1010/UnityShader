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

public enum SwipeResponseType
{
    TypeOne,
    TypeTwo
}

public class SwipeCtrl : MonoBehaviour
{
    public SwipeResponseType swipeResponseType;

    public float horizontalForce = 4.8f;
    public float verticalForce = 6.4f;

    private Rigidbody rigid;

    private bool horizontalRightMovable = true;
    private bool horizontalLeftMovable = true;
    private bool verticalUpMovable = true;
    private bool verticalDownMovable = true;

    void OnEnable()
    {
        SwipeHand.SwipeEvent += SwipeResponseCtrl;

        rigid = this.GetComponent<Rigidbody>();
    }

    void OnDisable()
    {
        SwipeHand.SwipeEvent -= SwipeResponseCtrl;
    }

    void Update()
    {
        if (this.transform.position.y > 2f || this.transform.position.y < -2f)
        {
            horizontalRightMovable = false;
            horizontalLeftMovable = false;
        }
        else 
        {
            if (this.transform.position.x > -5f)
            {
                horizontalLeftMovable = true;
            }
            else
            {
                horizontalLeftMovable = false;
            }

            if (this.transform.position.x < 5f)
            {
                horizontalRightMovable = true;
            }
            else
            {
                horizontalRightMovable = false;
            }
        }
    }

    void SwipeResponseCtrl(HandType handType, SwipeType swipeType)
    {
        if (swipeResponseType == SwipeResponseType.TypeOne)
        {
            if (handType == HandType.Right && swipeType == SwipeType.Right)
            {
                if (horizontalRightMovable)
                {
                    this.rigid.AddForce(new Vector3(horizontalForce, 0f, 0f));
                }
            }
            else if (handType == HandType.Left && swipeType == SwipeType.Left)
            {
                if (horizontalLeftMovable)
                {
                    this.rigid.AddForce(new Vector3(-1f * horizontalForce, 0f, 0f));
                }
            }
            else if (handType == HandType.Right && swipeType == SwipeType.Up)
            {
                if (verticalUpMovable)
                {
                    this.rigid.AddForce(new Vector3(0f, verticalForce, 0f));
                }
            }
            else if (handType == HandType.Left && swipeType == SwipeType.Down)
            {
                if (verticalDownMovable)
                {
                    this.rigid.AddForce(new Vector3(0f, -1f * verticalForce, 0f));
                }
            }
        }
        else if (swipeResponseType == SwipeResponseType.TypeTwo)
        {
            if (swipeType == SwipeType.Right)
            {
                if (horizontalRightMovable)
                {
                    this.rigid.AddForce(new Vector3(horizontalForce, 0f, 0f));
                }
            }
            else if (swipeType == SwipeType.Left)
            {
                if (horizontalLeftMovable)
                {
                    this.rigid.AddForce(new Vector3(-1f * horizontalForce, 0f, 0f));
                }
            }
            else if (swipeType == SwipeType.Up)
            {
                if (verticalUpMovable)
                {
                    this.rigid.AddForce(new Vector3(0f, verticalForce, 0f));
                }
            }
            else if (swipeType == SwipeType.Down)
            {
                if (verticalDownMovable)
                {
                    this.rigid.AddForce(new Vector3(0f, -1f * verticalForce, 0f));
                }
            }
        }
    }
}

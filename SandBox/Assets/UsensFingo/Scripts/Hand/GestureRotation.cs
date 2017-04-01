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
namespace Fingo
{
    internal class GestureRotation
    {
        Vector3[,] gestureJointEulerAngle = new Vector3[30, 20];

        public GestureRotation(HandType handType)
        {
            if (handType == HandType.Right)
            {
                /*---------------------------------------------------------------------------------------------------*\
                 * 基础手势。
                 * Here begins the basic gesture.
                \*---------------------------------------------------------------------------------------------------*/
                int i = GestureNameToInt(GestureName.Palm);

                gestureJointEulerAngle[i, 0] = new Vector3(25, 40, -45);
                gestureJointEulerAngle[i, 1] = new Vector3(-15, 40, 10);
                gestureJointEulerAngle[i, 2] = new Vector3(10, 0, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-5, 0, -7);
                gestureJointEulerAngle[i, 5] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-5, 0, 7);
                gestureJointEulerAngle[i, 13] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-5, 0, 15);
                gestureJointEulerAngle[i, 17] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.Grab);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, 65, -60);
                gestureJointEulerAngle[i, 1] = new Vector3(-30, 42, 5);
                gestureJointEulerAngle[i, 2] = new Vector3(-20, -10, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-30, 0, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-60, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-45, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-25, 0, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-45, -5, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-25, 0, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-45, -10, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-25, 0, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-45, -10, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.ShootEm);

                gestureJointEulerAngle[i, 0] = new Vector3(0, 40, -45);
                gestureJointEulerAngle[i, 1] = new Vector3(-17, 0, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-15, 0, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-70, -5, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-70, -5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-70, -5, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.CallMe);

                gestureJointEulerAngle[i, 0] = new Vector3(10, 40, -45);
                gestureJointEulerAngle[i, 1] = new Vector3(-10, 40, 10);
                gestureJointEulerAngle[i, 2] = new Vector3(0, 0, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-10, 0, 7);
                gestureJointEulerAngle[i, 17] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.Okay);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, 65, -60);
                gestureJointEulerAngle[i, 1] = new Vector3(-30, 42, 5);
                gestureJointEulerAngle[i, 2] = new Vector3(-20, -10, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-33, 0, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-60, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-45, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-35, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-6, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-5, 0, 8);
                gestureJointEulerAngle[i, 13] = new Vector3(-30, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-12, 3, -4);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(0, -8, 20);
                gestureJointEulerAngle[i, 17] = new Vector3(-22, 4, -3.5f);
                gestureJointEulerAngle[i, 18] = new Vector3(-21, 1, -3);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.Fist);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, 65, -50);
                gestureJointEulerAngle[i, 1] = new Vector3(-45, 30, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-45, -20, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.Point);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, 65, -50);
                gestureJointEulerAngle[i, 1] = new Vector3(-45, 30, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-45, -20, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.Peace);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, 65, -50);
                gestureJointEulerAngle[i, 1] = new Vector3(-45, 30, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-45, -20, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-5, 0, -7);
                gestureJointEulerAngle[i, 5] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-55, -5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, -5, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-55, -5, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, -5, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.ThumbsUp);

                gestureJointEulerAngle[i, 0] = new Vector3(10, 40, -45);
                gestureJointEulerAngle[i, 1] = new Vector3(-10, 40, 10);
                gestureJointEulerAngle[i, 2] = new Vector3(0, 0, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.PinchCloseMRP);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, 65, -60);
                gestureJointEulerAngle[i, 1] = new Vector3(-30, 42, 5);
                gestureJointEulerAngle[i, 2] = new Vector3(-20, -10, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-33, 0, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-60, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-45, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-45, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-45, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.PinchOpenMRP);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, 65, -60);
                gestureJointEulerAngle[i, 1] = new Vector3(-30, 42, 5);
                gestureJointEulerAngle[i, 2] = new Vector3(-20, -10, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-33, 0, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-60, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-45, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-35, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-6, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-5, 0, 8);
                gestureJointEulerAngle[i, 13] = new Vector3(-30, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-12, 3, -4);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(0, -8, 20);
                gestureJointEulerAngle[i, 17] = new Vector3(-22, 4, -3.5f);
                gestureJointEulerAngle[i, 18] = new Vector3(-21, 1, -3);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*\
                 * 扩展手势。
                 * Here begins the extended gesture.
                \*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.Pinky);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, 65, -50);
                gestureJointEulerAngle[i, 1] = new Vector3(-45, 30, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-45, -20, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-10, 0, 7);
                gestureJointEulerAngle[i, 17] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.MiddleFinger);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, 65, -50);
                gestureJointEulerAngle[i, 1] = new Vector3(-45, 30, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-45, -20, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.Horns);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, 65, -50);
                gestureJointEulerAngle[i, 1] = new Vector3(-45, 30, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-45, -20, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-10, 0, 7);
                gestureJointEulerAngle[i, 17] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.Rock);

                gestureJointEulerAngle[i, 0] = new Vector3(0, 40, -45);
                gestureJointEulerAngle[i, 1] = new Vector3(-17, 0, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-15, 0, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-70, -5, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-70, -5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-10, 0, 7);
                gestureJointEulerAngle[i, 17] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.ThreeFingersIMR);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, 75, -50);
                gestureJointEulerAngle[i, 1] = new Vector3(-45, 30, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-45, -20, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-5, 0, -7);
                gestureJointEulerAngle[i, 5] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-5, 0, 7);
                gestureJointEulerAngle[i, 13] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-50, -20, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-105, -15, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-30, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.FourFingersIMRP);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, 65, -50);
                gestureJointEulerAngle[i, 1] = new Vector3(-45, 30, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-45, -20, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-5, 0, -7);
                gestureJointEulerAngle[i, 5] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-5, 0, 7);
                gestureJointEulerAngle[i, 13] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-5, 0, 15);
                gestureJointEulerAngle[i, 17] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.ThreeFingersTIM);

                gestureJointEulerAngle[i, 0] = new Vector3(0, 40, -45);
                gestureJointEulerAngle[i, 1] = new Vector3(-17, 0, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-15, 0, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-5, 0, -7);
                gestureJointEulerAngle[i, 5] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-55, -5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, -5, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-55, -5, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, -5, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*\
                 * 订制手势。
                 * Here begins the custom gesture.
                \*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.CustomSnapStartOne);

                gestureJointEulerAngle[i, 0] = new Vector3(-4, 63, -70);
                gestureJointEulerAngle[i, 1] = new Vector3(-30, 42, 5);
                gestureJointEulerAngle[i, 2] = new Vector3(-40, -10, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-33, 0, -4);
                gestureJointEulerAngle[i, 5] = new Vector3(-68, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-35, -5, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-45, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-45, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.CustomSnapStartTwo);

                gestureJointEulerAngle[i, 0] = new Vector3(-4, 77.5f, -75);
                gestureJointEulerAngle[i, 1] = new Vector3(-30, 42, 5);
                gestureJointEulerAngle[i, 2] = new Vector3(-40, -10, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-40, 2, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-68, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-35, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-45, -3, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-35, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-45, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-45, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.CustomSnapEndOne);

                gestureJointEulerAngle[i, 0] = new Vector3(8, 85, -54);
                gestureJointEulerAngle[i, 1] = new Vector3(-40, 10, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-45, 0, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.CustomSnapEndTwo);

                gestureJointEulerAngle[i, 0] = new Vector3(10, 40, -45);
                gestureJointEulerAngle[i, 1] = new Vector3(-10, 40, 10);
                gestureJointEulerAngle[i, 2] = new Vector3(0, 0, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-75, -5, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
            }

            else if (handType == HandType.Left)
            {
                /*---------------------------------------------------------------------------------------------------*\
                 * 基础手势。
                 * Here begins the basic gesture.
                \*---------------------------------------------------------------------------------------------------*/
                int i = GestureNameToInt(GestureName.Palm);

                gestureJointEulerAngle[i, 0] = new Vector3(25, -40, 45);
                gestureJointEulerAngle[i, 1] = new Vector3(-15, -40, -10);
                gestureJointEulerAngle[i, 2] = new Vector3(10, 0, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-5, 0, 7);
                gestureJointEulerAngle[i, 5] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-5, 0, -7);
                gestureJointEulerAngle[i, 13] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-5, 0, -15);
                gestureJointEulerAngle[i, 17] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.Grab);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, -65, 60);
                gestureJointEulerAngle[i, 1] = new Vector3(-30, -42, -5);
                gestureJointEulerAngle[i, 2] = new Vector3(-20, 10, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-30, 0, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-60, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-45, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-25, 0, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-45, 5, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-25, 0, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-45, 10, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-25, 0, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-45, 10, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.ShootEm);

                gestureJointEulerAngle[i, 0] = new Vector3(0, -40, 45);
                gestureJointEulerAngle[i, 1] = new Vector3(-17, 0, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-15, 0, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-70, 5, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-70, 5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-70, 5, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.CallMe);

                gestureJointEulerAngle[i, 0] = new Vector3(10, -40, 45);
                gestureJointEulerAngle[i, 1] = new Vector3(-10, -40, -10);
                gestureJointEulerAngle[i, 2] = new Vector3(0, 0, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-10, 0, -7);
                gestureJointEulerAngle[i, 17] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.Okay);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, -65, 60);
                gestureJointEulerAngle[i, 1] = new Vector3(-30, -42, -5);
                gestureJointEulerAngle[i, 2] = new Vector3(-20, 10, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-33, 0, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-60, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-45, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-35, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-6, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-5, 0, 8);
                gestureJointEulerAngle[i, 13] = new Vector3(-30, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-12, -3, 4);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(0, 8, -20);
                gestureJointEulerAngle[i, 17] = new Vector3(-22, -4, 3.5f);
                gestureJointEulerAngle[i, 18] = new Vector3(-21, -1, 3);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.Fist);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, -65, 50);
                gestureJointEulerAngle[i, 1] = new Vector3(-45, -30, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-45, 20, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.Point);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, -65, 50);
                gestureJointEulerAngle[i, 1] = new Vector3(-45, -30, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-45, 20, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.Peace);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, -65, 50);
                gestureJointEulerAngle[i, 1] = new Vector3(-45, -30, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-45, 20, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-5, 0, 7);
                gestureJointEulerAngle[i, 5] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-55, 5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 5, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-55, 5, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, 5, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.ThumbsUp);

                gestureJointEulerAngle[i, 0] = new Vector3(10, -40, 45);
                gestureJointEulerAngle[i, 1] = new Vector3(-10, -40, -10);
                gestureJointEulerAngle[i, 2] = new Vector3(0, 0, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.PinchCloseMRP);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, -65, 60);
                gestureJointEulerAngle[i, 1] = new Vector3(-30, -42, -5);
                gestureJointEulerAngle[i, 2] = new Vector3(-20, 10, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-33, 0, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-60, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-45, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-45, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-45, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.PinchOpenMRP);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, -65, 60);
                gestureJointEulerAngle[i, 1] = new Vector3(-30, -42, -5);
                gestureJointEulerAngle[i, 2] = new Vector3(-20, 10, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-33, 0, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-60, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-45, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-35, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-6, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-5, 0, 8);
                gestureJointEulerAngle[i, 13] = new Vector3(-30, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-12, -3, 4);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(0, 8, -20);
                gestureJointEulerAngle[i, 17] = new Vector3(-22, -4, 3.5f);
                gestureJointEulerAngle[i, 18] = new Vector3(-21, -1, 3);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*\
                 * 扩展手势。
                 * Here begins the extended gesture.
                \*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.Pinky);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, -65, 50);
                gestureJointEulerAngle[i, 1] = new Vector3(-45, -30, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-45, 20, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-10, 0, -7);
                gestureJointEulerAngle[i, 17] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.MiddleFinger);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, -65, 50);
                gestureJointEulerAngle[i, 1] = new Vector3(-45, -30, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-45, 20, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.Horns);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, -65, 50);
                gestureJointEulerAngle[i, 1] = new Vector3(-45, -30, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-45, 20, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-10, 0, -7);
                gestureJointEulerAngle[i, 17] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.Rock);

                gestureJointEulerAngle[i, 0] = new Vector3(0, -40, 45);
                gestureJointEulerAngle[i, 1] = new Vector3(-17, 0, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-15, 0, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-70, 5, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-70, 5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-10, 0, -7);
                gestureJointEulerAngle[i, 17] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.ThreeFingersIMR);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, -75, 50);
                gestureJointEulerAngle[i, 1] = new Vector3(-45, -30, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-45, 20, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-5, 0, 7);
                gestureJointEulerAngle[i, 5] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-5, 0, -7);
                gestureJointEulerAngle[i, 13] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-50, 20, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-105, 15, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-30, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.FourFingersIMRP);

                gestureJointEulerAngle[i, 0] = new Vector3(-5, -65, 50);
                gestureJointEulerAngle[i, 1] = new Vector3(-45, -30, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-45, 20, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-5, 0, 7);
                gestureJointEulerAngle[i, 5] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-5, 0, -7);
                gestureJointEulerAngle[i, 13] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-5, 0, -15);
                gestureJointEulerAngle[i, 17] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.ThreeFingersTIM);

                gestureJointEulerAngle[i, 0] = new Vector3(0, -40, 45);
                gestureJointEulerAngle[i, 1] = new Vector3(-17, 0, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-15, 0, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-5, 0, 7);
                gestureJointEulerAngle[i, 5] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-55, 5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 5, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-55, 5, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, 5, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-20, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*\
                 * 订制手势。
                 * Here begins the custom gesture.
                \*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.CustomSnapStartOne);

                gestureJointEulerAngle[i, 0] = new Vector3(-4, -63, 70);
                gestureJointEulerAngle[i, 1] = new Vector3(-30, -42, -5);
                gestureJointEulerAngle[i, 2] = new Vector3(-40, 10, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-33, 0, 4);
                gestureJointEulerAngle[i, 5] = new Vector3(-68, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-35, 5, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-45, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-45, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.CustomSnapStartTwo);

                gestureJointEulerAngle[i, 0] = new Vector3(-4, -77.5f, 75);
                gestureJointEulerAngle[i, 1] = new Vector3(-30, -42, -5);
                gestureJointEulerAngle[i, 2] = new Vector3(-40, 10, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-40, -2, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-68, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-35, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-45, 3, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-35, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-45, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-70, 0, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-45, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.CustomSnapEndOne);

                gestureJointEulerAngle[i, 0] = new Vector3(8, -85, 54);
                gestureJointEulerAngle[i, 1] = new Vector3(-40, -10, 0);
                gestureJointEulerAngle[i, 2] = new Vector3(-45, 0, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-10, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-5, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
                i = GestureNameToInt(GestureName.CustomSnapEndTwo);

                gestureJointEulerAngle[i, 0] = new Vector3(10, -40, 45);
                gestureJointEulerAngle[i, 1] = new Vector3(-10, -40, -10);
                gestureJointEulerAngle[i, 2] = new Vector3(0, 0, 0);
                gestureJointEulerAngle[i, 3] = new Vector3();

                gestureJointEulerAngle[i, 4] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 5] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 6] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 7] = new Vector3();

                gestureJointEulerAngle[i, 8] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 9] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 10] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 11] = new Vector3();

                gestureJointEulerAngle[i, 12] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 13] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 14] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 15] = new Vector3();

                gestureJointEulerAngle[i, 16] = new Vector3(-75, 5, 0);
                gestureJointEulerAngle[i, 17] = new Vector3(-115, 0, 0);
                gestureJointEulerAngle[i, 18] = new Vector3(-75, 0, 0);
                gestureJointEulerAngle[i, 19] = new Vector3();
                /*---------------------------------------------------------------------------------------------------*/
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
        public Vector3 GetGestureJointEulerAngle(GestureName gestureType, int jointIndex)
        {
            return gestureJointEulerAngle[GestureNameToInt(gestureType), jointIndex];
        }

        private int GestureNameToInt(GestureName gestureName)
        {
            int i = (int)gestureName;

            if (gestureName == GestureName.Pinky)
            {
                return 12;
            }
            else if (gestureName == GestureName.MiddleFinger)
            {
                return 13;
            }
            else if (gestureName == GestureName.Horns)
            {
                return 14;
            }
            else if (gestureName == GestureName.Rock)
            {
                return 15;
            }
            else if (gestureName == GestureName.ThreeFingersIMR)
            {
                return 16;
            }
            else if (gestureName == GestureName.FourFingersIMRP)
            {
                return 17;
            }
            else if (gestureName == GestureName.ThreeFingersTIM)
            {
                return 18;
            }
            else if (gestureName == GestureName.CustomNone)
            {
                return 19;
            }
            else if (gestureName == GestureName.CustomSnapEndOne)
            {
                return 20;
            }
            else if (gestureName == GestureName.CustomSnapEndTwo)
            {
                return 21;
            }
            else if (gestureName == GestureName.CustomSnapStartOne)
            {
                return 22;
            }
            else if (gestureName == GestureName.CustomSnapStartTwo)
            {
                return 23;
            }

            return i;
        }
        /*---------------------------------------------------------------------------------------------------*/
    }
}
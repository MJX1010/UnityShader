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

namespace Fingo
{
    /// <summary>
    /// Mesh hand is a class to drive a hand model by data from Fingo Client. 
    /// </summary>
    public class MeshHand : MonoBehaviour
    {
        private Hand hand;

        [Tooltip("The hand type of the hand.")]
        public HandType handType; //!< The hand type of the hand.

        [Tooltip("The move scale of joint position from original hand data.")]
        [SerializeField]
        private float moveScale = 1.0f; //!< The scale of joint position from original hand data.
        [Tooltip("The render scale of the collision hand.")]
        [SerializeField]
        private float renderScale = 1.0f; //!< The render scale of the collision hand.

        [HideInInspector]
        public bool isDetected = false; //!< Whether or not this hand is detected.

        [Tooltip("Enable this switch to set hand in stable mode. (Coming soon...)")]
        public bool enableStabilizer = false;           //!< Enable this switch to set hand in stable mode.

        private GestureRotation gestureJointRotation;
        private GestureName currentGesture = GestureName.None;
        private GestureName previousGesture = GestureName.None;
        private bool startJointTrans;
        private Quaternion[] jointRotation = new Quaternion[20];
        private Quaternion tipRotation = new Quaternion();

        private Vector3[] currentJointRotationEulerAngle = new Vector3[20];
        private Vector3[] previousJointRotationEulerAngle = new Vector3[20];
        private Vector3 tempVector = new Vector3();

        private float jointRotationTransCounter;
        public float jointRotationTransInterval = 5f;

        [Header("Hand Joint Mapping")]
        public Transform root;                         //!< The Transform of root of this hand.
        public Transform wrist;                        //!< The Transform of wrist of this hand.
        public Transform thumbProximal;                //!< The Transform of thumb proximal of this hand.        
        public Transform thumbIntermediate;            //!< The Transform of thumb intermediate of this hand.            
        public Transform thumbDistal;                  //!< The Transform of thumb distal of this hand.      
        public Transform thumbTip;                     //!< The Transform of thumb tip of this hand.   
        public Transform indexProximal;                //!< The Transform of index proximal of this hand.        
        public Transform indexIntermediate;            //!< The Transform of index intermediate of this hand.            
        public Transform indexDistal;                  //!< The Transform of index distal of this hand.      
        public Transform indexTip;                     //!< The Transform of index tip of this hand.   
        public Transform middleProximal;               //!< The Transform of middle proximal of this hand.         
        public Transform middleIntermediate;           //!< The Transform of middle intermediate of this hand.             
        public Transform middleDistal;                 //!< The Transform of middle distal of this hand.       
        public Transform middleTip;                    //!< The Transform of middle tip of this hand.    
        public Transform ringProximal;                 //!< The Transform of ring proximal of this hand.       
        public Transform ringIntermediate;             //!< The Transform of ring intermediate of this hand.           
        public Transform ringDistal;                   //!< The Transform of ring distal of this hand.     
        public Transform ringTip;                      //!< The Transform of ring tip of this hand.  
        public Transform pinkyProximal;                //!< The Transform of pinky proximal of this hand.        
        public Transform pinkyIntermediate;            //!< The Transform of pinky intermediate of this hand.            
        public Transform pinkyDistal;                  //!< The Transform of pinky distal of this hand.      
        public Transform pinkyTip;                     //!< The Transform of pinky tip of this hand.  
        /*---------------------------------------------------------------------------------------------------*/
        void OnEnable()
        {
            gestureJointRotation = new GestureRotation(this.handType);
        }

        void Update()
        {
            hand = FingoMain.Instance.GetHand(handType);

            isDetected = hand.IsDetected();

            currentGesture = hand.GetGestureName();

            if (isDetected)
            {
                transform.localScale = Vector3.one * renderScale;
            }
            else
            {
                transform.localScale = Vector3.zero;
            }

            UpdateMeshHand();

            previousGesture = currentGesture;
        }
        /*---------------------------------------------------------------------------------------------------*/
        /// <summary>
        /// Update mesh hand data.
        /// </summary>
        void UpdateMeshHand()
        {
            if (isDetected)
            {
                SetMeshHandPosition();
                SetMeshHandRotation();
            }
        }

        /// <summary>
        /// Set Mesh hand position.
        /// </summary>
        void SetMeshHandPosition()
        {
            wrist.localPosition = hand.GetWristPosition() * moveScale / renderScale;
        }
        /*---------------------------------------------------------------------------------------------------*/
        /// <summary>
        /// Set Mesh hand joint rotation.
        /// </summary>
        void SetMeshHandRotation()
        {
            if (hand.IsDetected())
            {
                root.localRotation = hand.GetJointLocalRotation(JointIndex.RootJoint);
                wrist.localRotation = hand.GetJointLocalRotation(JointIndex.WristJoint);

                if (enableStabilizer && currentGesture != GestureName.None)
                {
                    if (currentGesture != previousGesture && previousGesture != GestureName.None)
                    {
                        startJointTrans = true;

                        TrackJointRotationEulerAngle();

                        jointRotationTransCounter = 0f;
                    }
                    else if (previousGesture == GestureName.None)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            jointRotation[i] = Quaternion.Euler(gestureJointRotation.GetGestureJointEulerAngle(currentGesture, i));
                        }

                        SetJointRotation(enableStabilizer);
                    }

                    if (startJointTrans && jointRotationTransInterval == 0)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            jointRotation[i] = Quaternion.Euler(gestureJointRotation.GetGestureJointEulerAngle(currentGesture, i));
                        }

                        SetJointRotation(enableStabilizer);
                    }

                    if (startJointTrans && jointRotationTransCounter < jointRotationTransInterval)
                    {
                        jointRotationTransCounter++;

                        for (int i = 0; i < 20; i++)
                        {
                            currentJointRotationEulerAngle[i] = gestureJointRotation.GetGestureJointEulerAngle(currentGesture, i);

                            tempVector = previousJointRotationEulerAngle[i] + (currentJointRotationEulerAngle[i] - previousJointRotationEulerAngle[i]) *
                                         jointRotationTransCounter / jointRotationTransInterval;

                            jointRotation[i] = Quaternion.Euler(tempVector);
                        }

                        SetJointRotation(enableStabilizer);
                    }
                    else if (jointRotationTransCounter >= jointRotationTransInterval)
                    {
                        startJointTrans = false;

                        jointRotationTransCounter = 0f;
                    }
                }
                else if (!enableStabilizer)
                {
                    SetJointRotation(enableStabilizer);
                }
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
        void SetJointRotation(bool enableStabilizer)
        {
            if (enableStabilizer)
            {
                thumbProximal.localRotation = jointRotation[0];
                thumbIntermediate.localRotation = jointRotation[1];
                thumbDistal.localRotation = jointRotation[2];
                thumbTip.localRotation = jointRotation[3];

                indexProximal.localRotation = jointRotation[4];
                indexIntermediate.localRotation = jointRotation[5];
                indexDistal.localRotation = jointRotation[6];
                indexTip.localRotation = jointRotation[7];

                middleProximal.localRotation = jointRotation[8];
                middleIntermediate.localRotation = jointRotation[9];
                middleDistal.localRotation = jointRotation[10];
                middleTip.localRotation = jointRotation[11];

                ringProximal.localRotation = jointRotation[12];
                ringIntermediate.localRotation = jointRotation[13];
                ringDistal.localRotation = jointRotation[14];
                ringTip.localRotation = jointRotation[15];

                pinkyProximal.localRotation = jointRotation[16];
                pinkyIntermediate.localRotation = jointRotation[17];
                pinkyDistal.localRotation = jointRotation[18];
                pinkyTip.localRotation = jointRotation[19];
            }
            else
            {
                thumbProximal.localRotation = hand.GetJointLocalRotation(JointIndex.ThumbProximal);
                thumbIntermediate.localRotation = hand.GetJointLocalRotation(JointIndex.ThumbIntermediate);
                thumbDistal.localRotation = hand.GetJointLocalRotation(JointIndex.ThumbDistal);
                thumbTip.localRotation = hand.GetTipLocalRotation(TipIndex.ThumbTip);

                indexProximal.localRotation = hand.GetJointLocalRotation(JointIndex.IndexProximal);
                indexIntermediate.localRotation = hand.GetJointLocalRotation(JointIndex.IndexIntermediate);
                indexDistal.localRotation = hand.GetJointLocalRotation(JointIndex.IndexDistal);
                indexTip.localRotation = hand.GetTipLocalRotation(TipIndex.IndexTip);

                middleProximal.localRotation = hand.GetJointLocalRotation(JointIndex.MiddleProximal);
                middleIntermediate.localRotation = hand.GetJointLocalRotation(JointIndex.MiddleIntermediate);
                middleDistal.localRotation = hand.GetJointLocalRotation(JointIndex.MiddleDistal);
                middleTip.localRotation = hand.GetTipLocalRotation(TipIndex.MiddleTip);

                ringProximal.localRotation = hand.GetJointLocalRotation(JointIndex.RingProximal);
                ringIntermediate.localRotation = hand.GetJointLocalRotation(JointIndex.RingIntermediate);
                ringDistal.localRotation = hand.GetJointLocalRotation(JointIndex.RingDistal);
                ringTip.localRotation = hand.GetTipLocalRotation(TipIndex.RingTip);

                pinkyProximal.localRotation = hand.GetJointLocalRotation(JointIndex.PinkyProximal);
                pinkyIntermediate.localRotation = hand.GetJointLocalRotation(JointIndex.PinkyIntermediate);
                pinkyDistal.localRotation = hand.GetJointLocalRotation(JointIndex.PinkyDistal);
                pinkyTip.localRotation = hand.GetTipLocalRotation(TipIndex.PinkyTip);
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
        void TrackJointRotationEulerAngle()
        {
            for (int i = 0; i < 20; i++)
            {
                previousJointRotationEulerAngle[i] = gestureJointRotation.GetGestureJointEulerAngle(previousGesture, i);
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
    }
}
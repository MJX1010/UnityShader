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
    /// Stick hand is a class to drive a hand model by data from Fingo Client. 
    /// </summary>
    public class StickHand : MonoBehaviour
    {
        private Hand hand;

        [Tooltip("The hand type of the hand.")]
        public HandType HandType; //!< The hand type of the skeleton hand.

        [Tooltip("The move scale of joint position from original hand data.")]
        [SerializeField]
        private float moveScale = 1.0f; //!< The scale of joint position from original hand data.
        [Tooltip("The render scale of the collision hand.")]
        [SerializeField]
        private float renderScale = 1.0f; //!< The render scale of the collision hand.

        [HideInInspector]
        public bool isDetected = false; //!< Whether or not this hand is detected.

        [Tooltip("Joint Prefab of stick hand.")]
        [SerializeField]
        private Transform jointPrefab; //!< Joint Prefab of stick hand.
        [Tooltip("Finger Tip Prefab of stick hand.")]
        [SerializeField]
        private Transform fingerTipPrefab; //!< Finger Tip Prefab of stick hand.
        [Tooltip("Bone Prefab of stick hand.")]
        [SerializeField]
        private Transform bonePrefab; //!< Bone Prefab of stick hand.
        [Tooltip("The stick hand is visible or not.")]
        public bool isVisible; //!< The stick hand is visible or not.

        private Transform[] fingerTips = new Transform[5]; //!< the Transform of five fingers.
        private Transform[] joints = new Transform[17]; //!< the Transform of seventeen joints of a hand.
        private Transform[] bones = new Transform[20]; //!< the Transform of twenty bones of a hand.

        private Vector3 bonePrefabScale = new Vector3(1, 1, 1); //!< the original scale of the bone prefab.
        private Vector3 jointPrefabScale = new Vector3(2, 2, 2); //!< the original scale of the joint prefab.
        private Vector3 tipPrefabScale = new Vector3(2, 2, 2); //!< the original scale of the finger tip prefab.

        void Awake()
        {
            iniStickHand();
        }

        void Update()
        {
            hand = FingoMain.Instance.GetHand(HandType);
            isDetected = hand.IsDetected();
            if (isDetected)
            {
				transform.localScale = Vector3.one * moveScale;
                setJoints();
                setBones();
            }
            else
            {
                transform.localScale = Vector3.zero;
            }
        }

        /// <summary>
        /// Initialize joint, finger tip and bone gameobject.
        /// </summary>
        void iniStickHand()
        {
            for (int i = 0; i < 17; ++i)
            {
                if (joints[i] == null)
                {
                    joints[i] = GameObject.Instantiate(jointPrefab).transform;
                }
                joints[i].GetComponent<Renderer>().enabled = isVisible;
                joints[i].name = "Joint" + i.ToString();
                joints[i].parent = this.transform;
            }
            joints[0].GetComponent<Renderer>().enabled = false;   //Hide Root Node
            for (int i = 0; i < 5; ++i)
            {
                if (fingerTips[i] == null)
                {
                    fingerTips[i] = GameObject.Instantiate(fingerTipPrefab).transform;
                }
                fingerTips[i].GetComponent<Renderer>().enabled = isVisible;
                fingerTips[i].name = "Tip" + i.ToString();
                fingerTips[i].parent = this.transform;
            }
            for (int i = 0; i < 20; ++i)
            {
                if (bones[i] == null)
                {
                    bones[i] = GameObject.Instantiate(bonePrefab).transform;
                }
                bones[i].GetComponent<Renderer>().enabled = isVisible;
                bones[i].name = "Bone" + i.ToString();
                bones[i].parent = this.transform;
            }
        }

        /// <summary>
        /// Set Joints and Fingertips position and rotation from Fingo Device.
        /// </summary>
        void setJoints()
        {
            for (int i = 0; i < 17; ++i)
            {
                joints[i].localScale = jointPrefabScale * 0.01f / moveScale * renderScale;
                joints[i].localPosition = (hand.GetJointPosition((JointIndex)i) - hand.GetJointPosition(JointIndex.WristJoint))
                    / moveScale * renderScale + hand.GetJointPosition(JointIndex.WristJoint);
                joints[i].localRotation = hand.GetJointLocalRotation((JointIndex)i);
                joints[i].GetComponent<Renderer>().enabled = isVisible;
            }
			joints[0].GetComponent<Renderer>().enabled = false;   //Hide Root Node
            for (int i = 0; i < 5; ++i)
            {
                fingerTips[i].localScale = tipPrefabScale * 0.01f / moveScale * renderScale;
                fingerTips[i].localPosition = (hand.GetTipPosition((TipIndex)i) - hand.GetJointPosition(JointIndex.WristJoint))
                    / moveScale * renderScale + hand.GetJointPosition(JointIndex.WristJoint);
                fingerTips[i].GetComponent<Renderer>().enabled = isVisible;
            }
        }

        /// <summary>
        /// Set bones of a stick hand.
        /// </summary>
        void setBones()
        {
            for (int i = 0; i < 5; ++i)
            {
                Finger finger = hand.GetFinger((FingerIndex)i);
                for (int j = 0; j < 4; ++j)
                {
                    Bone bone = finger.GetBone((BoneIndex)j);
                    Vector3 startPoint = transform.TransformPoint((bone.GetStartJointPosition() - hand.GetWristPosition()) / moveScale * renderScale + hand.GetWristPosition());
                    Vector3 endPoint = transform.TransformPoint((bone.GetEndJointPosition() - hand.GetWristPosition()) / moveScale * renderScale + hand.GetWristPosition());
                    float dis = bone.GetLength() / moveScale * renderScale;
                    bones[4 * i + j].localScale = new Vector3(0.01f * bonePrefabScale.x / moveScale * renderScale,
                    dis * .5f * bonePrefabScale.y,
                    0.01f * bonePrefabScale.z / moveScale * renderScale);
                    bones[4 * i + j].position = (startPoint + endPoint) * .5f;
                    bones[4 * i + j].localRotation = Quaternion.LookRotation(-bone.GetNormalDirection(), bone.GetUpDirection());
                    bones[4 * i + j].GetComponent<Renderer>().enabled = isVisible;
                }
            }
        }
    }
}
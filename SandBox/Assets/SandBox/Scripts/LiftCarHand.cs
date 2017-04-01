using Fingo;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RigidHand))]
public class LiftCarHand : MonoBehaviour
{
    public MeshHand meshHand;

    private CarObjectPart carObject;

    private RigidHand rigidHand;

    private void Start()
    {
        carObject = GameObject.Find("Car_dynamic").GetComponent<CarObjectPart>();
    }

    private void OnEnable()
    {
        FingoGestureEvent.OnFingoGraspStart += OnFingoGraspStart;
        FingoGestureEvent.OnFingoRelease += OnFingoGraspRelease;
        FingoGestureEvent.OnFingoGraspingPalmInfo += GetRigidHandTransform;
    }

    private void OnDisable()
    {
        FingoGestureEvent.OnFingoGraspStart -= OnFingoGraspStart;
        FingoGestureEvent.OnFingoRelease -= OnFingoGraspRelease;
        FingoGestureEvent.OnFingoGraspingPalmInfo -= GetRigidHandTransform;
    }

    private void Update()
    {
        
    }

    private void OnFingoGraspStart(HandType handType)
    {
        if (carObject)
        {
            if (rigidHand == null)
                return;
            carObject.OnGraspStart();
        }
    }

    private void OnFingoGraspRelease(HandType handType)
    {
        if (carObject) {
            if (rigidHand == null)
                return;
            carObject.OnGraspEnd();
        }
    }


    private void GetRigidHandTransform(HandType handType,RigidHand rigidHand)
    {
        this.rigidHand = rigidHand;
    }
}

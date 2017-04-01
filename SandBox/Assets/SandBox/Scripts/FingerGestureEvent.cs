using UnityEngine;
using System.Collections;   

public class FingoGestureEvent{

    public delegate void Action<T1>(T1 arg1);
    public delegate void Action<T1, T2>(T1 arg1, T2 arg2);
    public delegate void Action<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3);

    public static Action<Fingo.HandType> OnFingoGraspStart;
    public static Action<Fingo.HandType> OnFingoRelease;
    public static Action<Fingo.HandType, RigidHand> OnFingoGraspingPalmInfo;
    public static Action<Fingo.HandType, Vector3[]> OnFingoTipsPos;
}

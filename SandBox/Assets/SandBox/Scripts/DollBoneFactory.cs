using UnityEngine;
using System.Collections;

public class DollBoneFactory : MonoBehaviour {

    public Transform boneRoot;

    private Transform graspBone;
    private Transform boneRootParent;
    private Transform graspBoneParent;

    // Use this for initialization
    void Start () {
        boneRootParent = boneRoot.parent;

        AddComponent(boneRoot);
	}

    void AddComponent(Transform obj)
    {
        if (obj.GetComponent<Collider>() != null)
        {
            DollObject temp = obj.gameObject.AddComponent<DollObject>();
            temp.SetBoneRoot(boneRoot);
        }
        for (int i = 0; i < obj.childCount; i++)
        {
            AddComponent(obj.GetChild(i));
        }
    }
}

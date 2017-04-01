using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DollObject : RigidObject {

    private Transform boneRoot;
    private Transform boneRootParent;
    private Transform graspBoneParent;
    private List<Rigidbody> fathersList;
    private RigidHand myHand;

    void Start()
    {
        fathersList = new List<Rigidbody>();
    }

    void Update()
    {

    }

    public void SetBoneRoot(Transform boneRoot)
    {
        this.boneRoot = boneRoot;
        boneRootParent = boneRoot.parent;
    }

    public override void GraspStart(RigidHand myHand)
    {
        SetMass();

        this.myHand = myHand;
        isGrasped = true;
        graspBoneParent = this.transform.parent;

        this.GetComponent<Rigidbody>().isKinematic = true;
        this.transform.SetParent(myHand.transform);

        if (this.transform != boneRoot)
        {
            boneRoot.SetParent(this.transform);
        }

        //this.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        Debug.Log("doll grasped");
    }

    public override void GraspEnd()
    {
        ReturnMass();
        isGrasped = false;
        boneRoot.SetParent(boneRootParent);
        if (this.transform != boneRoot)
        {
            this.transform.SetParent(graspBoneParent);
        }

        this.GetComponent<Rigidbody>().isKinematic = false;
        Debug.Log("doll release");
    }

    void SetMass()
    {
        fathersList = new List<Rigidbody>();

        if (this.transform != boneRoot)
            FindFather(this.transform.parent, fathersList);
        this.transform.GetComponent<Rigidbody>().mass = 10 + 10 * (fathersList.Count + 1);

        for (int i = 0; i < fathersList.Count; i++)
        {
            fathersList[i].mass = 10 + 10*(fathersList.Count - i);
            Debug.Log(i + " " + fathersList[i].name + "Mass " + fathersList[i].mass); 
        }
    }

    void ReturnMass()
    {
        this.transform.GetComponent<Rigidbody>().mass = 1;

        for (int i = 0; i < fathersList.Count; i++)
        {
            fathersList[i].mass = 1;
        }
    }

    void FindFather(Transform obj, List<Rigidbody> list)
    {
        if(obj != null)
        {
            if (obj.GetComponent<Rigidbody>() != null)
            {
                list.Add(obj.GetComponent<Rigidbody>());
            }
        }

        if (obj == boneRoot)
            return;
        FindFather(obj.parent.transform, list);
    }
    /*
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer.ToString() != "Hand" && (other.transform.name != "Palm_Left" || other.transform.name != "Palm_Right") )
        {
            if(isGrasped)
            {
                GraspEnd();
            }
        }
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColaLiquid : MonoBehaviour
{
    public event System.Action<Vector3> FallOnTheFloor;

    public Transform clipPlane;
    public Transform liquidMin;
    public Transform liquidMax;
    public float strength;

    private MeshRenderer liquidRenderer;
    private float currPlaneY;
    private Quaternion clipPlaneOriginalRot;
    private void OnEnable()
    {
        if (GetComponent<MeshRenderer>()) {
            liquidRenderer = GetComponent<MeshRenderer>();
        }
        clipPlane.position = new Vector3(clipPlane.position.x,liquidMin.position.y,clipPlane.position.z);
        clipPlaneOriginalRot = clipPlane.rotation;
        currPlaneY = clipPlane.position.y;
    }

    private void Update()
    {
        //For test
        //if (Input.GetMouseButton(0))
        //{
        //    LiquidUp();
        //}else if (Input.GetMouseButton(1))
        //{
        //    LiquidDown();
        //}else if (Input.GetMouseButtonDown(2))
        //{
        //    ResetLiquidFull();
        //}

        if (clipPlane)
        {
            clipPlane.rotation = clipPlaneOriginalRot;
            var hasPlanePoint = liquidRenderer.material.HasProperty("_PlanePoint");
            if (hasPlanePoint)
            {
                liquidRenderer.material.SetVector("_PlanePoint", new Vector4(0, clipPlane.position.y, 0, 0));
            }
        }
    }

    public void LiquidUp()
    {
        if (clipPlane)
        {
            currPlaneY = clipPlane.position.y;
            if (currPlaneY > liquidMax.position.y)
                return;
            currPlaneY += strength;
            clipPlane.position = new Vector3(clipPlane.position.x,currPlaneY,clipPlane.position.z);
        }
    }

    public void LiquidDown()
    {
        if (clipPlane)
        {
            currPlaneY = clipPlane.position.y;
            if (currPlaneY < liquidMin.position.y)
                return;
             currPlaneY -= strength;
             clipPlane.position = new Vector3(clipPlane.position.x, currPlaneY, clipPlane.position.z);
        }
    }


    public void ResetLiquidFull()
    {
        clipPlane.position = new Vector3(clipPlane.position.x, liquidMax.position.y, clipPlane.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("floor"))
        {
            this.gameObject.SetActive(false);
            if (FallOnTheFloor != null)
                FallOnTheFloor(this.transform.parent.position);
        }
    }

}

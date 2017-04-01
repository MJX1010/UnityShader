using UnityEngine;
using System.Collections;

public class ChangeObjectColorHand : MonoBehaviour {

    private Transform interactionObj;
    private Renderer[] interactionObjMaterial;
    private Color[] originColor;
    private RigidHand rigidHand;

	// Use this for initialization
	void Start () {
        interactionObj = null;
        rigidHand = this.transform.GetComponent<RigidHand>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(interactionObj != null && rigidHand.GetGraspObj() != null)
        {
            if(rigidHand.GetGraspObj() == interactionObj.transform)
                ResetObjColor();
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if(interactionObj == null && other.GetComponent<RigidObject>())
        {
            interactionObj = other.transform;
            interactionObjMaterial = other.GetComponentsInChildren<Renderer>();

            originColor = new Color[interactionObjMaterial.Length];
            for(int i = 0; i < interactionObjMaterial.Length; i++)
            {
                var hasEmissinColor = interactionObjMaterial[i].material.HasProperty("_EmissionColor");
                if (hasEmissinColor)
                {
                    originColor[i] = interactionObjMaterial[i].material.GetColor("_EmissionColor");
                    interactionObjMaterial[i].material.SetColor("_EmissionColor", Color.yellow);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.transform == interactionObj)
        {
            ResetObjColor();
        }
    }

    void ResetObjColor()
    {
        interactionObj = null;
        for (int i = 0; i < interactionObjMaterial.Length; i++)
        {
            var hasEmissinColor = interactionObjMaterial[i].material.HasProperty("_EmissionColor");
            if (hasEmissinColor)
            {
                interactionObjMaterial[i].material.SetColor("_EmissionColor", originColor[i]);
            }
        }
    }
}

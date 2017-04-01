using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// need to add Collider with trigger true to excute plug in
/// need to add Collider shaped as its mesh and with trigger false to collide with other physic objs.
/// </summary>
public class ElectricSource : MonoBehaviour {

    public Transform pos;

    public Transform outLineObj;
    public Color highLightColor = Color.blue;

    private Renderer[] outLineObjMaterial;
    private Color[] originColor;

    /*public Color flashingStartColor = Color.yellow;
    public Color flashingEndColor = Color.blue;
    public float flashingFrequency = 2f;

    private HighlightableObject highLightObj; 
    */
    // Use this for initialization
    void Start ()
    {
        outLineObjMaterial = outLineObj.GetComponentsInChildren<Renderer>();

        originColor = new Color[outLineObjMaterial.Length];
        for (int i = 0; i < outLineObjMaterial.Length; i++)
        {
            originColor[i] = outLineObjMaterial[i].material.GetColor("_EmissionColor");
        }
        //highLightObj = outLineObj.gameObject.AddComponent<HighlightableObject>();
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void SetUp(ComputerPowerPlugCtrl pcSwitch, TweenCallback callback)
    {
        pcSwitch.GetComponent<Collider>().isTrigger = true;
        pcSwitch.transform.SetParent(this.transform);
        pcSwitch.DOKill();
        pcSwitch.transform.DOMove(pos.position, 0.5f).Play().OnComplete(callback);
        pcSwitch.transform.DORotate(pos.eulerAngles, 0.5f).Play();
        DisHighLight();
    }

    public void HighLight()
    {
        for (int i = 0; i < outLineObjMaterial.Length; i++)
        {
            outLineObjMaterial[i].material.SetColor("_EmissionColor", highLightColor);
        }
        //highLightObj.FlashingOn(flashingStartColor, flashingEndColor, flashingFrequency);
    }

    public void DisHighLight()
    {
        for (int i = 0; i < outLineObjMaterial.Length; i++)
        {
            outLineObjMaterial[i].material.SetColor("_EmissionColor", originColor[i]);
        }
        //highLightObj.FlashingOff();
    }
}

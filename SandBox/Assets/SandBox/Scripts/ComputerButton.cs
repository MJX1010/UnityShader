using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class ComputerButton : MonoBehaviour {

    public Color hightLightColor = Color.yellow;
    public UnityEvent onClick;

    private bool isOpen;
    private bool isButtonDown;
    private bool isEventExcute;
    private List<Transform> finger;

    private Material material;
    private Color originColor;
    private Vector3 originTouchPos;

    // Use this for initialization
    void Start()
    {
        isOpen = false;
        isButtonDown = false;
        isEventExcute = false;
        finger = new List<Transform>();
        material = this.transform.GetComponent<Renderer>().material;
        originColor = material.GetColor("_EmissionColor");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            finger.Add(other.transform);
            if (!isButtonDown)
            {
                isButtonDown = true;
                originTouchPos = this.transform.InverseTransformPoint(finger[0].position);
                material.SetColor("_EmissionColor", hightLightColor);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(isButtonDown && !isEventExcute && other.transform == finger[0])
        {
            Vector3 pos = this.transform.InverseTransformPoint(finger[0].position);
            if(pos.y < originTouchPos.y)
            {
                onClick.Invoke();
                isEventExcute = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            finger.Remove(other.transform);
        }

        if (finger.Count == 0)
        {
            isEventExcute = false;
            material.SetColor("_EmissionColor", originColor);
            isButtonDown = false;
        }
    }
}

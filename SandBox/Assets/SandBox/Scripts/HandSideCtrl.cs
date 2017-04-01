using UnityEngine;
using System.Collections;

public class HandSideCtrl : MonoBehaviour {

    private Material mMaterial;
    private Color mColor;

    // Use this for initialization
    void Start () {
        mMaterial = this.transform.GetComponent<Renderer>().material;
        mColor = mMaterial.GetColor("_TintColor");
        mMaterial.SetColor("_TintColor", new Color(mColor.r, mColor.g, mColor.b, 0.0f));
    }

    // Update is called once per frame
    void Update () {
	}

    void OnTriggerExit(Collider other)
    {
        if(other.transform.name == "Palm_Left" || other.transform.name == "Palm_Right")
        {
            if(other.GetComponent<RigidHand>().meshHand.isDetected)
                AwakeHandSide();
        }
    }

    void AwakeHandSide()
    {
        StopAllCoroutines();
        StartCoroutine(AwakeBoxSide());
    }

    IEnumerator AwakeBoxSide()
    {
        float alpha = 0.0f;
        float speed = 0.03f;

        while(alpha < 0.5f)
        {
            alpha += speed;
            if (alpha > 0.5f)
            {
                alpha = 0.5f;
            }
            mMaterial.SetColor("_TintColor", new Color(mColor.r, mColor.g, mColor.b, alpha));
            yield return 0;
        }
            
        while(alpha > 0.0f)
        {
            alpha -= speed;
            if(alpha < 0.0f)
            {
                alpha = 0.0f;
            }
            mMaterial.SetColor("_TintColor", new Color(mColor.r, mColor.g, mColor.b, alpha));
            yield return 1;
        }
    }
}

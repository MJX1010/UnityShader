/*************************************************************************\
*                           USENS CONFIDENTIAL                            *
* _______________________________________________________________________ *
*                                                                         *
* [2014] - [2017] USENS Incorporated                                      *
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

public class ButtonCtrl : MonoBehaviour
{
    private Color originColor;
    private Renderer targetRenderer;

    void OnEnable()
    {
        targetRenderer = this.GetComponent<Renderer>();

        if (targetRenderer != null)
        {
            originColor = targetRenderer.material.color;
        }
    }

    public void ColorRed()
    {
        if (targetRenderer != null)
        {
            targetRenderer.material.color = Color.red;

            return;
        }

        Debug.Log("ButtonCtrl: ColorRed targetRenderer is null.");
    }

    public void ColorOrigin()
    {
        if (targetRenderer != null)
        {
            targetRenderer.material.color = originColor;

            return;
        }

        Debug.Log("ButtonCtrl: ColorOrigin targetRenderer is null.");
    }
}

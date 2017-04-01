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

[RequireComponent(typeof(Rigidbody))]
public class ThrowCtrl : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    public float factor = 3f;
    public bool enableFlyBack = false;
    public float flyBackTime = 2.0f;

    void OnEnable()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    public void Thrown(Vector3 speed)
    {
        if (rb != null)
        {
            rb.velocity = speed * factor;
        }
        if (enableFlyBack)
        {
            StartCoroutine(AutoFlyBack());
        }
    }

    IEnumerator AutoFlyBack()
    {
        yield return new WaitForSeconds(flyBackTime);
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        rb.velocity = Vector3.zero;
    }
}

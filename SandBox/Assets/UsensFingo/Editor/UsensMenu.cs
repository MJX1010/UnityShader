/*************************************************************************\
*                           USENS CONFIDENTIAL                            *
* _______________________________________________________________________ *
*                                                                         *
* [2015] - [2017] USENS Incorporated                                      *
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
using UnityEditor;

public class UsensMenu : MonoBehaviour {

    [MenuItem("uSens/Documentation/Developers Site", false, 100)]
    private static void OpenDocumentation() 
    {
        Application.OpenURL("https://developers.usens.com");
    }

    [MenuItem("uSens/Create Hand/Stick Hand Left", false, 100)]
    private static void CreateLeftStickHand() 
    {
        loadHand("StickHand_L");
    }

    [MenuItem("uSens/Create Hand/Stick Hand Right", false, 100)]
    private static void CreateRightStickHand() 
    {
        loadHand("StickHand_R");
    }

    [MenuItem("uSens/Create Hand/Mesh Hand Left", false, 100)]
    private static void CreateLeftMeshHand() 
    {
        loadHand("MeshHand_L");
    }

    [MenuItem("uSens/Create Hand/Mesh Hand Right", false, 100)]
    private static void CreateRightMeshHand() 
    {
        loadHand("MeshHand_R");
    }

    [MenuItem("uSens/Create Hand/Collision Hand Left", false, 100)]
    private static void CreateLeftCollisionHand() 
    {
        loadHand("CollisionHand_L");
    }

    [MenuItem("uSens/Create Hand/Collision Hand Right", false, 100)]
    private static void CreateRightCollisionHand() 
    {
        loadHand("CollisionHand_R");
    }

    [MenuItem("uSens/Create Hand/Transparent Hand Left", false, 100)]
    private static void CreateLeftTransparentHand()
    {
        loadHand("TransparentHand_L");
    }

    [MenuItem("uSens/Create Hand/Transparent Hand Right", false, 100)]
    private static void CreateRightTransparentHand()
    {
        loadHand("TransparentHand_R");
    }

    private static void loadHand(string name) 
    {
        GameObject hand = GameObject.Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Assets/UsensFingo/Prefab/Hands/" + name + ".prefab", typeof(GameObject)));
        hand.name = name;
    }
}

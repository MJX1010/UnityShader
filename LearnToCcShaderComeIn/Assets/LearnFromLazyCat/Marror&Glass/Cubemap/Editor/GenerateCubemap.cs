using UnityEngine;
using UnityEditor;
using System.Collections;

/*
 * 创建Cubemap
 */
public class GenerateCubemap : ScriptableWizard
{
    public Transform renderFromPosition;
    public Cubemap cubemap;

    void OnWizardUpdate()
    {
        string helpText = "Select transform to render from and cubemap to render into";
        isValid = (renderFromPosition != null) && (cubemap != null);
    }

    void OnWizardCreate()
    {
        GameObject go = new GameObject("CubemapCamera");
        go.AddComponent<Camera>();
        go.transform.position = renderFromPosition.position;
        go.transform.rotation = Quaternion.identity;

        go.GetComponent<Camera>().RenderToCubemap(cubemap);

        DestroyImmediate(go);
    }

    [MenuItem("GameObject/Render into Cubemap")]
    private static void RenderCubemap()
    {
        ScriptableWizard.DisplayWizard<GenerateCubemap>("Render cubemap","Render!");

        //ScriptableWizard.DisplayWizard("Render cubemap", typeof (GenerateCubemap), "Render!");
    }
}



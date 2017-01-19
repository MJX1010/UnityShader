/*
	SetRenderQueue.cs
	
	Sets the RenderQueue of an object's materials on Awake. This will instance
	the materials, so the script won't interfere with other renderers that
	reference the same materials.
*/

using UnityEngine;

[AddComponentMenu("Rendering/SetRenderQueue")]

public class SetRenderQueue : MonoBehaviour {
	
	[SerializeField]
	protected int[] m_queues = new int[]{3000};
	
	protected void Awake() {
        /*Material[] materials = GetComponent<Renderer>().materials;
		for (int i = 0; i < materials.Length && i < m_queues.Length; ++i) {
			materials[i].renderQueue = m_queues[i];
		}*/
        SetQueue(this.transform);
	}

    void SetQueue(Transform father)
    {
        Renderer render = father.transform.GetComponent<Renderer>();
        if (render != null)
            render.material.renderQueue = m_queues[0];

        if (this.transform.childCount != 0)
        {
            Transform[] childs = new Transform[father.transform.childCount];
            for (int i = 0; i < childs.Length; i++)
            {
                childs[i] = father.GetChild(i);
                SetQueue(childs[i]);
            }
        }
    }
}
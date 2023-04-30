using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is used to grow a resource at runtime by changing the scale of the object it is attached to
public class ResourceController : MonoBehaviour
{
    float m_GrowFactor; // A float value to track the growth factor of the resource
    [SerializeField] float m_GrowSpeed; // The speed at which the resource should grow
    [HideInInspector] public bool m_IsReadyToHarvest; // A boolean flag indicating if the resource is ready to harvest
    [HideInInspector] public LandController m_LandController; // A reference to the LandController of the land the resource is planted on

    // The OnEnable method is called when the object is enabled. It starts the coroutine that grows the resource.
    private void OnEnable()
    {
        StartCoroutine(Co_GrowResource());
    }

    // The Co_GrowResource coroutine grows the resource until it reaches a scale of 2. It then sets the m_IsReadyToHarvest flag and notifies the LandController that it is ready to harvest.
    IEnumerator Co_GrowResource()
    {
        transform.localScale = Vector3.zero;
        while (m_GrowFactor < 2)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            transform.localScale = new Vector3(m_GrowFactor, m_GrowFactor, m_GrowFactor);
            m_GrowFactor += Time.deltaTime * m_GrowSpeed;
        }
        m_IsReadyToHarvest = true;
        m_LandController.ReadyToHarvest();
    }

    // The ResetResource method is used to reset the state of the resource when it is returned to the object pool.
    public void ResetResource()
    {
        StopAllCoroutines();
        m_GrowFactor = 0;
        m_IsReadyToHarvest = false;
    }
}

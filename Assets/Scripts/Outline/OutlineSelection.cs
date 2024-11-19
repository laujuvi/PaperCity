using System.Collections.Generic;
using UnityEngine;

public class OutlineSelection : MonoBehaviour
{
    [SerializeField] private GameObject _RaycastPoint; // Shared Raycast Point
    [SerializeField] private float _RaycastDistance;
    [SerializeField] private float _RaycastDistance_2;
    [SerializeField] private LayerMask interactableLayerMask_1;
    [SerializeField] private LayerMask interactableLayerMask_2;

    private Transform highlight;

    void Update()
    {
        // Disable previous highlight outline
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        // Get the closest interactable object for highlighting
        Transform closestHighlight = GetClosestHighlightObject();

        if (closestHighlight != null)
        {
            Outline outline = closestHighlight.GetComponent<Outline>();
            if (outline == null)
            {
                outline = closestHighlight.gameObject.AddComponent<Outline>();
                outline.OutlineColor = Color.magenta;
                outline.OutlineWidth = 10.0f;
            }
            outline.enabled = true;
            highlight = closestHighlight;
        }
    }

    private Transform GetClosestHighlightObject()
    {
        Ray ray = new Ray(_RaycastPoint.transform.position, _RaycastPoint.transform.forward);
        Ray ray_2 = new Ray(_RaycastPoint.transform.position, _RaycastPoint.transform.forward);

        // Collect all potential objects
        RaycastHit[] hits = Physics.RaycastAll(ray, _RaycastDistance, interactableLayerMask_1);
        RaycastHit[] hits_2 = Physics.RaycastAll(ray_2, _RaycastDistance_2, interactableLayerMask_2);

        Transform closestObject = null;
        float closestDistance = float.MaxValue;

        // Check both raycasts
        foreach (RaycastHit hit in hits)
        {
            float distance = Vector3.Distance(_RaycastPoint.transform.position, hit.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = hit.transform;
            }
        }

        foreach (RaycastHit hit_2 in hits_2)
        {
            float distance = Vector3.Distance(_RaycastPoint.transform.position, hit_2.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = hit_2.transform;
            }
        }

        return closestObject;
    }
}

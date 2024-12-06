using UnityEngine;

public class OutlineSelection : MonoBehaviour
{
    [SerializeField] private GameObject _RaycastPoint;
    [SerializeField] private float _RaycastDistance;
    [SerializeField] private LayerMask interactableLayerMask_1;
    [SerializeField] private LayerMask blockingLayers;

    private Transform highlight;

    void Update()
    {
        // Disable the previous highlight outline
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
                outline.OutlineWidth = 2f;
            }
            outline.enabled = true;
            highlight = closestHighlight;
        }
    }

    private Transform GetClosestHighlightObject()
    {
        Ray ray = new Ray(_RaycastPoint.transform.position, _RaycastPoint.transform.forward);

        // Perform a raycast and stop at blocking objects
        if (Physics.Raycast(ray, out RaycastHit hit, _RaycastDistance, blockingLayers | interactableLayerMask_1))
        {
            // Check if the hit object is on the interactable layer
            if (((1 << hit.transform.gameObject.layer) & interactableLayerMask_1) != 0)
            {
                return hit.transform;
            }
        }

        return null;
    }
}

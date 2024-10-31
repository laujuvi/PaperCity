using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    public float interactDistance = 2f;

    public Transform player;

    void Update()
    {
        //// Highlight
        //if (highlight != null)
        //{
        //    highlight.gameObject.GetComponent<Outline>().enabled = false;
        //    highlight = null;
        //}
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) //Make sure you have EventSystem in the hierarchy before using EventSystem
        //{
        //    highlight = raycastHit.transform;
        //    if (highlight.CompareTag("Selectable") && highlight != selection)
        //    {
        //        if (highlight.gameObject.GetComponent<Outline>() != null)
        //        {
        //            highlight.gameObject.GetComponent<Outline>().enabled = true;

        //        }
        //        else
        //        {
        //            Outline outline = highlight.gameObject.AddComponent<Outline>();
        //            outline.enabled = true;
        //            highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
        //            highlight.gameObject.GetComponent<Outline>().OutlineWidth = 10.0f;
        //        }
        //    }
        //    else
        //    {
        //        highlight = null;
        //    }
        //}

        //// Selection
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (highlight)
        //    {
        //        if (selection != null)
        //        {
        //            selection.gameObject.GetComponent<Outline>().enabled = false;
        //        }
        //        selection = raycastHit.transform;
        //        selection.gameObject.GetComponent<Outline>().enabled = true;
        //        highlight = null;
        //    }
        //    else
        //    {
        //        if (selection)
        //        {
        //            selection.gameObject.GetComponent<Outline>().enabled = false;
        //            selection = null;
        //        }
        //    }
        //}

        // Reset the highlight effect if an object was previously highlighted
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        // Create a ray from the player's position, casting forward in the direction the player is facing
        Ray ray = new Ray(player.position, player.forward);

        // Check if the ray hits an object within a certain distance (e.g., 10 units)
        if (Physics.Raycast(ray, out raycastHit, interactDistance))
        {
            // Assign the hit object to the highlight variable
            highlight = raycastHit.transform;

            // Check if the object is tagged as "Selectable" and is not currently selected
            if (highlight.CompareTag("Selectable") && highlight != selection)
            {
                // Enable or add the outline component if it doesn't exist
                Outline outline = highlight.gameObject.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = true;
                }
                else
                {
                    outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    outline.OutlineColor = Color.magenta;
                    outline.OutlineWidth = 10.0f;
                }
            }
            else
            {
                // Reset highlight if the object is not selectable
                highlight = null;
            }
        }
    }

}

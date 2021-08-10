using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection_Objects : MonoBehaviour
{
    [SerializeField] private string selectionTag = "Selectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterils;

    private Transform _selection;
    
   
    void FixedUpdate()
    {
        if(_selection != null)
        {
            var selectionRenderer = _selection.GetComponentInChildren<Renderer>();
            selectionRenderer.material = defaultMaterils;
            _selection = null;
            
        }
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            Logger.Log(hit);
            var selection = hit.transform;
            if(selection.CompareTag(selectionTag))
             {
                var selectionRenderer = selection.GetComponentInChildren<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material = highlightMaterial;
                }
                _selection = selection;
            }
        }
    }
}

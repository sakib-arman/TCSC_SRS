using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Selection_Objects : MonoBehaviour
{
    [SerializeField] LayerMask _interactLayer;
    //[SerializeField] private string selectionTag = "Selectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterils;
    //[SerializeField] Transform textHolder;
    //[SerializeField] Renderer selectionRenderer;

    private Transform _selectedObject;
    private void Awake()
    {
        _selectedObject = null;
    }


    void FixedUpdate()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, 1000f, _interactLayer))
        {
            _selectedObject = hit.transform;
            _selectedObject.GetComponentInChildren<Renderer>().material = highlightMaterial;
            _selectedObject.Find("Txt").gameObject.SetActive(true);
        }
        else
        {
            if (_selectedObject == null)
            {
                return;
            }
            _selectedObject.GetComponentInChildren<Renderer>().material = defaultMaterils;
            _selectedObject.Find("Txt").gameObject.SetActive(false);
            _selectedObject = null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class RotatablePowerButton : MonoBehaviour
{
    public float threshold = 34f; // Rotation in Degree
    private float _sensitivity;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private bool _isRotating;
    private Vector3 OnPosition = new Vector3(0, 180, 0);
    private Vector3 OffPosition = new Vector3(0, 220, 0);
    private ButtonKeyCode ButtonID = ButtonKeyCode.POWER;
    private float delay = 3;
    public Barrett2090 barrett;
    void Start()
    {
        _sensitivity = 0.4f;
        _rotation = Vector3.zero;
    }
    private void Update()
    {
        if (_isRotating)
        {
            if (transform.localEulerAngles.y > 230)
            {
                transform.localEulerAngles = OffPosition;
                return;
            }
            if (transform.localEulerAngles.y < 180)
            {
                transform.localEulerAngles = OnPosition;
                return;
            }
            // offset
            _mouseOffset = (Input.mousePosition - _mouseReference);
            // apply rotation
            //_rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;
            _rotation.y = -(_mouseOffset.x) * _sensitivity;
            //_rotation.x = -(_mouseOffset.y) * _sensitivity;
            // rotate
            //transform.Rotate(_rotation);
            transform.localEulerAngles += _rotation;
            // store mouse
            _mouseReference = Input.mousePosition;
        }
    }
    void OnMouseDown()
    {
        // rotating flag
        _isRotating = true;

        // store mouse
        _mouseReference = Input.mousePosition;
    }

    void OnMouseUp()
    {
        // rotating flag
        _isRotating = false;
        if (transform.localEulerAngles.y < 230 && transform.localEulerAngles.y > 220)
        {
            transform.localEulerAngles = OffPosition;
            barrett.PowerOnOff(false);
        }
        else if (transform.localEulerAngles.y < 185 && transform.localEulerAngles.y > 175)
        {
            transform.localEulerAngles = OnPosition;
            barrett.PowerOnOff(true);
        }

    }

}

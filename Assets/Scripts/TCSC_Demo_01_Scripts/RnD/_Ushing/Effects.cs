using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    [SerializeField] bool _canRepeat;
    [SerializeField] private float _lerpTime = 0.125f;
    [SerializeField] private float _speed = 4f;
    [SerializeField] Vector3 _startScale, _endScale;
    [SerializeField] Color _startColor, _endColor;

    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (_canRepeat)
        {
            // color Blink
            float fraction = Mathf.PingPong((Time.time - startTime), (_lerpTime * _speed));
            transform.GetComponentInChildren<Renderer>().material.color = Color.Lerp(_startColor, _endColor, fraction);
        }
        else
        {
            float fraction = (Time.time - startTime) / (_lerpTime * _speed);
            
            transform.GetComponentInChildren<Renderer>().material.color = Color.Lerp(_startColor, _endColor, fraction);
        }

    }
}

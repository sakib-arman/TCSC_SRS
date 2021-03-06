using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control_Rotation : MonoBehaviour
{
    private Quaternion startingRotation;
    public float speed = 10;

    public Slider RotationSpeed;
    float sliderValue;

    void Start()
    {
        RotationSpeed.onValueChanged.AddListener(delegate { valueChangecheck(); });
        //save the starting rotation
        startingRotation = this.transform.rotation;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            StopAllCoroutines();
            StartCoroutine(Rotate(0));
        }

        //go to 90 degrees with right arrow
        if (Input.GetKeyDown(KeyCode.L))
        {
            StopAllCoroutines();
            StartCoroutine(Rotate(90));
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            StopAllCoroutines();
            StartCoroutine(Rotate(180));
        }

        //go to -90 degrees with left arrow
        if (Input.GetKeyDown(KeyCode.R))
        {
            StopAllCoroutines();
            StartCoroutine(Rotate(-90));
        }


    }
    IEnumerator Rotate(float rotationAmount)
    {
        Quaternion finalRotation = Quaternion.Euler(0, rotationAmount, 0) * startingRotation;
        

        while (this.transform.rotation != finalRotation)
        {
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, finalRotation, Time.deltaTime * (speed * sliderValue));
            
            yield return 0;
        }
    }
    public void valueChangecheck()
    {
        sliderValue = RotationSpeed.value;
        Debug.Log(RotationSpeed.value);
    }
}

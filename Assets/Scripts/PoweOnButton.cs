using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoweOnButton : MonoBehaviour
{
    public GameObject LCD;
    bool Powered;
    Animator animator;
    void Start()
    {
        // LCD.SetActive (false);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Powered = !Powered;
            if (Powered)
            {
                animator.SetTrigger("PowerOn");

            }
            else
            {
                animator.SetTrigger("PowerOff");
                // LCD.transform.GetChild (0).GetComponent<TextMesh> ().text = "Powering Off";
            }
            StartCoroutine(PowerSwitch(1.5f));
        }

    }
    IEnumerator PowerSwitch(float second)
    {
        yield return new WaitForSecondsRealtime(second);
        LCD.SetActive(Powered);
        // LCD.transform.GetChild (0).GetComponent<TextMesh> ().text = "Initializing...";
        yield return new WaitForSecondsRealtime(2f);
        // LCD.transform.GetChild (0).GetComponent<TextMesh> ().text = "Select Mode";

    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log ("Power Button");
        }

    }
}
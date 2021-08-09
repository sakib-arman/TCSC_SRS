using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoggerGUI : MonoBehaviour
{
    public object message = "";
    public string mssg = "";
    Vector2 scrollPosition;
    public bool isGUIDebug = true;
    public void GUILog(object LogMessage)
    {
        message += LogMessage.ToString() + "\n";
        mssg = message.ToString();

    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {

            if (Input.GetKey(KeyCode.LeftShift))
            {

                if (Input.GetKey(KeyCode.G))
                {
                    isGUIDebug = true;
                }
            }
        }
    }
    void OnGUI()
    {
        if (isGUIDebug)
        {
            scrollPosition = GUILayout.BeginScrollView(
           scrollPosition, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height));
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Clear", GUILayout.Width(50)))
            {
                message = "";
            }
            if (GUILayout.Button("Hide", GUILayout.Width(50)))
            {
                this.isGUIDebug = false;
            }
            GUILayout.EndHorizontal();
            GUILayout.Label(message.ToString());
            GUILayout.EndScrollView();
        }

    }
}

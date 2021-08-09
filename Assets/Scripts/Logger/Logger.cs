using System.Diagnostics;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Logger
{

    public static void Log(object messages)
    {
        if (GLOBALTAG.isDebug)
        {
            UnityEngine.Debug.Log( messages.ToString() );
        }
        else
        {
            string method = new StackTrace().GetFrame(1).GetMethod().Name;
            File.AppendAllText(GLOBALTAG.LogFileName,
                 System.DateTime.Now.ToString("HH:mm:ss >> ") + method + "()>>" + messages.ToString() + Environment.NewLine);
        }
        //SetMessageInGUI("<color=blue>" + messages + "</color>");
    }
    public static void Log(object gameObject, object messages)
    {
        if (GLOBALTAG.isDebug)
        {
            UnityEngine.Debug.Log( messages.ToString() );
        }
        else
        {
            string method = new StackTrace().GetFrame(1).GetMethod().Name;
            File.AppendAllText(GLOBALTAG.LogFileName,
                 System.DateTime.Now.ToString("HH:mm:ss >> ") + gameObject.GetType().Name + "{}>>" + method + "()>>" + messages.ToString() + Environment.NewLine);
        }
        SetMessageInGUI("<color=blue>" + messages + "</color>");
    }
    public static void LogError(object gameObject, object messages)
    {
        if (GLOBALTAG.isDebug)
        {
            UnityEngine.Debug.Log("<color=red>" + messages.ToString() + "</color>");
        }
        else
        {
            string method = new StackTrace().GetFrame(1).GetMethod().Name;
            File.AppendAllText(GLOBALTAG.LogFileName,
                 System.DateTime.Now.ToString("HH:mm:ss >> ") + gameObject.GetType().Name + "{}>>" + method + "()>>" + messages.ToString() + Environment.NewLine);
        }
        SetMessageInGUI("<color=red>" + messages + "</color>");
    }
    public static void LogError(object messages)
    {
        if (GLOBALTAG.isDebug)
        {
            UnityEngine.Debug.Log("<color=red>" + messages.ToString() + "</color>");
        }
        else
        {
            string method = new StackTrace().GetFrame(1).GetMethod().Name;
            File.AppendAllText(GLOBALTAG.LogFileName,
                 System.DateTime.Now.ToString("HH:mm:ss >> ")  + method + "()>>" + messages.ToString() + Environment.NewLine);
        }
        SetMessageInGUI("<color=red>" + messages + "</color>");
    }

    public static void Message(string message)
    {
        //if (GLOBALTAG.isDebug)
        //{
        //    EditorUtility.DisplayDialog("Info", message, "OK", "Cancel");
        //}
    }
    private static void SetMessageInGUI(object message)
    {
        GameObject.FindObjectOfType<LoggerGUI>().GUILog(message);
    }

}

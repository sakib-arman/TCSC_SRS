using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionTest : MonoBehaviour
{
    public Dictionary<string, Dictionary<string, string>> info =
     new Dictionary<string, Dictionary<string, string>>
     {
        {
            "General",
            new Dictionary<string, string>
            {
                {"name", "Genesis"},
                {"chapters", "50"},
                {"before", ""},
                {"after", "Exod"}
            }
        },
        {
            "Exod",
            new Dictionary<string, string>
            {
                {"name", "Exodus"},
                {"chapters", "40"},
                {"before", "Gen"},
                {"after", "Lev"}
            }
        }
     };
    void Start()
    {
        foreach (var item in info)
        {
            Logger.Log(this, item.Key.ToString());
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}

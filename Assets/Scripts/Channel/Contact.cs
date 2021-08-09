using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contact 
{
    public int ID { set; get; } = 0;
    public string Label { set; get; } = "Undefinite";
    public bool isLinked = false;
    public Contact(string Label, int ID, bool isLinked)
    {
        this.Label = Label;
        this.ID = ID;
        this.isLinked = isLinked;
    }
    public Contact(int ID)
    {
        this.ID = ID;
    }
}

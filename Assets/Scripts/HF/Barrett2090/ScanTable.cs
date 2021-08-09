using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanTable
{
    public string identifier= "ScanListAction"; //Non Edittable Name
    public string tableLabel = "Table 1";
    public List<Channel> scanList = new List<Channel>(20);
    public ScanTable( string tableLabel)
    {
        this.tableLabel = tableLabel;

    }

}

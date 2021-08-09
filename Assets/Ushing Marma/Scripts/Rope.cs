using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] GameObject partPrefabs, parentObject;

    [SerializeField]
    [Range(1, 1000)]
    int length = 1;

    [SerializeField] float partDistanc = 0.21f;

    [SerializeField] bool reset, spawn, snapfirst, snapLast;
    void Start()
    {
        
    }

    void Update()
    {
       if(reset)
        {
            foreach(GameObject tap in GameObject.FindGameObjectsWithTag("Player"))
            {
                Destroy(tap);
            }
            reset = false;
        }
       if(spawn)
        {
            Spawn();
            spawn = false;
        }
    }
    public void Spawn()
    {
        int count = (int)(length/partDistanc);
        for(int x= 0;x<count;x++)
        {
            GameObject tap;
           tap =  Instantiate(partPrefabs, new Vector3(transform.position.x, transform.position.y * partDistanc *(x + 1), transform.position.z), Quaternion.identity, parentObject.transform);
           tap.name = parentObject.transform.childCount.ToString();
            tap.transform.eulerAngles = new Vector3(180, 0, 0);
            if(x == 0)
            {
                Destroy(tap.GetComponent<CharacterJoint>());
                if(snapfirst)
                {
                    tap.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            }
            else
            {
                tap.GetComponent<CharacterJoint>().connectedBody = parentObject.transform.Find((parentObject.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();
            }
        }
        if(snapLast)
        {
            parentObject.transform.Find((parentObject.transform.childCount).ToString()).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}

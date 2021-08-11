using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentProfileControl : MonoBehaviour
{
    public GameObject basicInfoBody;
    public GameObject loiginIDpassBody;
    public GameObject userLevelBody;
    //public GameObject userActivityBody;

    void Start()
    {
        basicInfoBody.SetActive(true);
        loiginIDpassBody.SetActive(false);
        userLevelBody.SetActive(false);
    }

    public void openBasicInfoBody()
    {
        basicInfoBody.SetActive(true);
        loiginIDpassBody.SetActive(false);
        userLevelBody.SetActive(false);
    }

    public void openLoginIDpassBody()
    {
        basicInfoBody.SetActive(false);
        loiginIDpassBody.SetActive(true);
        userLevelBody.SetActive(false);
    }

    public void openUserLevelBody()
    {
        basicInfoBody.SetActive(false);
        loiginIDpassBody.SetActive(false);
        userLevelBody.SetActive(true);
    }


}

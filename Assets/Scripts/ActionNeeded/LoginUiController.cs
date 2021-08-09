using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginUiController : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;
    public void LoginButtonTrigger()
    {
        animator.SetTrigger("Pressed");

    }
    public void LoggedIn()
    {
        SceneManager.LoadScene(1);
    }


}

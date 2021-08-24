using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    public GameObject welcomeScreen;
    public GameObject registrationScreen;
    public GameObject registrationSuccessfullScreen;
    public GameObject loginScreen;
    public GameObject modeSelectionScreen;
    public GameObject nonGuidedScreen;
    public GameObject GuidedScreen;
    public GameObject EWScreen;
    public GameObject profileScreen;

    public GameObject systemTypePanel;
    public GameObject narrativesPanel;

    public GameObject AnalyzerPanel;
    public GameObject MDFpanel;
    public GameObject jammerPanel;

    int selected;

    void Start()
    {
        
        welcomeScreen.SetActive(false);
        registrationScreen.SetActive(false);
        registrationSuccessfullScreen.SetActive(false);
        loginScreen.SetActive(false);
        modeSelectionScreen.SetActive(false);
        nonGuidedScreen.SetActive(true);
        GuidedScreen.SetActive(false);
        EWScreen.SetActive(false);
        profileScreen.SetActive(false);
    }

    public void Update()
    {
        selected = PlayerPrefs.GetInt("selectedType");
    }

    public void openWelcomeScreen()
    {
        welcomeScreen.SetActive(true);
        registrationScreen.SetActive(false);
        registrationSuccessfullScreen.SetActive(false);
        loginScreen.SetActive(false);
        modeSelectionScreen.SetActive(false);
        nonGuidedScreen.SetActive(false);
        GuidedScreen.SetActive(false);
        EWScreen.SetActive(false);
        profileScreen.SetActive(false);
    }

    public void openRegistrationScreen()
    {
        welcomeScreen.SetActive(false);
        registrationScreen.SetActive(true);
        registrationSuccessfullScreen.SetActive(false);
        loginScreen.SetActive(false);
        modeSelectionScreen.SetActive(false);
        nonGuidedScreen.SetActive(false);
        GuidedScreen.SetActive(false);
        EWScreen.SetActive(false);
        profileScreen.SetActive(false);
    }

    public void openRegistrationSuccessfullScreen()
    {
        welcomeScreen.SetActive(false);
        registrationScreen.SetActive(false);
        registrationSuccessfullScreen.SetActive(true);
        loginScreen.SetActive(false);
        modeSelectionScreen.SetActive(false);
        nonGuidedScreen.SetActive(false);
        GuidedScreen.SetActive(false);
        EWScreen.SetActive(false);
        profileScreen.SetActive(false);
    }
    public void openLoginScreen()
    {
        welcomeScreen.SetActive(false);
        registrationScreen.SetActive(false);
        registrationSuccessfullScreen.SetActive(false);
        loginScreen.SetActive(true);
        modeSelectionScreen.SetActive(false);
        nonGuidedScreen.SetActive(false);
        GuidedScreen.SetActive(false);
        EWScreen.SetActive(false);
        profileScreen.SetActive(false);
    }

    public void openModeSelectionScreen()
    {
        systemTypePanel.SetActive(false);
        narrativesPanel.SetActive(false);
        welcomeScreen.SetActive(false);
        registrationScreen.SetActive(false);
        registrationSuccessfullScreen.SetActive(false);
        loginScreen.SetActive(false);
        modeSelectionScreen.SetActive(true);
        nonGuidedScreen.SetActive(false);
        GuidedScreen.SetActive(false);
        EWScreen.SetActive(false);
        profileScreen.SetActive(false);


    }

    public void openNonGuidedScreen()
    {
        welcomeScreen.SetActive(false);
        registrationScreen.SetActive(false);
        registrationSuccessfullScreen.SetActive(false);
        loginScreen.SetActive(false);
        modeSelectionScreen.SetActive(false);
        nonGuidedScreen.SetActive(true);
        GuidedScreen.SetActive(false);
        EWScreen.SetActive(false);
        profileScreen.SetActive(false);
    }

    public void openEwScreen()
    {
        welcomeScreen.SetActive(false);
        registrationScreen.SetActive(false);
        registrationSuccessfullScreen.SetActive(false);
        loginScreen.SetActive(false);
        modeSelectionScreen.SetActive(false);
        nonGuidedScreen.SetActive(false);
        GuidedScreen.SetActive(false);
        EWScreen.SetActive(true);
        profileScreen.SetActive(false);
    }

    public void openProfileScreen()
    {
        welcomeScreen.SetActive(false);
        registrationScreen.SetActive(false);
        registrationSuccessfullScreen.SetActive(false);
        loginScreen.SetActive(false);
        modeSelectionScreen.SetActive(false);
        nonGuidedScreen.SetActive(false);
        GuidedScreen.SetActive(false);
        EWScreen.SetActive(false);
        profileScreen.SetActive(true);
    }
    public void closeProfileScreen()
    {
        welcomeScreen.SetActive(false);
        registrationScreen.SetActive(false);
        registrationSuccessfullScreen.SetActive(false);
        loginScreen.SetActive(false);
        modeSelectionScreen.SetActive(true);
        nonGuidedScreen.SetActive(false);
        GuidedScreen.SetActive(false);
        EWScreen.SetActive(false);
        profileScreen.SetActive(false);
    }
    public void openGuidedScreen()
    {
        welcomeScreen.SetActive(false);
        registrationScreen.SetActive(false);
        registrationSuccessfullScreen.SetActive(false);
        loginScreen.SetActive(false);
        modeSelectionScreen.SetActive(false);
        nonGuidedScreen.SetActive(false);
        GuidedScreen.SetActive(true);
        EWScreen.SetActive(false);
        profileScreen.SetActive(false);
    }

    public void openAnalyzer()
    {
        AnalyzerPanel.SetActive(true);
        MDFpanel.SetActive(false);
        jammerPanel.SetActive(false);
    }
    public void closeAnalyzer()
    {
        AnalyzerPanel.SetActive(false);
        MDFpanel.SetActive(false);
        jammerPanel.SetActive(false);
    }
    public void openMDF()
    {
        AnalyzerPanel.SetActive(false);
        MDFpanel.SetActive(true);
        jammerPanel.SetActive(false);
    }
    public void closeMDF()
    {
        AnalyzerPanel.SetActive(false);
        MDFpanel.SetActive(false);
        jammerPanel.SetActive(false);
    }
    public void openJammer()
    {
        AnalyzerPanel.SetActive(false);
        MDFpanel.SetActive(false);
        jammerPanel.SetActive(true);
    }
    public void closeJammer()
    {
        AnalyzerPanel.SetActive(false);
        MDFpanel.SetActive(false);
        jammerPanel.SetActive(false);
    }

    public void nextButtonFunction()
    {
        if(selected==1)
        {
            openAnalyzer();
        }
        else if(selected == 2)
        {
            openMDF();
        }
        else if(selected == 3)
        {
            openJammer();
        }
    }

}

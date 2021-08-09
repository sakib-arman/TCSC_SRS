using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModeButtonControl : MonoBehaviour
{
    public Text Title;
    public Text Info;
    public GameObject TrainingArrow;
    public GameObject ExerciseArrow;
    public GameObject ExamArrow;


    private void Awake()
    {
        Title.text = "Training Info";
        Info.text = "Individual Training\nAll Open Options\nAssist/Guided Training\nWarning\nEquipment Information";
        TrainingArrow.SetActive(true);
        ExerciseArrow.SetActive(false);
        ExamArrow.SetActive(false);
    }
    public void ShowTrainingInfo( )
    {
        Title.text = "Training Info";
        Info.text = "Individual Training\nAll Open Options\nAssist/Guided Training\nWarning\nEquipment Information";
        TrainingArrow.SetActive(true);
        ExerciseArrow.SetActive(false);
        ExamArrow.SetActive(false);
    }
    public void ShowExerciseInfo()
    {
        Title.text = "Exercise Info";
        Info.text = "Multiplayer Training\nTasked Exercise\nRealtime Voice Communication\nNo Assistance\nWarning";
        TrainingArrow.SetActive(false);
        ExerciseArrow.SetActive(true);
        ExamArrow.SetActive(false);
    }
    public void ShowExamInfo()
    {
        Title.text = "Exam Info";
        Info.text = "Individual\nAssigned Exam\nNo Assistance\nNo Warning";
        TrainingArrow.SetActive(false);
        ExerciseArrow.SetActive(false);
        ExamArrow.SetActive(true);
    }
    public void MainInterface()
    {
        SceneManager.LoadScene(2);
    } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionController : MonoBehaviour
{
    [SerializeField]
    public Question[] character1Questions;
    public Question[] character2Questions;
    public Question[] character3Questions;
    public Question[] character4Questions;
    public GameObject blankButton;
    public GameObject questionsBox;
    public Question[] activeQuestionSet;

    public void OnCharacterSelect(int number)
    {
        // destroy children questions already there
        foreach (Transform t in questionsBox.transform)
        {
            Destroy(t.gameObject);
        }

        if(number == 1) 
        {
            activeQuestionSet = character1Questions;
        }
        else if (number == 2)
        {
            activeQuestionSet = character2Questions;
        }
        else if (number == 3)
        {
            activeQuestionSet = character3Questions;
        }
        else
        {
            activeQuestionSet = character4Questions;
        }

        foreach(Question q in activeQuestionSet)
        {
            if(q.available)
            {
                GameObject button = Instantiate(blankButton, questionsBox.transform);
                button.GetComponentInChildren<TMP_Text>().text = q.text;
                button.GetComponent<DialogueStarter>().entry = q.entry;
            }
        }
    }
}

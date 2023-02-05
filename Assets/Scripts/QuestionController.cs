using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour
{
    [SerializeField]
    public Question[] character1Questions;
    public Question[] character2Questions;
    public Question[] character3Questions;
    public Question[] character4Questions;
    public Question[] character5Questions;
    public GameObject blankButton;
    public GameObject questionsBox;
    public Question[] activeQuestionSet;
    public GameObject char1, char2, char3, char4, char5;
    private float xi1, xi2, xi3, xi4, xi5;
    public DialogueEntry next1, next2, next3, next4, next5;
    private Coroutine movingCoroutine;
    public DialogueController dc;
    public DialogueEntry charSelect;
    public DialogueEntry next;
    

    void Start()
    {
        // get initial positions so we can move them back
        xi1 = char1.GetComponent<RectTransform>().position.x;
        xi2 = char2.GetComponent<RectTransform>().position.x;
        xi3 = char3.GetComponent<RectTransform>().position.x;
        xi4 = char4.GetComponent<RectTransform>().position.x;
        xi5 = char5.GetComponent<RectTransform>().position.x;
    }

    IEnumerator MoveToLocation(GameObject character, float initx, float newx, bool enableOthers)
    {
        RectTransform rt = character.GetComponent<RectTransform>();

        for(int i = 0; i <=100; i++) 
        {
            rt.position = Vector3.Lerp(new Vector3(initx, rt.position.y, rt.position.z), new Vector3(newx, rt.position.y, rt.position.z), i*0.01f);
            yield return new WaitForSeconds(0.01f);
        }

        if(enableOthers)
        {
            char1.SetActive(true);
            char2.SetActive(true);
            char3.SetActive(true);
            char4.SetActive(true);
            char5.SetActive(true);
            character.GetComponent<Button>().interactable = true;
            dc.HandleCallback("character_select", new Object[] {charSelect});
        }
        else
        {
            dc.HandleCallback("next", new Object[] {next});
        }
    }

    public void OnCharacterSelect(int number)
    {
        if(number == 1) 
        {
            activeQuestionSet = character1Questions;
            next = next1;
            // disable other characters
            char2.SetActive(false);
            char3.SetActive(false);
            char4.SetActive(false);
            char5.SetActive(false);
            // start the message typing
            movingCoroutine = StartCoroutine(MoveToLocation(char1, xi1, 500, false));
            // disable button
            char1.GetComponent<Button>().interactable = false;
        }
        else if (number == 2)
        {
            activeQuestionSet = character2Questions;
            next = next2;
            // disable other characters
            char1.SetActive(false);
            char3.SetActive(false);
            char4.SetActive(false);
            char5.SetActive(false);
            // start the message typing
            movingCoroutine = StartCoroutine(MoveToLocation(char2, xi2, 500, false));
            // disable button
            char2.GetComponent<Button>().interactable = false;
        }
        else if (number == 3)
        {
            activeQuestionSet = character3Questions;
            next = next3;
            // disable other characters
            char1.SetActive(false);
            char2.SetActive(false);
            char4.SetActive(false);
            char5.SetActive(false);
            // start the message typing
            movingCoroutine = StartCoroutine(MoveToLocation(char3, xi3, 500, false));
            // disable button
            char3.GetComponent<Button>().interactable = false;
        }
        else if (number == 4)
        {
            activeQuestionSet = character4Questions;
            next = next4;
            // disable other characters
            char1.SetActive(false);
            char2.SetActive(false);
            char3.SetActive(false);
            char5.SetActive(false);
            // start the message typing
            movingCoroutine = StartCoroutine(MoveToLocation(char4, xi4, 500, false));
            // disable button
            char4.GetComponent<Button>().interactable = false;
        }
        else
        {
            activeQuestionSet = character5Questions;
            next = next5;
            // disable other characters
            char1.SetActive(false);
            char2.SetActive(false);
            char3.SetActive(false);
            char4.SetActive(false);
            // start the message typing
            movingCoroutine = StartCoroutine(MoveToLocation(char5, xi5, 500, false));
            // disable button
            char5.GetComponent<Button>().interactable = false;
        }
    }

    public void LoadQuestions()
    {
        // destroy children questions already there
        foreach (Transform t in questionsBox.transform)
        {
            Destroy(t.gameObject);
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

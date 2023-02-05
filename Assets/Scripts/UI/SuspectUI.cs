using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SuspectUI : UILayer
{
    [SerializeField] private List<GameObject> Buttons;
    [SerializeField] private Button CloseButton;
    [SerializeField] private List<string> KeySuspects;
    [SerializeField] private Button Submit;

    private string Selected = "";

    private void Start()
    {
        foreach (GameObject obj in Buttons)
        {
            Button btn = obj.GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                Selected = obj.gameObject.name;
            });

            if (KeySuspects.Contains(obj.gameObject.name))
            {
                Image img = obj.GetComponent<Image>();
                img.color = Color.yellow;
            }
            else
            {
                btn.interactable = false;
            }
        }

        Submit.onClick.AddListener(() =>
        {
            GameManager gm = FindObjectOfType<GameManager>();
            gm.InvokeGameChange("signal", Selected);
            gm.InvokeGameChange("end", Selected);
        });
    }

    private void Update()
    {
        if (Selected == "")
        {
            Submit.interactable = false;
        }
        else
        {
            Submit.interactable = true;
        }
    }
}

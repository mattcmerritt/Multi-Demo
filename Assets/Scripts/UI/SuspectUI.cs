using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SuspectUI : UILayer
{
    [SerializeField] private List<GameObject> Buttons;
    [SerializeField] private Button CloseButton;
    [SerializeField] private List<string> KeySuspects;

    private void Start()
    {
        foreach (GameObject obj in Buttons)
        {
            Button btn = obj.GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                Debug.Log(obj.gameObject.name);
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

        CloseButton.onClick.AddListener(() =>
        {
            Deactivate();
        });
    }
}

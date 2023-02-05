using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject MainCanvas;
    [SerializeField] private GameObject WinPanel, LosePanel;
    [SerializeField] private CameraController Cameras;

    public void InvokeGameChange(string type, string value)
    {
        if (type == "end")
        {
            Cameras.ReturnToMain();
            if (value == "Caroline")
            {
                WinPanel.SetActive(true);
            }
            else
            {
                LosePanel.SetActive(true);
            }
        }
    }
}

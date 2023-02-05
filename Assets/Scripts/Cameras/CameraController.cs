using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject MainCamera, Camera1, Camera2;
    [SerializeField] private GameObject MainPanel, Panel1, Panel2;

    public void DisableMain()
    {
        MainCamera.SetActive(false);
        MainPanel.SetActive(false);
    }

    public void Activate1()
    {
        DisableMain();
        Camera1.SetActive(true);
        Camera2.SetActive(false);

        Panel1.SetActive(true);
        Panel2.SetActive(false);
    }

    public void Activate2()
    {
        DisableMain();
        Camera2.SetActive(true);
        Camera1.SetActive(false);

        Panel2.SetActive(true);
        Panel1.SetActive(false);
    }

    public void ReturnToMain()
    {
        Camera1.SetActive(false);
        Camera2.SetActive(false);
        MainCamera.SetActive(true);

        MainPanel.SetActive(true);
        Panel1.SetActive(false);
        Panel2.SetActive(false);
    }
}

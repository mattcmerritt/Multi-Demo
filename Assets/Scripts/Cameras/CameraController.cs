using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject MainCamera, Camera1, Camera2;

    public void DisableMain()
    {
        MainCamera.SetActive(false);
    }

    public void Activate1()
    {
        DisableMain();
        Camera1.SetActive(true);
        Camera2.SetActive(false);
    }

    public void Activate2()
    {
        DisableMain();
        Camera2.SetActive(true);
        Camera1.SetActive(false);
    }
}

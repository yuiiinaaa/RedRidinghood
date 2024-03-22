using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    
    public static CameraManager Instance { get; private set; }
    public enum CameraMode { ThirdPerson, FirstPerson }
    public CameraMode cameraMode;
    public GameObject thirdPOVCamera;
    public GameObject firstPOVCamera;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ChangeCameraMode(cameraMode);
    }

    private void OnValidate()
    {
        ChangeCameraMode(cameraMode);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeCameraMode(CameraMode.ThirdPerson);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeCameraMode(CameraMode.FirstPerson);
        }
        
    }

    void ChangeCameraMode(CameraMode mode)
    {
        cameraMode = mode;
        thirdPOVCamera.SetActive(false);
        firstPOVCamera.SetActive(false);

        switch (cameraMode)
        {
            case CameraMode.ThirdPerson:
                thirdPOVCamera.SetActive(true);
                break;
            case CameraMode.FirstPerson:
                firstPOVCamera.SetActive(true);
                break;
            default:
                break;
        }
    }
}

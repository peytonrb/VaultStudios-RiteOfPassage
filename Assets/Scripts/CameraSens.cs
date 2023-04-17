using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSens : MonoBehaviour
{
    public CinemachineFreeLook cam;

    // Start is called before the first frame update
    void Start()
    {
        cam.m_XAxis.m_MaxSpeed = GameManager.Instance.xSens * 50f;
        cam.m_YAxis.m_MaxSpeed = GameManager.Instance.ySens * 5f;
    }
}

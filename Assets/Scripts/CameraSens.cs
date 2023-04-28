using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSens : MonoBehaviour
{
    public CinemachineFreeLook cam;

    // Start is called before the first frame update
    void Update()
    {
        cam.m_XAxis.m_MaxSpeed = 0.5f + GameManager.Instance.xSens * 10f;
        cam.m_YAxis.m_MaxSpeed = 0.005f + (GameManager.Instance.ySens * 0.1f);
    }
}

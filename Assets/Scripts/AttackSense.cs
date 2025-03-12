using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AttackSense : MonoBehaviour
{
    private static AttackSense instance;
    public static AttackSense Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AttackSense>();
            }
            return instance;
        }
    }

    private bool isShake;
    private CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        if (impulseSource == null)
        {
            Debug.LogError("CinemachineImpulseSource component not found!");
        }
    }

    public void HitPause(float duration)
    {
        StartCoroutine(Pause(duration)); // 调用协程 暂停时间增加打击感
    }

    IEnumerator Pause(float duration)
    {
        float pauseTime = duration / 60f;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(pauseTime);
        Time.timeScale = 1;
    }

    public void CameraShake(float strength, float shakeTime)
    {
        if (impulseSource != null)
        {
            impulseSource.m_ImpulseDefinition.m_TimeEnvelope.m_SustainTime = shakeTime;
            impulseSource.GenerateImpulse(strength);
        }
        else
        {
            Debug.LogError("CinemachineImpulseSource component is null!");
        }
    }
}

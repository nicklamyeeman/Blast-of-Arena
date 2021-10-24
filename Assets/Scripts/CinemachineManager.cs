using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineManager : MonoBehaviour
{
    public static CinemachineManager Instance { get; private set; }

    private CinemachineVirtualCamera vcam;

    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    private int currentSpectateCamera = -1;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        vcam.m_Lens.OrthographicSize = 4;
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin perlin =
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        perlin.m_AmplitudeGain = intensity;
        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }

    public void SpectateMode(string tagToFollow)
    {
        GameObject playerLight;
        GameObject[] playerEntities = GameObject.FindGameObjectsWithTag(tagToFollow);

        if (playerEntities.Length <= 1)
            return;
        if (currentSpectateCamera >= 0)
        {
            playerLight = playerEntities[currentSpectateCamera].transform.GetChild(3).gameObject;
            playerLight.SetActive(false);
        }

        currentSpectateCamera += 1;
        if (currentSpectateCamera >= playerEntities.Length)
            currentSpectateCamera = 0;
        vcam.Follow = playerEntities[currentSpectateCamera].transform;
        playerLight = playerEntities[currentSpectateCamera].transform.GetChild(3).gameObject;
        playerLight.SetActive(true);

    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            CinemachineBasicMultiChannelPerlin perlin =
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            perlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, shakeTimer / shakeTimerTotal);
        }
    }
}

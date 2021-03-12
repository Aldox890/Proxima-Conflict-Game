using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public GameObject virtualCamera;

    public float maxDistance;
    public float minDistance;
    private float currentDistance;

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    public float shakeTimer;
    void Start()
    {
        currentDistance = maxDistance;
        cinemachineVirtualCamera = virtualCamera.GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float axis = Input.GetAxis("Mouse ScrollWheel");
        
        if(axis > 0)
        {
            if (currentDistance > minDistance) currentDistance += axis * -15f;
        }
        else if (axis < 0)
        {
            if (currentDistance < maxDistance) currentDistance += axis * -15f;
        }

        if (target) 
        {
            //transform.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            transform.position = Vector3.Slerp(transform.position, new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z), 2f * Time.deltaTime);
            transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, currentDistance, transform.position.z), 5f * Time.deltaTime);
        }

        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }

    public void shake(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }
}

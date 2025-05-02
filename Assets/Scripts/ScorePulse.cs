using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class ScorePulse : MonoBehaviour
{
    private Vector3 startingScale;

    [SerializeField] private AudioSource hitSound;
    [SerializeField] private AudioSource missSound;

    [SerializeField] private float decayScale = 5.0f;
    [SerializeField] private float pulseScale = 1.05f;
    [SerializeField] private float maximumScale = 2.0f;
    [SerializeField] private CinemachineImpulseSource listener;
 
    private void Start()
    {
        startingScale = transform.localScale;
    }

    public void Pulse()
    {
        hitSound.Play();

        transform.localScale = transform.localScale * pulseScale;
        if (transform.localScale.x > maximumScale)
        {
            transform.localScale = new Vector3(maximumScale, maximumScale, maximumScale);
        }
    }

    public void PulseOnError()
    {
        GameManager.Instance.ApplyImpulse();
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, startingScale, Time.deltaTime * decayScale);
    }
}

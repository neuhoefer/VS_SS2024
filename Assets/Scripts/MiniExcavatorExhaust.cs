using UnityEngine;
using VehiclePhysics.Specialized;

[RequireComponent (typeof(ParticleSystem))]

public class MiniExcavatorExhaust : MonoBehaviour
{
    [SerializeField] VPHydraulicTrackedVehicleControllerInput m_VehicleControllerInput;
    [SerializeField] private float m_AlphaAtIdle = 30.0f;
    [SerializeField] private float m_AlphaAtPeak = 170.0f;

    private ParticleSystem m_ParticleSystem;

    void Start()
    {
        m_ParticleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (m_VehicleControllerInput.ignitionKey == -1)
        {
            m_ParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
        else
        {
            if (m_ParticleSystem.isStopped)
            {
                m_ParticleSystem.Play(true);
            }

            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
            GradientColorKey[] colorKeys = new GradientColorKey[2];

            float alpha = m_AlphaAtIdle + m_VehicleControllerInput.idleControlInput * (m_AlphaAtPeak - m_AlphaAtIdle);

            alphaKeys[0].alpha = alpha / 255.0f; // variabel
            alphaKeys[0].time = 0f;
            colorKeys[0].color = Color.black;
            colorKeys[0].time = 0f;

            alphaKeys[1].alpha = 0f;
            alphaKeys[1].time = 1f;
            colorKeys[1].color = Color.gray;
            colorKeys[1].time = 1f;

            Gradient gradient = new Gradient();
            gradient.SetKeys(colorKeys, alphaKeys);

            ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule = m_ParticleSystem.colorOverLifetime;
            colorOverLifetimeModule.color = gradient;
        }
    }
}

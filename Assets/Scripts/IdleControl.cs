using UnityEngine;
using UnityEngine.UI;
using VehiclePhysics.Specialized;

public class IdleControl : MonoBehaviour
{
    [SerializeField] VPHydraulicTrackedVehicleControllerInput m_VehicleControllerInput;
    [SerializeField] ParticleSystem m_ParticleSystem;

    private float m_IdleControlInput;

    void Start()
    {
        gameObject.GetComponent<Slider>().onValueChanged.AddListener(TaskOnValueChanged);
        m_IdleControlInput = m_VehicleControllerInput.idleControlInput;
        SetParticleSystem();
    }

    private void TaskOnValueChanged(float value)
    {
        m_IdleControlInput = value;
        m_VehicleControllerInput.idleControlInput = m_IdleControlInput;
        SetParticleSystem();
    }

    private void SetParticleSystem()
    {
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        GradientColorKey[] colorKeys = new GradientColorKey[2];

        float m_AlphaAtIdle = 30.0f;
        float m_AlphaAtPeak = 170.0f;
        float alpha = m_AlphaAtIdle + m_IdleControlInput * (m_AlphaAtPeak - m_AlphaAtIdle);

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

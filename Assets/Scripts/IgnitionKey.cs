using UnityEngine;
using UnityEngine.UI;
using VehiclePhysics.Specialized;

public class IgnitionKey : MonoBehaviour
{
    [SerializeField] VPHydraulicTrackedVehicleControllerInput m_VehicleControllerInput;
    [SerializeField] ParticleSystem m_ParticleSystem;

    private int m_State;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        m_State = m_VehicleControllerInput.ignitionKey;
        SetRotationAndParticleSystem();
    }

    private void TaskOnClick()
    {
        if (m_State == -1)
        {
            m_State = 0;
        }
        else if (m_State == 0)
        {
            m_State = -1;
        }
        else if (m_State == 1)
        {
            m_State = 0;
        }
        m_VehicleControllerInput.ignitionKey = m_State;
        SetRotationAndParticleSystem();
    }

    private void SetRotationAndParticleSystem()
    {
        switch (m_State)
        {
            case -1:
                transform.eulerAngles = new Vector3(0, 0, 0);
                m_ParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                break;
            case 0:
                transform.eulerAngles = new Vector3(0, 0, -30);
                m_ParticleSystem.Play(true);
                break;
            case 1:
                transform.eulerAngles = new Vector3(0, 0, -60);
                break;
        }
    }
}

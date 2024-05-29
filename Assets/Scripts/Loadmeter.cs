using UnityEngine;
using VehiclePhysics;
using VehiclePhysics.Specialized;

public class Loadmeter : MonoBehaviour
{
    [SerializeField] private VPHydraulicTrackedVehicleController m_VehicleController;
    [SerializeField] private Transform m_Needle;

    private int[] m_VehicleData;
    private const float MAX_LOAD = 100.0f;
    private const float ZERO_LOAD_ANGLE = 90.0f;
    private const float MAX_LOAD_ANGLE = -90.0f;

    void Start()
    {
        m_VehicleData = m_VehicleController.data.Get(VehiclePhysics.Channel.Vehicle);
    }


    void Update()
    {
        float load = m_VehicleData[VehicleData.EngineLoad] / 10.0f;
        float needleAngle = ZERO_LOAD_ANGLE + load / MAX_LOAD * (MAX_LOAD_ANGLE - ZERO_LOAD_ANGLE);
        m_Needle.eulerAngles = new Vector3(0, 0, needleAngle);
    }
}

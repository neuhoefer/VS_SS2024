using UnityEngine;
using VehiclePhysics.Specialized;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private VPHydraulicTrackedVehicleController m_VehicleController;
    [SerializeField] private Transform m_Needle;

    private const float MAX_SPEED = 3.5f;
    private const float MAX_SPEED_ANGLE = -90;

    void Start()
    {
        
    }


    void Update()
    {
        float speed = m_VehicleController.speed * 3.6f;
        float needleAngle = speed / MAX_SPEED * MAX_SPEED_ANGLE;
        m_Needle.eulerAngles = new Vector3(0, 0, needleAngle);
    }
}

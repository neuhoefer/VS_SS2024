using UnityEngine;

public class SoapboxController : MonoBehaviour
{
    private float m_horizontalInput;
    private float m_vertikalInput;
    private bool m_isBraking;

    [SerializeField] private float m_motorForce;
    [SerializeField] private float m_breakForce;
    [SerializeField] private float m_maxSteeringAngle;

    [SerializeField] private WheelCollider m_FrontLeftCollider;
    [SerializeField] private WheelCollider m_FrontRightCollider;
    [SerializeField] private WheelCollider m_RearLeftCollider;
    [SerializeField] private WheelCollider m_RearRightCollider;

    [SerializeField] private Transform m_FrontLeftWheel;
    [SerializeField] private Transform m_FrontRightWheel;
    [SerializeField] private Transform m_RearLeftWheel;
    [SerializeField] private Transform m_RearRightWheel;

    private void FixedUpdate()
    {
        GetInput();
        ApplyForce();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_vertikalInput = Input.GetAxis("Vertical");
        m_isBraking = Input.GetKey(KeyCode.Space);
    }

    private void ApplyForce()
    {
        m_FrontLeftCollider.motorTorque = m_vertikalInput * m_motorForce;
        m_FrontRightCollider.motorTorque = m_vertikalInput * m_motorForce;

        float currentBrakeForce = m_isBraking ? m_breakForce : 0.0f;
        m_FrontLeftCollider.brakeTorque = currentBrakeForce;
        m_FrontRightCollider.brakeTorque = currentBrakeForce;
        m_RearLeftCollider.brakeTorque = currentBrakeForce;
        m_RearRightCollider.brakeTorque = currentBrakeForce;
    }

    private void HandleSteering()
    {
        m_FrontLeftCollider.steerAngle = m_horizontalInput * m_maxSteeringAngle;
        m_FrontRightCollider.steerAngle = m_horizontalInput * m_maxSteeringAngle;
    }

    private void UpdateWheels()
    {
        UpdateWheel(m_FrontLeftCollider, m_FrontLeftWheel);
        UpdateWheel(m_FrontRightCollider, m_FrontRightWheel);
        UpdateWheel(m_RearLeftCollider, m_RearLeftWheel);
        UpdateWheel(m_RearRightCollider, m_RearRightWheel);
    }

    private void UpdateWheel(WheelCollider collider, Transform wheel)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        wheel.position = pos;
        wheel.rotation = rot;

        //wheel.Rotate(Vector3.forward, 90, Space.Self);
    }
}


using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using VehiclePhysics.Specialized;

public class Automation : MonoBehaviour
{
    [SerializeField] private PIDController m_RotationController;
    [SerializeField] private PIDController m_DistanceController;
    [SerializeField] private float m_DistanceTolerance;
    
    [SerializeField] private GameObject m_Destination;
    [SerializeField] private NavMeshAgent m_Agent;
    [SerializeField] private VPHydraulicTrackedVehicleController m_Vehicle;

    private TextMeshProUGUI m_Label = null;
    private bool m_FollowingActive = false;

    void Start()
    {
        m_Label = GetComponentInChildren<TextMeshProUGUI>();
        gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    private void FixedUpdate()
    {
        if(m_FollowingActive)
        {
            var vehiclePosition = m_Vehicle.transform.position;
            var targetPosition = m_Agent.transform.position;
            targetPosition.y = vehiclePosition.y;
            var forwardDirection = m_Vehicle.transform.rotation * Vector3.forward;
            var targetDirection = (targetPosition - vehiclePosition).normalized;
            var currentAngle = Vector3.SignedAngle(Vector3.forward, forwardDirection, Vector3.up);
            var targetAngle = Vector3.SignedAngle(Vector3.forward, targetDirection, Vector3.up);
            var currentDistance = (targetPosition - vehiclePosition).magnitude;

            float rotationalInput = m_RotationController.UpdateAngle(Time.fixedDeltaTime, currentAngle, targetAngle);
            float translationalInput = m_DistanceController.Update(Time.fixedDeltaTime, currentDistance, 0.0f);

            if(currentDistance > m_DistanceTolerance)
            {
                m_Vehicle.leftTrackInput = rotationalInput + Mathf.Abs(translationalInput);
                m_Vehicle.rightTrackInput = -rotationalInput + Mathf.Abs(translationalInput);
            }
        }
    }

    private void TaskOnClick()
    {
        if(m_Label.color == Color.white)
        {
            m_Agent.transform.position = m_Vehicle.transform.position;
            m_Agent.SetDestination(m_Destination.transform.position);
            m_Label.color = new Color(255, 190, 0);
            m_FollowingActive = true;
        } 
        else if (m_FollowingActive)
        {
            m_Agent.SetDestination(m_Agent.transform.position);
            m_Label.color = Color.white;
            m_FollowingActive = false;
        }
    }

    public void SetAgentDestination (Vector3 pos)
    {
        m_Destination.SetActive(true);
        m_Destination.transform.position = pos;

        if(m_FollowingActive)
        {
            m_Agent.SetDestination(m_Destination.transform.position);
        }
        else
        {
            m_Label.color = Color.white;
        }
    }
}

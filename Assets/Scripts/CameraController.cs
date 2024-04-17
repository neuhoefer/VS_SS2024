using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject m_target;
    [SerializeField] private Vector3 m_offset;
    
    private void FixedUpdate()
    {
        transform.position = m_target.transform.TransformPoint(m_offset);
        var direction = m_target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }
}

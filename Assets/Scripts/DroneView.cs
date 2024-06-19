using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DroneView : MonoBehaviour
{
    [SerializeField] private Camera m_DroneCamera;
    [SerializeField] private MeshCollider m_Ground;
    [SerializeField] private Automation m_Automation;

    private RectTransform m_RectTransform = null;
    private RawImage m_RawImage = null;
    private Vector2 m_DroneViewMousePos = new Vector2();

    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_RawImage = GetComponent<RawImage>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Prüfen, ob linke Maustaste gedrückt wurde
        {
            // Cursorposition in Rectangle-Koordinaten erhalten (Rectangle-Format: 296x180)
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_RectTransform, Input.mousePosition, null, out m_DroneViewMousePos);

            //print("Rect coordinates: " + m_DroneViewMousePos.x + " || " + m_DroneViewMousePos.y);

            // Umrechnung in Viewport-Koordinaten (0,0) | (1,1) inkl. Berücksichtigung des Pivotpunkts
            m_DroneViewMousePos.x = (m_DroneViewMousePos.x / m_RectTransform.rect.width) + m_RectTransform.pivot.x;
            m_DroneViewMousePos.y = (m_DroneViewMousePos.y / m_RectTransform.rect.height) + m_RectTransform.pivot.y;

            m_DroneViewMousePos.x += m_RawImage.uvRect.x;       // Korrektur wegen Verschiebung nach links (UV Rect X = 0.06)
            m_DroneViewMousePos.x *= m_RawImage.uvRect.width;   // Korrektur wegen Stretching nach rechts (UV Rect W = 0.88)

            //print("Viewport coordinates: " + m_DroneViewMousePos.x + " || " + m_DroneViewMousePos.y);

            if (m_DroneViewMousePos.x > 0.0f && m_DroneViewMousePos.x < 1.0f && m_DroneViewMousePos.y > 0.0f && m_DroneViewMousePos.y < 1.0f)
            {
                // Erhalt eines Strahls von der Drone Camera ausgehend durch den Mousecursor (in Viewport-Koordinaten)
                Ray ray = m_DroneCamera.ViewportPointToRay(m_DroneViewMousePos);

                // Raycasting „Ray vs. Ground“ (2. Parameter mit Modifizierer „out“: Ergebnis, 3. Parameter: maximale Distanz)
                m_Ground.Raycast(ray, out RaycastHit raycastHit, 20.0f);

                // Setzen der Destination des Agent
                m_Automation.SetAgentDestination(raycastHit.point);
            }
        }
    }
}

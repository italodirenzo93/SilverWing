using UnityEngine;

public class Enemy01Movement : MonoBehaviour
{
    [SerializeField]
    private float m_speed;

    [SerializeField]
    private float m_waitTime;

    [SerializeField]
    private Transform[] m_movementNodes;

    private bool m_isMoving;
    private Vector3 m_currentPosition;
    private Vector3 m_targetPosition;
    private float m_startTime;
    private float m_journeyLength;
    private float m_elapsedWaitTime;

    private void Update()
    {
        if (!m_isMoving) // currently sitting at a node
        {
            if (m_elapsedWaitTime > m_waitTime)
            {
                // Reset timer
                m_elapsedWaitTime = 0f;

                // acquire target node
                m_currentPosition = transform.position;
                m_targetPosition = m_movementNodes[Random.Range(0, m_movementNodes.Length)].position;

                m_startTime = Time.time;
                m_journeyLength = Vector3.Distance(transform.position, m_targetPosition);
                m_isMoving = true;
            }
            else
            {
                // Increment the timer
                m_elapsedWaitTime += Time.deltaTime;
            }
        }
        else // move toward the target node
        {
            float distCovered = (Time.time - m_startTime) * m_speed;
            float fracJourney = distCovered / m_journeyLength;
            transform.position = Vector3.Lerp(m_currentPosition, m_targetPosition, fracJourney);
            if (transform.position == m_targetPosition)
            {
                // reached destination
                m_isMoving = false;
            }
        }
    }
}
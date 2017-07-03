using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Renderer m_graphics;
    private FlightPatternBehaviour m_pattern;

    private enum EnemyState { Waiting, Active, Destroy }

    private EnemyState m_state = EnemyState.Waiting;

    private void Start()
    {
        m_pattern = GetComponent<FlightPatternBehaviour>();
        m_pattern.enabled = false;
        //Debug.Log("Waiting");
    }

    private void Update()
    {
        switch (m_state)
        {
            default:
            case EnemyState.Waiting:
                StateWaiting();
                break;
            case EnemyState.Active:
                StateActive();
                break;
            case EnemyState.Destroy:
                StateDestroy();
                break;
        }
    }

    private void StateWaiting()
    {
        if (m_graphics.isVisible)
        {
#if UNITY_EDITOR
            if (Camera.current != null && Camera.current.name == "SceneCamera")
                return;
#endif
            //Debug.Log("Active");
            m_pattern.enabled = true;
            m_state = EnemyState.Active;
        }

    }

    private void StateActive()
    {
        if (!m_graphics.isVisible)
        {
            m_state = EnemyState.Destroy;
        }
    }

    private void StateDestroy()
    {
        //Debug.Log("Destroy");
        Destroy(gameObject);
    }
}

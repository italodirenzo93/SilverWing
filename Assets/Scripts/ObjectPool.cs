using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private int m_size;
    [SerializeField]
    private GameObject m_objectPrefab;
    [SerializeField]
    private Transform m_parent;

    private GameObject[] m_objects;

    public GameObject nextAvailable
    {
        get
        {
            for (int i = 0; i < m_size; i++)
            {
                if (!m_objects[i].activeInHierarchy)
                    return m_objects[i];
            }
            return null;
        }
    }

    private void Start()
    {
        m_objects = new GameObject[m_size];
        for (int i = 0; i < m_size; i++)
        {
            GameObject obj = Instantiate(m_objectPrefab, m_parent);
            obj.SetActive(false);
            m_objects[i] = obj;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < m_size; i++)
        {
            Destroy(m_objects[i]);
        }
    }
}
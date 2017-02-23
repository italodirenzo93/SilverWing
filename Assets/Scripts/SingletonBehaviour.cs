using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_instance;

    public static T instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<T>();

                if (m_instance == null)
                {
                    Debug.LogError("An instance of type '" + typeof(T).Name + "' is needed in the scene, but there is none.");
                }
            }
            return m_instance;
        }
    }
}
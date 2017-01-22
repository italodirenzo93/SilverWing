using UnityEngine;

public class LaserController : MonoBehaviour
{
    public float Speed = 15f;

    [SerializeField]
    private Renderer m_graphics;

    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * Speed;

        if (!m_graphics.isVisible)
        {
            gameObject.SetActive(false);
        }
    }
}

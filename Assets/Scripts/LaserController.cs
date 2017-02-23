using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LaserController : MonoBehaviour
{
    public float Speed = 15f;

    [SerializeField]
    private Renderer m_graphics;

    private Rigidbody2D m_rbody;

    private void Start()
    {
        m_rbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!m_graphics.isVisible)
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        m_rbody.velocity = transform.right * Speed;
    }
}

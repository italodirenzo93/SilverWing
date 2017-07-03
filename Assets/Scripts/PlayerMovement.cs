using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    #region Public Fields
    public float MovementSpeed = 15f;
    public float EvasionAnimationSpeed = 200f;
    public float EvasionSpeedBoost = 20f;
    #endregion

    #region Serialized Fields
    [SerializeField]
    private GameObject m_graphicsObject;
    [SerializeField]
    private Camera m_mainCamera;
    [SerializeField]
    private Vector2 m_viewportBoundsPadding;
    [SerializeField]
    private ParticleSystem[] m_auxThrusters;
    #endregion

    #region Private Fields
    private Rigidbody2D m_rigidbody;
    private Coroutine m_evadeRoutineInstance;
    private float m_baseMovementSpeed;  // save a reference to the initial speed
    #endregion

    #region MonoBehaviour Events
    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_baseMovementSpeed = MovementSpeed;

        // Parent to the main camera
        transform.parent = m_mainCamera.transform;

        // Auxillery thrusters start disabled
        ActivateAuxThrusters(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown(InputMappings.EVADE))
        {
            if (m_evadeRoutineInstance != null)
            {
                StopCoroutine(m_evadeRoutineInstance);
                if (MovementSpeed > m_baseMovementSpeed)
                    MovementSpeed = m_baseMovementSpeed;     // cancel the speed boost
            }
            m_evadeRoutineInstance = StartCoroutine(Evade());
        }
    }

    private void FixedUpdate()
    {
        Vector3 xVel = transform.right * Input.GetAxis(InputMappings.HORIZONTAL_AXIS);
        Vector3 yVel = transform.up * Input.GetAxis(InputMappings.VERTICAL_AXIS);

        var velocity = (xVel + yVel) * MovementSpeed;
        if (velocity != Vector3.zero)
        {
            var newPosition = ClampToViewport(m_rigidbody.position + (new Vector2(velocity.x, velocity.y) * Time.fixedDeltaTime));
            m_rigidbody.MovePosition(newPosition);
        }
    }
    #endregion

    #region Private Methods
    private Vector2 ClampToViewport(Vector2 position)
    {
        // Calculate the viewport bounds in world space
        var bottomLeftWorldCoordinates = Camera.main.ViewportToWorldPoint(Vector3.zero);
        var topRightWorldCoordinates = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));

        // Modify the extends to account for the extents of the graphics
        var bounds = new Vector3(m_viewportBoundsPadding.x, m_viewportBoundsPadding.y);
        var minMoveDistance = bottomLeftWorldCoordinates + bounds;
        var maxMoveDistance = topRightWorldCoordinates - bounds;

        // Return the reclaculated position
        return new Vector2(
            Mathf.Clamp(position.x, minMoveDistance.x, maxMoveDistance.x),
            Mathf.Clamp(position.y, minMoveDistance.y, maxMoveDistance.y));
    }

    private IEnumerator Evade()
    {
        // Reset the rotation
        m_graphicsObject.transform.rotation = Quaternion.Euler(Vector3.zero);

        // Apply speed boost
        MovementSpeed += EvasionSpeedBoost;

        // Enable auxillery thrusters
        ActivateAuxThrusters(true);

        // 360 degree rotation
        float rotationAmount = 0f;
        while (rotationAmount < 360f)
        {
            rotationAmount += EvasionAnimationSpeed * Time.deltaTime;
            m_graphicsObject.transform.rotation = Quaternion.Euler(rotationAmount, 0, 0);
            yield return null;
        }

        // Disable auxillery thrusters
        ActivateAuxThrusters(false);

        // Back to normal speed
        MovementSpeed -= EvasionSpeedBoost;

        // Clear the running instance
        m_evadeRoutineInstance = null;
    }

    private void ActivateAuxThrusters(bool on)
    {
        if (on)
        {
            for (int i = 0; i < m_auxThrusters.Length; i++)
                m_auxThrusters[i].Play();
        }
        else
        {
            for (int i = 0; i < m_auxThrusters.Length; i++)
                m_auxThrusters[i].Stop();
        }
    }
    #endregion
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SineWavePattern : FlightPatternBehaviour
{
    public float verticalVelocity;
    public float horizontalVelocity;

    [SerializeField]
    public float maximumWavelength;

    private Rigidbody2D m_rigidBody;

    private void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }
    
	private void FixedUpdate()
    {
        float xPos = m_rigidBody.position.x - (horizontalVelocity * Time.fixedDeltaTime);
        float yPos = Mathf.PingPong(verticalVelocity * Time.fixedTime, maximumWavelength) - (maximumWavelength / 2);
        m_rigidBody.MovePosition(new Vector2(xPos, yPos));
    }
}

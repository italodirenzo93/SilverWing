using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 55f;

    private enum Direction { Left, Right }
    [SerializeField]
    private Direction direction;

    private void Update()
    {
        if (direction == Direction.Left)
            transform.position = transform.position + Vector3.right * (speed * Time.deltaTime);
        else
            transform.position = transform.position - Vector3.right * (speed * Time.deltaTime);
    }
}
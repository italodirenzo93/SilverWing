using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class BasicLaserWeapon : MonoBehaviour, IWeapon
{
    [SerializeField]
    private float m_fireRate;

    private ObjectPool m_pool;
    private float m_nextFire;

    private void Start()
    {
        m_pool = GetComponent<ObjectPool>();
    }

    public void Fire()
    {
        // Delay each shot so that it isn't just a stream of lasers
        if (Time.time > m_nextFire)
        {
            m_nextFire = Time.time + m_fireRate;

            // Launch the laser
            GameObject projectile = m_pool.nextAvailable;
            if (projectile != null)
            {
                projectile.transform.position = transform.position;
                projectile.transform.rotation = transform.rotation;
                projectile.SetActive(true);
            }
        }
    }
}
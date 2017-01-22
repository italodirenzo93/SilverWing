using UnityEngine;

public class Enemy01Shoot : MonoBehaviour
{
    [SerializeField]
    private Transform m_weaponFirePoint;

    [SerializeField]
    [Range(0f, 1f)]
    private float m_shotChance;

    private IWeapon m_weaponController;

    private void Start()
    {
        m_weaponController = WeaponManager.instance.SpawnWeapon<BasicLaserWeapon>(m_weaponFirePoint);

        if (m_weaponController == null)
            Debug.LogError(GetType().Name + ": could not find component of type BasicLaserWeapon.");
    }

    private void Update()
    {
        if (Random.Range(0f, 1f) < m_shotChance)
        {
            m_weaponController.Fire();
        }
    }
}

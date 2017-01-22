using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private Transform m_weaponFirePoint;

    private IWeapon m_weaponController;

    private void Start()
    {
        m_weaponController = WeaponManager.instance.SpawnWeapon<BasicLaserWeapon>(m_weaponFirePoint);

        if (m_weaponController == null)
            Debug.LogError(GetType().Name + ": could not find component of type BasicLaserWeapon.");
    }

    private void Update()
    {
        if (Input.GetButton(InputMappings.FIRE))
        {
            m_weaponController.Fire();
        }
    }
}
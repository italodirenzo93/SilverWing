using UnityEngine;

public class WeaponManager : SingletonBehaviour<WeaponManager>
{
    [SerializeField]
    private GameObject[] m_weaponPrefabs;

    public T SpawnWeapon<T>(Transform parent) where T : IWeapon
    {
        for (int i = 0; i < m_weaponPrefabs.Length; i++)
        {
            if (m_weaponPrefabs[i].GetComponent<T>() != null)
            {
                GameObject weaponObject = Instantiate(m_weaponPrefabs[i], parent.position, parent.rotation, parent);
                weaponObject.name = m_weaponPrefabs[i].name;
                return weaponObject.GetComponent<T>();
            }
        }
        return default(T);
    }
}
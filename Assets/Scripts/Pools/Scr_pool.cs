using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_pool : MonoBehaviour
{
    
    public int m_poolAmount = 20;
    public GameObject m_objectToPool;
    public GameObject[] m_objectPool;
    [Header("Drop settings")]
    public Vector3 m_checkSize;
    public PlayerItem m_itemType;
    public bool m_destroyOnCollision;
    public bool m_hoverObjectsToPlayer;

    // Start is called before the first frame update
    void Awake()
    {
        m_objectPool = new GameObject[m_poolAmount];
        Scr_player_controller player = FindObjectOfType<Scr_player_controller>();
        for (int i = 0; i < m_poolAmount; i++)
        {
            
            GameObject obj = Instantiate(m_objectToPool,transform);
            Scr_collision_checkbox objCheckBox = obj.AddComponent<Scr_collision_checkbox>();
            objCheckBox.m_checkLayer = 1 << LayerMask.NameToLayer("Player");
            objCheckBox.m_boxSize = m_checkSize;
            Scr_pool_object objCode = obj.AddComponent<Scr_pool_object>();
            objCode.m_item = m_itemType;
            objCode.m_destroyOnCollision = m_destroyOnCollision;
            objCode.m_pool = this;
            objCode.m_player = player;
            objCode.m_hoverToPlayer = m_hoverObjectsToPlayer;
            obj.SetActive(false);

            m_objectPool[i] = obj;

        }
    }

    private GameObject ActivateObject(Vector3 position, Transform parent)
    {
        foreach (var obj in m_objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                obj.transform.parent = parent;
                obj.transform.position = position;
                return obj;
            }
        }
        Debug.LogWarning("Object not found in pool!");
        return null;
    }

    #region SpawnObject()
    public GameObject SpawnObject()
    {
        return ActivateObject(Vector3.zero, null);
    }
    public GameObject SpawnObject(Vector3 position)
    {
        return ActivateObject(position, null);
    }
    public GameObject SpawnObject(Transform parent)
    {
        return ActivateObject(Vector3.zero, parent);
    }
    public GameObject SpawnObject(Vector3 position, Transform parent)
    {
        return ActivateObject(position, parent);
    }
    #endregion

}

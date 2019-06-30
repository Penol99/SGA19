using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_pool_money : MonoBehaviour
{
    private Scr_pool m_pool;

    // Start is called before the first frame update
    void Start()
    {
        m_pool = GetComponent<Scr_pool>();
    }

    public void DropMoney(GameObject objectThatDrops, int randMin, int randMax)
    {
        int randomNumber = Random.Range(randMin, randMax);

        for (int i = 0; i < randomNumber; i++)
        {
            // Spawn Money
            float randThreshold = 2;
            float randX = Random.Range(-randThreshold, randThreshold) + objectThatDrops.transform.position.x;
            float randY = Random.Range(-randThreshold, randThreshold) + objectThatDrops.transform.position.y;
            float randZ = Random.Range(-randThreshold, randThreshold) + objectThatDrops.transform.position.z;
            Vector3 randomPos = new Vector3(randX,randY,randZ);
            GameObject money = m_pool.SpawnObject(randomPos);

        }
    }

}

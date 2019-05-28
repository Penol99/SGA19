using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scr_global_lists : MonoBehaviour
{
    public LayerMask m_enemyLayer;

    public static List<GameObject> WorldObjects = new List<GameObject>();
    public static List<Transform> InteractList = new List<Transform>();
    public static List<Animator> AnimatorList = new List<Animator>();
    public static List<GameObject> EnemyList = new List<GameObject>();  

    // NEEDS TO BE AWAKE SO THAT OBJECTS THAT ARE GOING TO BE ADDED TO INTERACT LIST IS ADDED IN START, OR ELSE IT WILL BE CLEARED
    private void Awake()
    {
        EnemyList.Clear();
        WorldObjects.Clear();
        InteractList.Clear();
        AnimatorList.Clear();
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        for (int i = 0; i < allObjects.Length; i++)
        {
            
            WorldObjects.Add(allObjects[i]);
            if (allObjects[i].GetComponent<Animator>() != null)
                AnimatorList.Add(allObjects[i].GetComponent<Animator>());
        }
        EnemyList = LayerMaskToList(m_enemyLayer);

    }


    public static void RemoveFromWorldObjectList(GameObject obj)
    {
        WorldObjects.Remove(obj);
        WorldObjects.TrimExcess();

    }

    public static List<GameObject> LayerMaskToList(LayerMask mask)
    {
        List<GameObject> objList = new List<GameObject>();
        for (int i = 0; i < WorldObjects.Count; i++)
        {
            
            if (mask == (mask | (1 << WorldObjects[i].layer)))
            {
                objList.Add(WorldObjects[i]);
            }
        }
        return objList;
    }




}

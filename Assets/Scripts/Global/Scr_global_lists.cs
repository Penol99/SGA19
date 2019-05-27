using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_global_lists : MonoBehaviour
{
    public static List<GameObject> WorldObjects = new List<GameObject>();
    public static List<Transform> InteractList = new List<Transform>();
    public static List<Animator> AnimatorList = new List<Animator>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void OnStart()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        for (int i = 0; i < allObjects.Length; i++)
        {
            WorldObjects.Add(allObjects[i]);
            if (allObjects[i].GetComponent<Animator>() != null)
                AnimatorList.Add(allObjects[i].GetComponent<Animator>());
        }
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

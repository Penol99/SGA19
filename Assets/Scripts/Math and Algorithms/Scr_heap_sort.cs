using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_heap_sort : MonoBehaviour
{
    private int heapSize;

    public void HeapSortFloat(List<float> list)
    {
        BuildFloatHeap(list);
        for(int i = list.Count-1;i >= 0; i--)
        {
            SwapFloat(list, 0, i);
            heapSize--;
            HeapifyFloat(list, 0);
        }

    }
    public void HeapSortFloat(List<float> list, List<GameObject> objList)
    {
        BuildFloatHeap(list,objList);
        for (int i = list.Count - 1; i >= 0; i--)
        {
            SwapFloat(list,objList, 0, i);
            heapSize--;
            HeapifyFloat(list,objList, 0);
        }

    }

    private void BuildFloatHeap(List<float> list)
    {
        heapSize = list.Count;
        for (int i = heapSize / 2; i >= 0;i--)
        {
            HeapifyFloat(list, i);
        }
    }
    private void BuildFloatHeap(List<float> list,  List<GameObject> objList)
    {
        heapSize = list.Count;
        for (int i = heapSize / 2; i >= 0; i--)
        {
            HeapifyFloat(list, objList, i);
        }
    }

    private void HeapifyFloat(List<float> list, int index)
    {
        int left = 2 * index;
        int right = 2 * index + 1;
        int largest = index;

        if (left < heapSize && list[left] > list[largest])
            largest = left;
        if (right < heapSize && list[right] > list[largest])
            largest = right;

        if (index != largest)
        {
            SwapFloat(list, largest, index);
            HeapifyFloat(list, largest);
        }
    }
    private void HeapifyFloat(List<float> list, List<GameObject> objList, int index)
    {
        int left = 2 * index;
        int right = 2 * index + 1;
        int largest = index;

        if (left < heapSize && list[left] > list[largest])
            largest = left;
        if (right < heapSize && list[right] > list[largest])
            largest = right;

        if (index != largest)
        {
            SwapFloat(list, objList, largest, index);
            HeapifyFloat(list, objList, largest);
        }
    }


    private void SwapFloat(List<float> list, int x, int y)
    {
        float temp = list[x];
        list[x] = list[y];
        list[y] = temp;
    }
    private void SwapFloat(List<float> list, List<GameObject> objList, int x, int y)
    {
        float temp = list[x];
        GameObject tempObj = objList[x];
        list[x] = list[y];
        list[y] = temp;
        objList[x] = objList[y];
        objList[y] = tempObj;
    }
}

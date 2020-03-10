using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMangaer : MonoBehaviour
{
    public Transform target;

    public int step = 15;
    public float currentDistance = 0;
    public int indexer = 0;

    public List<GameObject> listBlocks = new List<GameObject>();
    public List<GameObject> listBuffer = new List<GameObject>();

    public void CoreUpdate()
    {
        LevelPartControl();
    }

    private void LevelPartControl()
    {
        currentDistance = target.localPosition.z;

        if (currentDistance >= step * indexer)
        {
            indexer++;

            CreatePart(step * indexer);
        }

        if(listBuffer.Count > 2)
        {
            Destroy(listBuffer[0]);
            listBuffer.RemoveAt(0);
        }
    }

    private void CreatePart(float dist)
    {
        Vector3 pos = Vector3.zero;
        pos.z = dist;
        pos.y = -5;
        GameObject newBlock = Instantiate(listBlocks[Random.Range(0, listBlocks.Count)], pos, Quaternion.identity);

        listBuffer.Add(newBlock);
    }

    public void ResetManager()
    {
        foreach(GameObject box in listBuffer)
        {
            Destroy(box);
        }

        listBuffer.Clear();
        currentDistance = 0;
        indexer = 0;
    }
}

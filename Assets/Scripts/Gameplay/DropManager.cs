using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{

    // drops -> chances
    // hopefully I'll found out how to get rid of this shit
    public GameObject[] drops;
    public float[] chances;

    public GameObject GetObjectToDrop()
    {
        float rnd = Random.value;
        GameObject prefab = null;
        for(int i = 0; i < drops.Length; i++)
        {
            if(chances[i] >= rnd)
            {
                prefab = drops[i];
            }
        }
        Debug.LogWarning("Chance = " + rnd + " obj to spawn = " + prefab);
        return prefab;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnRate = 2f;
    public float WaveSpawnHeight = -6f;
    public float WaveRandomRange = 4;
    [Range(-5f,5f)]
    public float XXSPPEED = 0f;

    private float time = 0;
    private ObjectPool objectPool;
    private int chileCount;
    void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
        chileCount = objectPool.gameObject.transform.childCount;
    }
    void Update()
    {
        
        if(Time.time - time > 1f/spawnRate)
        {
            if (Random.Range(0, 10) > 6)
            {
                SpawnOctpus();
            }
            SpawnWave();
            time = Time.time;
        }
        for(int i = 0; i < chileCount; i++)
        {
            objectPool.transform.GetChild(i).GetComponent<Wave>().XSpeed = XXSPPEED;
        }
    }
    void SpawnWave()
    {
        Vector3 position = new Vector3(Random.Range(-WaveRandomRange, WaveRandomRange), 0, 0);
        objectPool.Reuse(ObjectPool.WhichObject.wave, position + new Vector3(0, WaveSpawnHeight, 0));
    }
    void SpawnOctpus()
    {
        Vector3 position = new Vector3(Random.Range(-WaveRandomRange, WaveRandomRange), 0, 0);
        objectPool.Reuse(ObjectPool.WhichObject.octpus, position + new Vector3(0, WaveSpawnHeight, 0));
    }
}

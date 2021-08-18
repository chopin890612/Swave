using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public GameObject Wave;
    public int WaveLimit = 20;
    public GameObject Octpus;
    public int OctpusLimit = 20;

    public Queue<GameObject> Qwave = new Queue<GameObject>();
    public Queue<GameObject> Qoctpus = new Queue<GameObject>();

    public enum WhichObject
    {
        wave,
        octpus
    }

    private void Awake()
    {
        for(int i = 0; i < WaveLimit; i++)
        {
            var go = Instantiate(Wave,transform);
            Qwave.Enqueue(go);
            go.SetActive(false);
        }
        for (int i = 0; i < OctpusLimit; i++)
        {
            var go = Instantiate(Octpus,transform);
            Qoctpus.Enqueue(go);
            go.SetActive(false);
        }
    }
    public void Reuse(WhichObject type, Vector3 position)
    {
        if(type == WhichObject.wave && Qwave.Count > 0)
        {
            GameObject reuse = Qwave.Dequeue();
            reuse.transform.position = position;
            reuse.SetActive(true);
        }
        if(type == WhichObject.octpus && Qoctpus.Count > 0)
        {
            GameObject reuse = Qoctpus.Dequeue();
            reuse.transform.position = position;
            reuse.SetActive(true);
        }
    }
    public void Enduse(GameObject gameObject)
    {
        if(gameObject.CompareTag("Wave"))
        {
            Qwave.Enqueue(gameObject);
            gameObject.SetActive(false);
        }
        if(gameObject.CompareTag("Octpus"))
        {
            Qoctpus.Enqueue(gameObject);
            gameObject.SetActive(false);
        }
    }

}

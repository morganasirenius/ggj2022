using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObjectSpawner : MonoBehaviour
{

    public GameObject pooledObject;
    public int pooledAmount;

    private GameObject poolContainer;
    List<GameObject> pooledObjects = new List<GameObject>();
    IEnumerator ReleaseTheBeast(int delay, GameObject obj)
    {
        yield return new WaitForSeconds(delay);
        obj.transform.position = new Vector2(0, 8f);
        obj.SetActive(true);

    }
    // Start is called before the first frame update
    void Start()
    {
        poolContainer = new GameObject();
        poolContainer.name = pooledObject.name + " Container";
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.transform.parent = poolContainer.transform;
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
        foreach (GameObject obj in pooledObjects)
        {
            IEnumerator coroutine = ReleaseTheBeast(Random.Range(0, 2), obj);
            StartCoroutine(coroutine);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

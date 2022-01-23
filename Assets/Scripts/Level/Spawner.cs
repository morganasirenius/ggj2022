using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Transform StartPoint;
    public Transform EndPoint;

    public void SpawnAt(GameObject obj, Vector3 pos)
    {
        obj.SetActive(true);
        obj.transform.position = pos;
    }

    public Vector3 GetRandomSpawnPoint()
    {
        float x = Random.Range(StartPoint.transform.position.x, EndPoint.transform.position.x);
        float y = Random.Range(StartPoint.transform.position.y, EndPoint.transform.position.y);

        return new Vector3(x, y, StartPoint.transform.position.z); // z shouldn't matter since they are the same
    }

    public Vector3 GetMiddleSpawnPoint()
    {
        float x = (StartPoint.transform.position.x + EndPoint.transform.position.x) / 2;
        float y = (StartPoint.transform.position.y + EndPoint.transform.position.y) / 2;
        return new Vector3(x, y, StartPoint.transform.position.z); // z shouldn't matter since they are the same
    }
}

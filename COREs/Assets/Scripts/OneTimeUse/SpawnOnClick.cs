using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class SpawnOnClick : MonoBehaviour
{
    [SerializeField]
    [Required]
    GameObjectPool pool = null;
    List<GameObject> createdObjects = new List<GameObject>();
    [SerializeField]
    float recollectInterval;
    [SerializeField]
    [ReadOnly]
    float curTime = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            createdObjects.Add(pool.RequestInstance());
        }
        curTime += Time.deltaTime;
        if (curTime >= recollectInterval)
        {
            if (createdObjects.Count > 0)
            {
                var randomIndex = Random.Range(0, createdObjects.Count);
                pool.ReturnInstance(createdObjects[randomIndex]);
                createdObjects.Remove(createdObjects[randomIndex]);
            }
            curTime = 0;
        }
    }
}

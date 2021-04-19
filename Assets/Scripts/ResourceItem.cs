using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceItem : MonoBehaviour
{
    private ResourceSpawner rs;
    public int health;
    public enum ResourceTypes {Wood, Stone, Gold, Diamond };
    public ResourceTypes resourceType = ResourceTypes.Wood;
    // Start is called before the first frame update
    void Start()
    {
        rs = FindObjectOfType<ResourceSpawner>();
        health = Random.Range(4,10);
    }

    private void OnDestroy()
    {
        rs.StartCoroutine("RespawnDelay");
    }
}

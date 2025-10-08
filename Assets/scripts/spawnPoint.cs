using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPoint : MonoBehaviour
{
    public GameObject monster;

    void Start()
    {
        Instantiate(monster, transform.position, Quaternion.identity);
    }
}

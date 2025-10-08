using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objCleaner : MonoBehaviour
{
    public float objTime;
    void Start()
    {
        Destroy(gameObject, objTime);
    }
}

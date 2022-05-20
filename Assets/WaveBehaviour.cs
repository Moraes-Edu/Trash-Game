using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBehaviour : MonoBehaviour
{
    [SerializeField]
    public static int quantSpawn = 30;
    public static int contSpawn;
    public static int index = 0;
    void Start()
    {
        contSpawn = quantSpawn;
    }
    void Update()
    {
        if (contSpawn == 0)
            index++;
    }
}

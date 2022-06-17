using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretData
{
    [System.Serializable]
    public struct rowData 
    {
        public GameObject turret;
        public GameObject preview;
    }
    public rowData[] turrets;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct TowerInfo
{
    public GameObject turret;
    public GameObject preview;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    // public int GetSellAmount ()
	// {
	// 	return cost / 2;
	// }
}
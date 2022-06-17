using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

public class Terreno : MonoBehaviour
{
    [SerializeField]    private float minRadius;
    [SerializeField]    LayerMask layersToCollide;
    [SerializeField]    LayerMask layersTerrain;
    [SerializeField]    Text text;
    [SerializeField]    Material[] materials;
    public GameObject turretToBuild;
    public bool active;
    GameObject go = null;
    Vector3 pos;
    int towerCount;

    private void Update()
    {
        if (!active)
            return;
        
        (go = go != null ? go : Instantiate(turretToBuild,pos,Quaternion.identity)).transform.position = pos;

        if (Physics.OverlapSphere(pos, minRadius, layersToCollide).Length > 1)
        {
            go.GetComponent<MeshRenderer>().material = materials[0];
            return;
        } else
        {
            go.GetComponent<MeshRenderer>().material = materials[1];
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject turret = (GameObject)Instantiate(turretToBuild, pos, transform.rotation);
            Increase();
            Destruct();
            pos = Vector3.zero;
        }
    }
    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit HitInfo, Mathf.Infinity, Physics.AllLayers))
            return;
        if (!HitInfo.collider.CompareTag("Terrain"))
            return;
        pos = HitInfo.point;
    }
    private void Destruct()
    {
        Destroy(go);
        go = null;
        active = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        if (go != null)
        Gizmos.DrawWireSphere(go.transform.position, minRadius);
    }
    public void Change(GameObject goo)
    {
        turretToBuild = goo;
        active = true;
    }
    private void Increase()
    {
        towerCount++;
        text.text = $"{towerCount} torres";
    }
    private void Decrease()
    {
        towerCount--;
        text.text = $"{towerCount} torres";
    }


}

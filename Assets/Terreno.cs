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
    [SerializeField] LayerMask turretsLayer;
    [SerializeField]    Text text;
    [SerializeField]    Material[] materials;
    [Header("Torres")]
    [SerializeField]
    TurretData turretData;
    GameObject turretToBuild;
    GameObject turretPreview;
    bool active;
    GameObject go = null;
    Vector3 pos;
    int towerCount;
    bool removeTurret;
    Ray ray;
    private void Update()
    {
        if (removeTurret && Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray, out RaycastHit HitInfo, Mathf.Infinity, turretsLayer))
            {
                Destroy(HitInfo.collider.gameObject);
                Decrease();
                removeTurret = false;
            }
        }
        if (!active)
            return;
        
        (go = go != null ? go : Instantiate(turretPreview,pos,Quaternion.identity)).transform.position = pos;

        if (Physics.OverlapSphere(pos, minRadius, layersToCollide).Length > 0)
        {
            go.GetComponent<MeshRenderer>().material = materials[0];
            return;
        } else
        {
            go.GetComponent<MeshRenderer>().material = materials[1];
        }

        if (Input.GetMouseButtonDown(0) && pos != Vector3.zero)
        {
            GameObject turret = (GameObject)Instantiate(turretToBuild, pos, transform.rotation);
            Increase();
            Destruct();
            pos = Vector3.zero;
        }
    }
    private void FixedUpdate()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit HitInfo, Mathf.Infinity, layersTerrain))
        {
            pos = Vector3.zero;
            go?.SetActive(false);
            return;
        } else
        {
            go?.SetActive(true);
        }
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
    public void Change(int index)
    {
        turretPreview = turretData.turrets[index].preview;
        turretToBuild = turretData.turrets[index].turret;
        active = true;
    }
    public void Remove()
    {
        removeTurret = true;
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

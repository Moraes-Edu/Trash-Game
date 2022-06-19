using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

public class TurretPlacement : MonoBehaviour
{
    [SerializeField] private int maxTowers;
    [SerializeField] private float minRadius;
    [SerializeField] LayerMask layersToCollide;
    [SerializeField] LayerMask layersTerrain;
    [SerializeField] LayerMask turretsLayer;
    [SerializeField] Text text;
    [SerializeField] Material[] materials;
    [SerializeField] Terrain terrain;
    [Header("Torres")]
    [SerializeField]
    TowerInfo[] turretData;
    bool active;
    GameObject go = null;
    Vector3 pos;
    int towerCount;
    bool removeTurret;
    Ray ray;
    int turretIndexer = -1;

    private void Update()
    {
        if (removeTurret && Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit HitInfo, Mathf.Infinity, turretsLayer))
            {
                Currency.Increase(HitInfo.collider.gameObject.GetComponent<Turret>().Data.cost / 2);
                Destroy(HitInfo.collider.gameObject);
                Decrease();
            }
            removeTurret = false;
        }
        if (!active || maxTowers <= towerCount)
            return;

        (go = go != null ? go : Instantiate(turretData[turretIndexer].preview, pos, Quaternion.identity)).transform.position = pos;

        if (Physics.OverlapSphere(pos, minRadius, layersToCollide).Length > 0 ||(pos != Vector3.zero && TextureTest(pos,terrain) == "gravel"))
        {
            go.GetComponent<MeshRenderer>().material = materials[0];
            return;
        }
        else
        {
            go.GetComponent<MeshRenderer>().material = materials[1];
        }

        if (Input.GetMouseButtonDown(0) && pos != Vector3.zero)
        {
            if (!Currency.Decrease(turretData[turretIndexer].cost))
            {
                Destruct();
                return;
            }
            GameObject turret = (GameObject)Instantiate(turretData[turretIndexer].turret, pos, transform.rotation);
            turret.GetComponent<Turret>().Data = turretData[turretIndexer];
            Increase();
            Destruct();
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
        }
        else
        {
            go?.SetActive(true);
        }
        if (!HitInfo.collider.CompareTag("Terrain"))
            return;
        pos = HitInfo.point;
    }
    string TextureTest(Vector3 turretPos,Terrain t)
    {
        float[] cellMix = GetTextureMix(turretPos, t);
        float strongest = 0;
        int maxIndex = 0;
        for(int i = 0; i < cellMix.Length; i++)
        {
            if(cellMix[i] > strongest)
            {
                maxIndex = i;
                strongest = cellMix[i];
            }
        }
        Debug.Log(t.terrainData.terrainLayers[maxIndex].name);
        return t.terrainData.terrainLayers[maxIndex].name;
    }
    float[] GetTextureMix(Vector3 turretPos,Terrain t)
    {
        Vector3 tPos = t.transform.position;
        TerrainData tData = t.terrainData;

        int mapX = Mathf.RoundToInt((turretPos.x - tPos.x) / tData.size.x * tData.alphamapWidth);
        int mapZ = Mathf.RoundToInt((turretPos.z - tPos.z) / tData.size.z * tData.alphamapHeight);
        float[,,] splatMapData = tData.GetAlphamaps(mapX, mapZ, 1, 1);

        float[] cellmix = new float[splatMapData.GetUpperBound(2)+1];
        for(int i = 0; i < cellmix.Length; i++)
        {
            cellmix[i] = splatMapData[0,0,i];
        }
        return cellmix;
    } 
    private void Destruct()
    {
        pos = Vector3.zero;
        Destroy(go);
        go = null;
        active = false;
        turretIndexer = -1;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        if (go != null)
            Gizmos.DrawWireSphere(go.transform.position, minRadius);
    }
    public void Change(int index)
    {
        if (turretIndexer == index)
        {
            active = false;
            return;
        }
        turretIndexer = index;
        active = true;
    }
    public void Remove()
    {
        removeTurret = !removeTurret;
        Destruct();
    }
    private void Increase()
    {
        towerCount++;
        text.text = $"towers: {towerCount}";
    }
    private void Decrease()
    {
        towerCount--;
        text.text = $"towers: {towerCount}";
    }
}

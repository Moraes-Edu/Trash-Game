using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IEnemy
{
    void ChangeStats(int nWaves, int adtional);
    void TakeDamage(int dmg);
    void SetPath(Transform[] path);
}

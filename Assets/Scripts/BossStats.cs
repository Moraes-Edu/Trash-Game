using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : EnemyBehaviour, IEnemy
{
    public void ChangeStats(int nWave, int adtional)
    {
        this.life = initialLife*(2 * (nWave+adtional));
    }
}

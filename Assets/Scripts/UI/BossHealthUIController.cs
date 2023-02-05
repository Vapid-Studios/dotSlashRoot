using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUIController : MonoBehaviour
{
    public EnemyStats bossStats;
    public Image BossHealthImage;
    
    public void OnBossDamaged()
    {
        BossHealthImage.fillAmount = bossStats.maxHealth / bossStats.CurrentHealth;
    }
}

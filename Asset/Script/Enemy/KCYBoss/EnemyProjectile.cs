using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private RangeBoss rangeBoss;

    private void Awake()
    {
        rangeBoss = GetComponentInParent<RangeBoss>();
    }

    private void OnEffect()
    {
        rangeBoss?.CreateEffect();
    }

    private void OnProjectile()
    {
        rangeBoss?.CreateProjectile();
    }
}
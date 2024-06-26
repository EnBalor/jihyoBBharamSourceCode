using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    SpriteRenderer rend;
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }
    public void yFlip()
    {
        rend.flipX = true;
    }

    public void nFlip()
    {
        rend.flipX = false;
    }
}

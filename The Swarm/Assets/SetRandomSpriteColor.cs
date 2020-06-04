using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRandomSpriteColor : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer.color = Color.HSVToRGB(Random.Range(0f, 1f), 0.9f, 0.9f);
    }
}

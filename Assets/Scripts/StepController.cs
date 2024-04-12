using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StepController : MonoBehaviour
{
    private SpriteRenderer spriteColor;
    [SerializeField] private Color spriteNewColor;
    private void Start()
    {
        spriteColor = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            spriteColor.color = spriteNewColor;
            Destroy(gameObject, 10f);
        }
    }
}

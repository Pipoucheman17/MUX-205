using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ciel : MonoBehaviour
{
    public Collider2D collider;
    public LoadSpecificScene loader;

    public void Start()
    {
        collider = GetComponent<Collider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collider.CompareTag("Player"))
        {
            loader.loadNextScene();
        }
    }
}

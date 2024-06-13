using UnityEngine;
using System.Collections;
public class DeathZone : MonoBehaviour
{
    private Transform playerSpawn;
    private Animator fadeSystem;
    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            StartCoroutine(ReplacePlayer(collider));
        }
    }

    private IEnumerator ReplacePlayer(Collider2D collider)
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(0.5f);
        collider.transform.position = playerSpawn.position;
        
    }
}

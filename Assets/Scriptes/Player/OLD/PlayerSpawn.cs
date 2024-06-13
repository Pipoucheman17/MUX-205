using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] public GameObject player;
    void Start()
    {
        Instantiate(player,transform.position,transform.rotation);
    }
}

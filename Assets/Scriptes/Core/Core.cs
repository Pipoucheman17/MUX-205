using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement { get; private set; }
    public CollisionSenses CollisionSenses { get; private set; }
    public Attack Attack { get; private set; }
    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
        Attack = GetComponentInChildren<Attack>();
        if(!Movement || !CollisionSenses || !Attack)
        {
            Debug.LogError("Missing Movement Component");
        }
       
    }

    public void LogicUpdate()
    {
        Movement.LogicUpdate(); 
    }
}

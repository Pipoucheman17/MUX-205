using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : CoreComponent
{
    public Transform Canon { get => canon; private set => canon = value; }

    [SerializeField] private Transform canon;
}

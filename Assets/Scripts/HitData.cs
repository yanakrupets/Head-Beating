using System;
using UnityEngine;

[Serializable]
public struct HitData
{
    [field: SerializeField] public HitType HitType { get; private set; }
    [field: SerializeField] public float Force { get; private set; }
}
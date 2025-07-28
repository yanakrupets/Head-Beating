using System;
using Enums;
using UnityEngine;

namespace Serializable
{
    [Serializable]
    public struct HitData
    {
        [field: SerializeField] public HitType HitType { get; private set; }
        [field: SerializeField] public float Force { get; private set; }
    }
}
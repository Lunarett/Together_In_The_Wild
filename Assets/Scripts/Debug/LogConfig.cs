using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LogConfig", menuName = "PulseTools/Log", order = 1)]
public class LogConfig : ScriptableObject
{
    [Header("General")]
    [SerializeField] private float m_duration = 10.0f;
    [SerializeField] private int Limit = 30;
}

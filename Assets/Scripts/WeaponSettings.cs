using UnityEngine;
using System.Collections;

public class WeaponSettings : MonoBehaviour
{
    public static WeaponSettings Instance { get; private set; }

    [Header("Laser Settings")]
    public float _laserBaseReloadTime = 1;
    public float _laserBetterReloadTime = 0.5f;
    public float _laserSpeed = 2.5f;
    public int _laserDamage = 1;

    [Header("Beam Settings")]
    public float _beamReloadTime = 1;

    [Header("Missile Settings")]
    public float _missileReloadTime = 1;

    void Awake()
    {
        Instance = this;
    }
}

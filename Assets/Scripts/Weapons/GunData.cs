using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Weapon")]
public class GunData : ScriptableObject
{
    public GameObject bulletAsset;
    public bool bulletGravity;
    public float bulletForce;
    public float bulletTTL;
    public float gunDelay;
    public string gunName;
    public string gunDescription;
}

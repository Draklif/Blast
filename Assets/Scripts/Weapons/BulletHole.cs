using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHole : MonoBehaviour
{
    GameObject[] Attractables;
    Rigidbody AttractorRb;

    public float G = 1f;
    void Start()
    {
        Attractables = GameObject.FindGameObjectsWithTag("Target");
        AttractorRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Según F = G * m / d2
        foreach (GameObject Attractable in Attractables)
        {
            Rigidbody TargetRb = Attractable.GetComponent<Rigidbody>();

            float massProduct = TargetRb.mass * AttractorRb.mass * G;

            Vector3 dif = AttractorRb.position - TargetRb.position;
            float dist = dif.magnitude;

            float forceMagnitude = G * massProduct / Mathf.Pow(dist, 2);

            Vector3 forceDir = dif.normalized;
            Vector3 Force = forceDir * forceMagnitude;

            TargetRb.AddForce(Force);
        }
    }
}

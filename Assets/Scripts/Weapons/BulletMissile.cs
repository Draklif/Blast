using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMissile : MonoBehaviour
{
    private Vector3 stdPrediction, devPrediction;

    private float distMax = 100;
    private float distMin = 5;
    private float timeMax = 5;
    private float timeActivation = 1;
    private float deviationAmount = 50;
    private float deviationSpeed = 2;
    private float moveSpeed = 15;
    private float rotateSpeed = 95;

    private Rigidbody MissileRb;
    private GameObject Target;
    private GameObject[] Objectives;

    void Start()
    {
        Objectives = GameObject.FindGameObjectsWithTag("TargetLock");
        MissileRb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MissileRb.velocity = transform.forward * moveSpeed;

        // Si hay un objetivo, iniciar persecusión
        if (Target)
        {
            float leadTimePercentage = Mathf.InverseLerp(distMin, distMax, Vector3.Distance(transform.position, Target.transform.position));
            float predictionTime = Mathf.Lerp(0, timeMax, leadTimePercentage);

            Rigidbody TargetRb = Target.GetComponent<Rigidbody>();

            // Cálculo de predicción y desviación
            stdPrediction = TargetRb.position + TargetRb.velocity * predictionTime;

            Vector3 deviation = new Vector3(Mathf.Cos(Time.time * deviationSpeed), 0, 0);
            Vector3 offPrediction = transform.TransformDirection(deviation) * deviationAmount * leadTimePercentage;

            devPrediction = stdPrediction + offPrediction;

            // Rotar hacia dirección
            Vector3 heading = devPrediction - transform.position;
            Quaternion rotation = Quaternion.LookRotation(heading);
            MissileRb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.deltaTime));
        } 
        else
        {
            StartCoroutine(ActivateLockOn());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    private IEnumerator ActivateLockOn()
    {
        yield return new WaitForSeconds(timeActivation);

        // Encontrar el objetivo más cercano dentro de la lista de objetivos
        float previousDistance = 1000f;
        foreach (GameObject closestObjective in Objectives)
        {
            float actualDistance = Vector3.Distance(transform.position, closestObjective.transform.position);
            if (actualDistance < previousDistance)
            {
                previousDistance = actualDistance;
                Target = closestObjective;
            }
        }
    }
}

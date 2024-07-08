using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Animator animationController;
    string actualDance;
    void Start()
    {
        actualDance = StateController.actualDance;
        switch (actualDance)
        {
            case "Wave":
                animationController.SetBool("isWave", true);
                animationController.SetBool("isMacarena", false);
                break;
            case "Macarena":
                animationController.SetBool("isWave", false);
                animationController.SetBool("isMacarena", true);
                break;
            case "House":
                animationController.SetBool("isWave", false);
                animationController.SetBool("isMacarena", false);
                break;
            default:
                break;
        }
    }
}

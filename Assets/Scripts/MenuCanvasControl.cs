using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuCanvasControl : MonoBehaviour
{
    [SerializeField] Animator animationController;
    [SerializeField] Text text;
    [SerializeField] GameObject character;

    string actualDance;

    public void StartWave()
    {
        animationController.SetBool("isWave", true);
        animationController.SetBool("isMacarena", false);
        actualDance = "Wave";
        text.text = actualDance;
    }

    public void StartMacarena()
    {
        animationController.SetBool("isWave", false);
        animationController.SetBool("isMacarena", true);
        actualDance = "Macarena";
        text.text = actualDance;
    }

    public void StartHouse()
    {
        animationController.SetBool("isWave", false);
        animationController.SetBool("isMacarena", false);
        actualDance = "House";
        text.text = actualDance;
    }

    public void SwapScene(string scene)
    {
        StateController.actualDance = actualDance;
        SceneManager.LoadScene(scene);
    }

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            character.transform.Rotate(Vector3.up * 100f * Time.deltaTime * -1, Space.Self);
        }

        if (Input.GetKey(KeyCode.A))
        {
            character.transform.Rotate(Vector3.up * 100f * Time.deltaTime, Space.Self);
        }
    }
}

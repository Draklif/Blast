using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseCanvasControl : MonoBehaviour
{
    [SerializeField] Text sensText, fovText;
    [SerializeField] Slider sensSlider, fovSlider;
    [SerializeField] Toggle camToggle;
    [SerializeField] GameObject playerUI;
    [SerializeField] public GameObject pauseGO;

    public bool isPaused;
    private PlayerController playerController;

    public void LoadValues()
    {
        // Carga los valores de las configuraciones
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        sensSlider.value =  playerController.mouseSensitivity / 10;
        fovSlider.value = playerController.fov;
        camToggle.isOn = playerController.invertCamera;

        UpdateSens();
        UpdateFOV();
        ToggleInvert();
    }

    public void UpdateSens()
    {
        sensText.text = sensSlider.value.ToString();
        playerController.mouseSensitivity = sensSlider.value * 10;
    }

    public void UpdateFOV()
    {
        fovText.text = fovSlider.value.ToString();
        playerController.fov = fovSlider.value;
        playerController.playerCamera.fieldOfView = fovSlider.value;
    }

    public void ToggleInvert()
    {
        playerController.invertCamera = camToggle.isOn;
    }

    public void TogglePause(bool isPaused)
    {
        this.isPaused = isPaused;
        LoadValues();
        pauseGO.SetActive(isPaused);
        playerUI.SetActive(!isPaused);
        Cursor.visible = isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = isPaused ? 0f : 1.0f;
    }

    public void SwapScene(string scene)
    {
        TogglePause(false);
        SceneManager.LoadScene(scene);
    }
}

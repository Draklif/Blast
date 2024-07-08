using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] PauseCanvasControl pauseController;
    [SerializeField] Transform gunSocket;

    List<GameObject> GunList = new List<GameObject>();
    GunBehaviour CurrentGun;

    void Update()
    {
        if (!pauseController.isPaused)
        {
            if (GunList.Count > 0)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    CurrentGun.Shoot();
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) && GunList.Count > 0) SwitchWeapon(0);

            if (Input.GetKeyDown(KeyCode.Alpha2) && GunList.Count > 1) SwitchWeapon(1);

            if (Input.GetKeyDown(KeyCode.Alpha3) && GunList.Count > 2) SwitchWeapon(2);
        }
    }

    void SwitchWeapon(int index)
    {
        // Si tiene un arma actualmente, la oculta para mostrar la nueva
        if (CurrentGun) CurrentGun.gameObject.SetActive(false);

        // Selecciona el arma
        CurrentGun = GunList[index].GetComponent<GunBehaviour>();
        CurrentGun.gameObject.SetActive(true);

        // Bloquea el disparo para prevenir spam y añade delay según el delay del arma
        CurrentGun.canShoot = false;
        StartCoroutine(SwitchDelay());
    }

    public void AddWeapon(GameObject Weapon)
    {
        // Si tiene un arma actualmente, la oculta para mostrar la nueva
        if (CurrentGun) CurrentGun.gameObject.SetActive(false);

        // Selecciona el arma y la añade a la lista
        CurrentGun = Weapon.GetComponent<GunBehaviour>();
        GunList.Add(Weapon);

        // Mueve el arma al socket del jugador y lo emparenta
        Weapon.transform.position = gunSocket.position;
        Weapon.transform.rotation = gunSocket.rotation;
        Weapon.transform.parent = gunSocket;
    }

    private IEnumerator SwitchDelay()
    {
        yield return new WaitForSeconds(CurrentGun.gun.gunDelay);
        CurrentGun.canShoot = true;
    }
}

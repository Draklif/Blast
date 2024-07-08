using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunBehaviour : MonoBehaviour
{
    [SerializeField] public GunData gun;

    public bool canShoot = true;

    private GameObject player;
    private Transform muzzleSocket;

    void Start()
    {
        muzzleSocket = gameObject.transform.GetChild(0);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Shoot()
    {
        // Valida si puede disparar
        if (canShoot)
        {
            player.GetComponent<CanvasController>().AddScore(-1); ;
            GameObject bulletInstance = Instantiate(gun.bulletAsset, muzzleSocket.transform.position, muzzleSocket.transform.rotation);
            Rigidbody bulletRB = bulletInstance.GetComponent<Rigidbody>();
            bulletRB.useGravity = gun.bulletGravity;
            bulletRB.AddForce(transform.forward * gun.bulletForce, ForceMode.Impulse);
            Destroy(bulletInstance, gun.bulletTTL);
            canShoot = false;
            StartCoroutine(ShootDelay());
        }
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(gun.gunDelay);
        canShoot = true;
    } 

    #region OnTrigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Player"))
        {
            player.GetComponent<CanvasController>().SetInteractionText(name);
            player.GetComponent<CanvasController>().SetDescriptionText(gun.gunDescription);
            player.GetComponent<CanvasController>().ToggleInteraction(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Equals("Player")) player.GetComponent<CanvasController>().ToggleInteraction(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag.Equals("Player") && Input.GetKey(KeyCode.E))
        {
            other.gameObject.GetComponent<GunController>().AddWeapon(gameObject);
            player.GetComponent<CanvasController>().ToggleInteraction(false);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
    #endregion
}

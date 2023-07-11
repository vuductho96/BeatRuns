using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupgun : MonoBehaviour
{
    public Animator Anim;
    public GameObject GunObject;
    public GameObject TargetGun;
    public GameObject PickUpText;
    public GameObject bulletPrefab;
    public GameManager shoottingpoint;

    private string swordTag = "Gun";
    private GameObject currentWeapon;
    private bool hasPickedUpSword = false;
    private bool weaponInRange;
    private bool hasGunObject = false; // Flag to indicate if the player has the gun object

    private void Start()
    {
        Anim = GetComponent<Animator>();
        PickUpText.SetActive(false);
        TargetGun.SetActive(false);
        currentWeapon = GunObject;
    }

    private void OutOfRange()
    {
        PickUpText.SetActive(false);
        weaponInRange = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !hasPickedUpSword && weaponInRange)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 2.0f);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag(swordTag))
                {
                    PickUp(collider.gameObject);
                    hasPickedUpSword = true;
                    hasGunObject = true; // Set the flag to indicate that the player has the gun object
                    break;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q) && hasPickedUpSword)
        {
            Drop();
            hasPickedUpSword = false;
            hasGunObject = false; // Reset the flag since the player has dropped the gun object
        }

        // Toggle the crosshair with the aiming functionality

        if (hasGunObject && Input.GetKeyDown(KeyCode.F) && TargetGun.activeSelf)
        {
            Shoot();
        }
    }

    private void PickUp(GameObject sword)
    {
        GunObject.SetActive(false);
        currentWeapon.SetActive(false);
        currentWeapon = sword;
        TargetGun.SetActive(true);
        PickUpText.SetActive(false);
        Debug.Log("Picked up the Gun");
    }

    private float GetGroundHeight(Vector3 position)
    {
        RaycastHit hit;
        float groundHeight = 0f;
        if (Physics.Raycast(position, Vector3.down, out hit))
        {
            groundHeight = hit.point.y;
        }
        return groundHeight;
    }

    private void Drop()
    {
        TargetGun.SetActive(false);
        currentWeapon.SetActive(true);
        PickUpText.SetActive(false);

        Vector3 dropPosition = transform.position + transform.forward * 2.0f;
        dropPosition.y = GetGroundHeight(dropPosition) + 0.5f;

        currentWeapon.transform.position = dropPosition;

        Debug.Log("Dropped the Gun");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag(swordTag) && !hasPickedUpSword)
        {
            PickUpText.SetActive(true);
            weaponInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(swordTag))
        {
            OutOfRange();
        }
    }

    private void Shoot()
    {
        Anim.SetTrigger("Shoot");
        // Add your shooting logic here
        Debug.Log("Shoot");
    }
}

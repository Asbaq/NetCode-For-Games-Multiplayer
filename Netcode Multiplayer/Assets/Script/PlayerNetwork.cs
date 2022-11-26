using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour{
      
      // public GameObject bulletPrefab;
      public Transform cameraTransform;
      // public float fireRate = 15f;
      public float mouseSensitivity = 100f;
      public float movespeed = 6;
      private int damage = 1;
      GameObject s;
      new Rigidbody rigidbody;
      Vector3 velocity; 
      // private float nextTimeToFire = 0f;
       public float IForce = 30f;

   // Cursor.lockState = CursorLockMode.Locked;
    void Start()
    {
      // s = GameObject.FindGameObjectWithTag("Obj");
      cameraTransform = GetComponentInChildren<Camera>().transform;
      if(!IsOwner)
      {
         cameraTransform.GetComponent<Camera>().enabled = false;
         cameraTransform.GetComponent<AudioListener>().enabled = false;
      }
      // if(IsOwner)
      // {
      //    s.SetActive(true);  
      // }
      rigidbody = GetComponent<Rigidbody>();
    }
      // Cursor.lockState = CursorLockMode.Locked;
    //Update is called once per frame

    void Update()
    {
      if(!IsOwner) return;
      // Looking at by mouse input
      float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
      transform.Rotate(Vector3.up * mouseX);
      
      // Movement in x and z axis
      velocity = new Vector3 (Input.GetAxisRaw ("Horizontal"),0,Input.GetAxisRaw("Vertical")).normalized * movespeed; 
       
    //   if(Input.GetKeyDown(KeyCode.G)) //&& Time.time >= nextTimeToFire)
    //   {   
    //      Debug.Log("G");
    //      // nextTimeToFire = Time.time + 1f / fireRate;
    //      CmdFire();
    //   }
     }

    // public void CmdFire() 
    //     {
    //         RaycastHit hit;
    //         if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity )) 
    //         {
    //            Debug.Log(hit.transform.name);
    //            Health health = hit.transform.GetComponent<Health>();
		//         if (health != null)
		//         {
		//         	health.TakeDamage(damage);
		//         }
    //            GameObject bullet = GameObject.Instantiate(bulletPrefab, hit.point, Quaternion.LookRotation(hit.normal));
    //            Destroy(bullet,2f);
    //         }
    //     }
    void FixedUpdate()
    {
      // to move the rigidbody in fixed frames
      rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Bullet : MonoBehaviour
{
    // [HideInInspector]
	// private GameObject bulletDecal;

	private float speed = 1f;
	private float timeToDestroy = 3f;
	public Vector3 target { get; set; }
	public bool hit { get; set; }

	private void OnEnable()
	{
		Destroy(gameObject, timeToDestroy);
	}
	private void Update()
	{
		Debug.Log("A");
		transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
		if(!hit && Vector3.Distance(transform.position, target) < 0.01f)
		{
			Debug.Log("Y");
			Destroy(gameObject);
		}
	}
	void OnCollisionEnter(Collision collision)
	{	
		// ContactPoint contact = collision.GetContact(0);
		// GameObject.Instantiate(bulletDecal, contact.point + contact.normal * 0.0001f, Quaternion.LookRotation(contact.normal));
		// var hit = collision.gameObject;
		// var health = hit.GetComponent<Health>();
		// if (health != null)
		// {
		// 	health.TakeDamage(target, 10);
		// }
		// Destroy(gameObject);
	}
}



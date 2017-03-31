using UnityEngine;
using System.Collections;

public class AlienController : MonoBehaviour
{
	public GameObject shot;
	public Transform shotspawn;
	public float fireRate;
	private float nextFire;

	void Update()
	{
		if (Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotspawn.position, shotspawn.rotation);
			GetComponent<AudioSource> ().Play();
		}
	}


}

using UnityEngine;
using System.Collections;

	public class PlayerController2 : MonoBehaviour 
	{
		public float speed;
		public float tilt;
	    public float xMin, xMax, zMin, zMax;

		public GameObject shot;
		public Transform shotSpawn;
		public float fireRate;
		private float nextfire;

		void Update()
		{
			if (Input.GetButton ("Fire1.1") && Time.time > nextfire)
			{
				nextfire = Time.time + fireRate;
				Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
				//GetComponent<audio> ().Play;
				GetComponent<AudioSource>().Play();
			}
		}

		void FixedUpdate()
		{
			float moveHorizontal = Input.GetAxis ("Horizontal1");
			float moveVertical = Input.GetAxis ("Vertical1");

			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			GetComponent<Rigidbody>().velocity = movement * speed;
			GetComponent<Rigidbody> ().position = new Vector3 
				(
					Mathf.Clamp (GetComponent<Rigidbody> ().position.x, xMin, xMax),
					0.0f,	
					Mathf.Clamp (GetComponent<Rigidbody> ().position.z, zMin, zMax)
				);

			GetComponent<Rigidbody> ().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody> ().velocity.x * -tilt);
		}
	}




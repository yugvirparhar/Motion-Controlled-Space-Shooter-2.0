using UnityEngine;
using System.Collections;

public class DestroyByContact3 : MonoBehaviour 
{
	public GameObject explosion;
	public GameObject playerExplosion;

	public int scoreValue;
	private GameController3 gameController;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController3");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController3> ();
		}
		if (gameControllerObject == null)
		{
			Debug.Log ("Cannot Find 'GameController' script");
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Boundary")
		{
			return;
		}
			
		Instantiate (explosion, transform.position, transform.rotation);

		if (other.tag == "Player") 
		{
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
		    gameController.GameOver ();
		}
		gameController.AddScore (scoreValue);
		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}

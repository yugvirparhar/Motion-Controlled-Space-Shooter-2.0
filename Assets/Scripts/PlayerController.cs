using UnityEngine;
using System.Collections;
//using System.Collections;
using System.Collections.Generic;
//using UnityEngine;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
    TcpClient client;
    Stream s;
    StreamReader sr;
    StreamWriter sw;
    Thread recvT;
    public bool ready = false;
    public bool running = true;
    public string message;
    string temp;

    public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextfire;


    void Start()
    {
        new Thread(initThread).Start();
    }

    void Update()
	{
		if (message=="0" && Time.time > nextfire)
		{
            print("shoot");
			nextfire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			//GetComponent<audio> ().Play;
			GetComponent<AudioSource>().Play();
		}
	}

    public void initThread()
    {
        TcpClient client = new TcpClient("127.0.0.1", 5204);
        s = client.GetStream();
        sr = new StreamReader(s);
        sw = new StreamWriter(s);
        sw.AutoFlush = true;

        recvT = new Thread(recvThread);
        recvT.Start();

        ready = true;
    }

    public void send(string message)
    {
        temp = message;
        new Thread(sendThread).Start();
    }

    public void sendThread()
    {
        try
        {
            string name = temp;
            sw.WriteLine(name);
        }
        catch
        {
        }
    }

    public void recvThread()
    {
        print("RecvThread started..");
        while (running)
        {
            try
            {
                message = sr.ReadLine();
                print("Recieved : " + message);
                //FixedUpdate(message);
                // WindowsVoice.theVoice.speak(message);

            }
            catch (Exception e)
            {
                print(e);
            }
        }
    }


    void FixedUpdate()
	{
        if (message == "2")
        {
            print("left");
           // float moveHorizontal = Input.GetAxis("Horizontal");
            //float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(-0.2f, 0.0f, 0.0f);
 

            GetComponent<Rigidbody>().velocity = movement * speed;
            GetComponent<Rigidbody>().position = new Vector3
                (
                Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
            );

            GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
        }

        if (message == "3")
        {
            print("right");
            // float moveHorizontal = Input.GetAxis("Horizontal");
            //float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(0.2f, 0.0f, 0.0f);


            GetComponent<Rigidbody>().velocity = movement * speed;
            GetComponent<Rigidbody>().position = new Vector3
                (
                Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
            );

            GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
        }
    }

}


using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	public SwanSpot[] spawnSpots;
	// Use this for initialization
	void Start () {
		Connect();
		spawnSpots = GameObject.FindObjectsOfType<SwanSpot> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Connect(){
		PhotonNetwork.ConnectUsingSettings ("super demo FPS version 1.0");
	}

	void OnGUI(){
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
	}

	void OnJoinedLobby(){
		Debug.Log ("on joined lobby");
		PhotonNetwork.JoinRandomRoom ();
	}

	void OnPhotonRandomJoinFailed(){
		Debug.Log ("on joined lobby fail");
		PhotonNetwork.CreateRoom (null);
	}

	void OnJoinedRoom(){
		Debug.Log("On joined Room");
		SpawnMyPlayer ();
	}

	void SpawnMyPlayer(){
		if (spawnSpots == null) {
			Debug.Log("spawnSpot == null");
			return;
		}
		SwanSpot mySpawnSpot = spawnSpots[ Random.Range (0, spawnSpots.Length)];
		GameObject playerGO = (GameObject)PhotonNetwork.Instantiate ("PlayerHero", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
		((MonoBehaviour)playerGO.GetComponent ("NetworkCharacter")).enabled = false;

		((MonoBehaviour)playerGO.GetComponent ("FPSInputController")).enabled = true;
//		playerGO.GetComponent<CharacterMotor> ().enabled = true;
		((MonoBehaviour)playerGO.GetComponent ("CharacterMotor")).enabled = true;

//		playerGO.transform.FindChild("Hero").transform.GetComponent<Animator>().SetBool()
//		((MonoBehaviour)playerGO.GetComponent ("MouseLook")).enabled = true;
//		playerGO.transform.FindChild ("Main Camera").gameObject.SetActive (true);
//		playerGO.transform.FindChild ("Main Camera").transform.GetComponent<SmoothFollowCamera2> ().target = playerGO.transform.FindChild ("Hero").gameObject;

	}
}

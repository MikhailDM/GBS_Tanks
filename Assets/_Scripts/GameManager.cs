﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject PlayerPrefab;

    public GameObject spawnPosP1;
    public GameObject spawnPosP2;


    // Start is called before the first frame update
    void Start()
    {
        //Спавн персонажей
        //Vector2 pos = new Vector3(0, 0);
        Vector2 pos1 = spawnPosP1.transform.position;
        Vector2 pos2 = spawnPosP2.transform.position;
        if (PhotonNetwork.PlayerList.Length == 1)
        {
            PhotonNetwork.Instantiate(PlayerPrefab.name, pos1, Quaternion.identity);
        } else
        {
            PhotonNetwork.Instantiate(PlayerPrefab.name, pos2, Quaternion.identity);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Кнопка покидания комнаты
    /*public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }*/


    //Когда текущий игрок покидает комнату
    public override void OnLeftRoom()
    {
        //Загружаем сцену
        // SceneManager.LoadScene(0);
        PhotonNetwork.LoadLevel("TanksLobby");
    }

    //Игрок зашел в комнату
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("Player {0} enteren room", newPlayer.NickName);
    }

    /*//Игрок вышел из комнаты
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat("Player {0} left room", otherPlayer.NickName);
    }*/
}

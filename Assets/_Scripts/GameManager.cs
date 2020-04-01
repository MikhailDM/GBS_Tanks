using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Кнопка покидания комнаты
    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }


    //Когда текущий игрок покидает комнату
    public override void OnLeftRoom()
    {
        //Загружаем сцену
        SceneManager.LoadScene(0);
    }

    //Игрок зашел в комнату
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("Player {0} enteren room", newPlayer.NickName);
    }

    //Игрок вышел из комнаты
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat("Player {0} left room", otherPlayer.NickName);
    }
}

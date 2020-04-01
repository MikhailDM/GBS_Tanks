using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    //Поле debug
    public Text LogText;

    //private byte maxPlayersPerRoom = 2;

    void Start()
    {
        //Настройки фотона
        //Задаем имя игрокам
        PhotonNetwork.NickName = "Player" + Random.Range(1000, 9999);
        //Сообщение с именем игрока
        Log("Player's name is set to " + PhotonNetwork.NickName);

        //Автоматическое переключение сцен на всех клиентах
        PhotonNetwork.AutomaticallySyncScene = true;
        //Версия игры
        PhotonNetwork.GameVersion = "1";
        //Подключение к мастер серверу
        PhotonNetwork.ConnectUsingSettings();
    }

    //Подлючение к серверу
    public override void OnConnectedToMaster()
    {
        Log("Connected to master");
    }

    //Кнопка создания комнаты. Максимально 2 игрока
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
    }

    //Кнопка присоединения к комнате
    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    //Действия при подключении к комнате
    public override void OnJoinedRoom()
    {
        //Лог
        Log("Joined to the room");
        //Переключение сцены
        PhotonNetwork.LoadLevel("Game");
    }




    //Отображение сообщений консоли в клиенте
    private void Log(string message)
    {
        Debug.Log(message);
        LogText.text += "\n";
        LogText.text += message;
    }
}

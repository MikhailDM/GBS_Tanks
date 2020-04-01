using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    //Поле debug
    public Text LogText;

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

    public override void OnConnectedToMaster()
    {
        Log("Connected to master");
    }

    //Отображение сообщений консоли в клиенте
    private void Log(string message)
    {
        Debug.Log(message);
        LogText.text += "\n";
        LogText.text += message;
    }
}

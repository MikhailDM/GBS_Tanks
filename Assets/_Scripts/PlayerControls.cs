using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();        
    }

    
    void Update()
    {
        //Проверка на управление только своим персонажем
        if (!photonView.IsMine)
        {
            return;
        }
        //Управление персонажем
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-Time.deltaTime * 5, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Time.deltaTime * 5, 0, 0);
        }


    }
}

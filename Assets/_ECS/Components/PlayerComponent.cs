﻿using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Photon.Pun;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct PlayerComponent : IComponent {    
    public PhotonView PhotonViewLink;
    public Vector3 Position;
    public Transform Transform;
    public GameObject bulletPrefab;
    public int lives;
    public int speed;
}
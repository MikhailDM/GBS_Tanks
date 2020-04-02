using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Photon.Pun;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerControlSystem))]
public sealed class PlayerControlSystem : UpdateSystem {

    private Filter filter;
    private Vector2 bulletInstPos;

    public override void OnAwake()
    {
        //Ищем через фильтр компонент
        this.filter = this.World.Filter.With<PlayerComponent>();
        StartBulletPos();
    }

    public override void OnUpdate(float deltaTime) {
        UpdateMovement();
    }


    private void UpdateMovement()
    {
        var playersBag = this.filter.Select<PlayerComponent>();
        //Перебираем все сущности в фильтре
        for (int i = 0, length = this.filter.Length; i < length; i++)
        {
            //Получаем все ссылки компонента
            ref var playerComponent = ref playersBag.GetComponent(i);            

            //Проверка на управление только своим персонажем
            if (!playerComponent.PhotonViewLink.IsMine)
            {
                return;
            }
            //Управление персонажем
            if (Input.GetKey(KeyCode.A))
            {
                playerComponent.Transform.Translate(0, Time.deltaTime * playerComponent.speed, 0);
                playerComponent.Transform.localRotation = Quaternion.Euler(0, 0, 90);

                Vector2 currentPos = playerComponent.Transform.position;
                Vector2 add = new Vector2(-0.5f, 0);
                bulletInstPos = currentPos + add;                
            }
            else if (Input.GetKey(KeyCode.D))
            {
                playerComponent.Transform.Translate(0, Time.deltaTime * playerComponent.speed, 0);
                //playerComponent.Transform.localRotation = Quaternion.Euler(0, 0, 0);
                playerComponent.Transform.localRotation = Quaternion.Euler(0, 0, 270);

                Vector2 currentPos = playerComponent.Transform.position;
                Vector2 add = new Vector2(0.5f, 0);
                bulletInstPos = currentPos + add;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                playerComponent.Transform.Translate(0, Time.deltaTime * playerComponent.speed, 0);
                //playerComponent.Transform.Rotate(0, 0, 0);
                playerComponent.Transform.localRotation = Quaternion.Euler(0, 0, 0);

                Vector2 currentPos = playerComponent.Transform.position;
                Vector2 add = new Vector2(0, 0.5f);
                bulletInstPos = currentPos + add;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                playerComponent.Transform.Translate(0, Time.deltaTime * playerComponent.speed, 0);
                playerComponent.Transform.localRotation = Quaternion.Euler(0, 0, 180);
                
                Vector2 currentPos = playerComponent.Transform.position;
                Vector2 add = new Vector2(0, -0.5f);
                bulletInstPos = currentPos + add;
            }

            //Запускаем снаряд!!
            if (Input.GetKey(KeyCode.Space))
            {                               
                Instantiate(playerComponent.bulletPrefab, bulletInstPos, playerComponent.Transform.rotation);
                
            }
        }
    }

    private void StartBulletPos()
    {
        var playersStartBag = this.filter.Select<PlayerComponent>();
        for (int i = 0, length = this.filter.Length; i < length; i++)
        {
            //Получаем все ссылки компонента
            ref var playerComponent = ref playersStartBag.GetComponent(i);
            Vector3 add = new Vector3(0, 0.5f, 0);
            bulletInstPos = playerComponent.Transform.position + add;
        }
    }
    
}
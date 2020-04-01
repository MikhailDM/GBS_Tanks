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

    public override void OnAwake() {
        //Ищем через фильтр компонент
        this.filter = this.World.Filter.With<PlayerComponent>();
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
            }
            else if (Input.GetKey(KeyCode.D))
            {
                playerComponent.Transform.Translate(0, Time.deltaTime * playerComponent.speed, 0);
                //playerComponent.Transform.localRotation = Quaternion.Euler(0, 0, 0);
                playerComponent.Transform.localRotation = Quaternion.Euler(0, 0, 270);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                playerComponent.Transform.Translate(0, Time.deltaTime * playerComponent.speed, 0);
                //playerComponent.Transform.Rotate(0, 0, 0);
                playerComponent.Transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                playerComponent.Transform.Translate(0, Time.deltaTime * playerComponent.speed, 0);
                playerComponent.Transform.localRotation = Quaternion.Euler(0, 0, 180);
            }
        }
    }
}
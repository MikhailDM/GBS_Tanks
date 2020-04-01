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
        var playersBag = this.filter.Select<PlayerComponent>();
        //Перебираем все сущности в фильтре
        for (int i = 0, length = this.filter.Length; i < length; i++)
        {
            ref var playerComponent = ref playersBag.GetComponent(i);
            //Изменение координат обьекта
            //playerComponent.Position = playerComponent.Position + Vector3.one * deltaTime;
            //Перемещение обьекта
            //playerComponent.Transform.position = playerComponent.Position;
            //Проверка на управление только своим персонажем
            /*if (!PhotonView.IsMine)
            {
                return;
            }*/
            //Управление персонажем
            if (Input.GetKey(KeyCode.A))
            {
                playerComponent.Transform.Translate(-Time.deltaTime * 5, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                playerComponent.Transform.Translate(Time.deltaTime * 5, 0, 0);
            }
        }
    }
}
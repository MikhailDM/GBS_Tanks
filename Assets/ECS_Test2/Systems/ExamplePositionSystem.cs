using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ExamplePositionSystem))]
public sealed class ExamplePositionSystem : UpdateSystem {
    private Filter filter;


    public override void OnAwake() {
        this.filter = this.World.Filter.With<UnitComponent>();

        //Создаем обьекты
        //for (int i = 0, length = 5; i < length; i++)
        //{
        //   var ent = World.CreateEntity();
        //   ent.AddComponent<UnitComponent>();
        //}
    }

    public override void OnUpdate(float deltaTime) {
        foreach (var entity in this.filter)
        {
            ref var unit = ref entity.GetComponent<UnitComponent>();
            //Перемещает сущности
            unit.Position = unit.Position + Vector3.one * deltaTime;
            Debug.Log("Test");
        }
    }
}
using Morpeh;
using Morpeh.Globals;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ExamplePositionSystem))]
public sealed class ExamplePositionSystem : UpdateSystem {
    public GlobalEvent StopEvent;
    public GlobalEvent FreeEvent;

    private Filter filterMovableUnits;
    private Filter filterStoppedUnits;

    public override void OnAwake() {
        this.filterMovableUnits = this.World.Filter.With<UnitComponent>().Without<UnitStoppedMarker>();
        this.filterStoppedUnits = this.World.Filter.With<UnitComponent>().With<UnitStoppedMarker>();

        //Создаем обьекты
        //for (int i = 0, length = 5; i < length; i++)
        //{
        //   var ent = World.CreateEntity();
        //   ent.AddComponent<UnitComponent>();
        //}
    }

    public override void OnUpdate(float deltaTime) {       
        StopUnits();
        FreeUnits();
        MoveUnits(deltaTime);
    }

    private void FreeUnits()
    {
        if (FreeEvent.IsPublished)
        {
            foreach (var entity in this.filterStoppedUnits)
            {
                entity.RemoveComponent<UnitStoppedMarker>();
            }
        }

    }

    private void StopUnits()
    {
        if (StopEvent.IsPublished)
        {
            foreach (var entity in this.filterMovableUnits)
            {
                entity.AddComponent<UnitStoppedMarker>();
            }
        }
        
    }


    private void MoveUnits(float deltaTime)
    {
        foreach (var entity in this.filterMovableUnits)
        {
            ref var unit = ref entity.GetComponent<UnitComponent>();
            //Изменение координат
            unit.Position = unit.Position + Vector3.one * deltaTime;
            //Debug.Log("Test");
        }
    }
}
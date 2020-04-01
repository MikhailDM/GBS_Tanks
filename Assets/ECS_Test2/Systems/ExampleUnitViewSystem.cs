using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ExampleUnitViewSystem))]
public sealed class ExampleUnitViewSystem : UpdateSystem {

    private Filter filter;

    public override void OnAwake() {
        this.filter = this.World.Filter.With<UnitViewComponent>();
    }

    public override void OnUpdate(float deltaTime) {
        //Все обьекты компонентов
        var units = this.filter.Select<UnitComponent>();
        var views = this.filter.Select<UnitViewComponent>();

        //Перебираем все сущности в фильтре
        for (int i = 0, length = this.filter.Length; i < length; i++)
        {
            ref var unit = ref units.GetComponent(i);
            ref var view = ref views.GetComponent(i);

            view.Transform.position = unit.Position;
        }
    }
}
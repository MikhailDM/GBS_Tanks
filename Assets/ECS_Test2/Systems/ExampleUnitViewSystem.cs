using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ExampleUnitViewSystem))]
public sealed class ExampleUnitViewSystem : UpdateSystem {

    //Переменные фильтров
    private Filter filter;
    private Filter filterInitialize;

    public override void OnAwake() {
        //Общий фильтр
        var commonFilter = this.World.Filter.With<UnitViewComponent>().With<UnitComponent>();

        //Фильтруем элементы
        this.filter = commonFilter.With<UnitViewInitializedMarker>();
        this.filterInitialize = commonFilter.Without<UnitViewInitializedMarker>();
    }

    public override void OnUpdate(float deltaTime) {
        InitializeUnits();
        UpdateViews();
    }

    //Инициалзируем юниты в точках размещения а не в нулевой
    private void InitializeUnits()
    {
        //Все обьекты компонентов
        var units = this.filterInitialize.Select<UnitComponent>();
        var views = this.filterInitialize.Select<UnitViewComponent>();

        //Перебираем все сущности в фильтре
        for (int i = 0, length = this.filterInitialize.Length; i < length; i++)
        {
            ref var unit = ref units.GetComponent(i);
            ref var view = ref views.GetComponent(i);
           
            unit.Position = view.Transform.position;
            var ent = this.filterInitialize.GetEntity(i);
            ent.AddComponent<UnitViewInitializedMarker>();
        }
    }

    //Метод перемещения компонентов
    private void UpdateViews() {
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
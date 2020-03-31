using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(HealthSystem))]
public sealed class HealthSystem : UpdateSystem {

    private Filter filter;

    public override void OnAwake() {
        //Ищем через фильтр компонент
        this.filter = this.World.Filter.With<HealthComponent>();
        //You can chain filters by two operators With<> and Without<>.
        //For example this.World.Filter.With<FooComponent>().With<BarComponent>().Without<BeeComponent>();
    }    

    public override void OnUpdate(float deltaTime)
    {
        var healthBag = this.filter.Select<HealthComponent>();
        //Перебираем все сущности в фильтре
        for (int i = 0, length = this.filter.Length; i < length; i++)
        {
            ref var healthComponent = ref healthBag.GetComponent(i);
            //Печатает количество жизней у сущности
            Debug.Log("Health Points is:" + healthComponent.healthPoints);
            Debug.Log("Health Text is:" + healthComponent.healthText);
        }

        /*//Перебираем все сущности в фильтре
        foreach (var entity in this.filter)
        {
            ref var healthComponent = ref entity.GetComponent<HealthComponent>();
            //Печатает количество жизней у сущности
            Debug.Log(healthComponent.healthPoints);
        }*/
    }
}
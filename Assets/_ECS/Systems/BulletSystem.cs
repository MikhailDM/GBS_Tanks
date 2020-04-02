using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(BulletSystem))]
public sealed class BulletSystem : UpdateSystem {

    private Filter filter;

    public override void OnAwake() {
        this.filter = this.World.Filter.With<BulletComponent>();
    }

    public override void OnUpdate(float deltaTime) {
        moveForward();
    }


    private void moveForward()
    {
        var bulletsBag = this.filter.Select<BulletComponent>();

        //Перебираем все сущности в фильтре
        for (int i = 0, length = this.filter.Length; i < length; i++)
        {
            //Получаем все ссылки компонента
            ref var bulletComponent = ref bulletsBag.GetComponent(i);

            //Двигаем снаряд вперед
            //bulletComponent.transform.Translate(Vector2.up * Time.deltaTime * bulletComponent.bulletSpeed);
            bulletComponent.transform.Translate(0, Time.deltaTime * bulletComponent.bulletSpeed, 0);
        }
    }
}
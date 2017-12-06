using UnityEngine;
using Zenject;

public class MonsterInstaller : MonoInstaller<MonsterInstaller>
{

    public override void InstallBindings()
    {
		MonsterData.Initialize();
		Container.Bind<string>().WithId("recipeList").FromInstance(MonsterData.recipes);
    }
}
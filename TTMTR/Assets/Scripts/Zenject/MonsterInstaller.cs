using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class MonsterInstaller : MonoInstaller<MonsterInstaller>
{

    public override void InstallBindings()
    {
		MonsterData.Initialize();
		Container.Bind<List<CraftingTool.Recipe>>().WithId("recipeList").FromInstance(MonsterData.recipes);
    }
}
using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class MonsterInstaller : MonoInstaller<MonsterInstaller>
{

    public override void InstallBindings()
    {
		MonsterData.Initialize();
		Container.Bind<List<CraftingTool.Recipe>>().WithId("recipeList").FromInstance(MonsterData.recipes);
		Container.Bind<List<AreaSpawn.SignWithCount>>().WithId("areaSignsToSpawn").FromInstance(XMLReaderTool.areaSigns);
		Container.Bind<List<Sign>>().WithId("signsToSpawn").FromInstance(XMLReaderTool.spotSignsPrefabs);
    }
}
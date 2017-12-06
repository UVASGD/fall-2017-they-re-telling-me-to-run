using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class MonsterInstaller : MonoInstaller<MonsterInstaller>
{
	public GameController gameCont;
	public List<Transform> wanderPoints;

    public override void InstallBindings()
    {
		MonsterData.Initialize();

		Container.Bind<List<CraftingTool.Recipe>>().WithId("recipeList").FromInstance(MonsterData.recipes).AsSingle();
		Container.Bind<AreaSpawn.SignWithCount[]>().WithId("areaSignsToSpawn").FromInstance(XMLReaderTool.areaSigns.ToArray()).AsSingle();
		Container.Bind<Sign[]>().WithId("signsToSpawn").FromInstance(XMLReaderTool.spotSignsPrefabs.ToArray()).AsSingle();
		Container.Bind<GameController>().WithId("gameController").FromInstance(gameCont).AsSingle();
		Container.Bind<List<Transform>>().WithId("wanderPoints").FromInstance(wanderPoints).AsSingle();
    }
}
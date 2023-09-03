using Game.Scripts.Controllers;
using Game.Scripts.Enemy;
using Game.Scripts.Signals;
using Game.Scripts.Tile;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Transform tileHolder;
    
    #region Injection

    private ApplicationSettings _applicationSettings;

    [Inject]
    private void Construct(ApplicationSettings applicationSettings)
    {
        _applicationSettings = applicationSettings;
    }

    #endregion

    public override void InstallBindings()
    {
        GameSignalsInstaller.Install(Container);

        Container.Bind<LevelModel>().AsSingle();

        Container.BindInterfacesAndSelfTo<ApplicationController>().AsSingle();
        Container.BindInterfacesAndSelfTo<LevelController>().AsSingle();
        Container.BindInterfacesAndSelfTo<WordController>().AsSingle();
        

        InstallTiles();
    }
    
    private void InstallTiles()
    {
        Container.BindFactory<TileFacade.Args, TileFacade, TileFacade.Factory>()
            .FromPoolableMemoryPool<TileFacade.Args, TileFacade, TileFacade.Pool>(poolBinder => poolBinder
                .WithInitialSize(20)
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<TileInstaller>(_applicationSettings.tileView)
                .UnderTransform(tileHolder));
    }
}
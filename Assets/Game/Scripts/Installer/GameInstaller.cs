using Game.Scripts.Signals;
using Zenject;

public class GameInstaller : MonoInstaller
{
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
    }
}
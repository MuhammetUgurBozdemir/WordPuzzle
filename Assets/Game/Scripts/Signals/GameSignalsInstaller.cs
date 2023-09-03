using Zenject;

namespace Game.Scripts.Signals
{
    public class GameSignalsInstaller : Installer<GameSignalsInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
           
            Container.DeclareSignal<LevelEndSignal>();
            Container.DeclareSignal<TileClickedSignal>();
            Container.DeclareSignal<SubmitButtonClickedSignal>();
            Container.DeclareSignal<UndoButtonClickedSignal>();
            Container.DeclareSignal<WordSubmittedSignal>();
            Container.DeclareSignal<TileRemovedFromSubmittedSignal>();
        }
    }
}
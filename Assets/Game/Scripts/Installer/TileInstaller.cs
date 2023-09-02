using Game.Scripts.Tile;
using Zenject;

namespace Game.Scripts.Enemy
{
    public class TileInstaller : Installer<TileInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TileStateManager>().AsSingle();

            Container.Bind<TileDisableState>().AsSingle();
            Container.Bind<TileEnableState>().AsSingle();
            Container.Bind<TileSelectedState>().AsSingle();
        }
    }
}
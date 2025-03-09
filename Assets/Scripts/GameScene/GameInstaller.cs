using Gameplay;
using Windows;
using Zenject;

namespace GameScene
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameplayController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<WindowsController>().FromComponentInHierarchy().AsSingle();
        }
    }
}
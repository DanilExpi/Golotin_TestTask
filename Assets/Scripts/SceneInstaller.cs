using Scripts.InventoryBase;
using UnityEngine;
using Zenject;

namespace Scripts
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private CameraManager _cameraManager;
        [SerializeField] private Player _player;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private ViewInventory _viewInventoryPrefab;

        public override void InstallBindings()
        {
            Container
                .Bind<GameManager>()
                .FromInstance(_gameManager)
                .AsSingle();

            Container
                .Bind<CameraManager>()
                .FromInstance(_cameraManager)
                .AsSingle();

            Container
                .Bind<Player>()
                .FromInstance(_player)
                .AsSingle();

            Container
                .Bind<Inventory>()
                .FromInstance(_inventory)
                .AsSingle();

            Container
                .Bind<UIManager>()
                .FromInstance(_uiManager)
                .AsSingle();

            Container.
                BindMemoryPool<ViewInventory, ViewInventoryPool>().
                FromComponentInNewPrefab(_viewInventoryPrefab);
        }
    }
}

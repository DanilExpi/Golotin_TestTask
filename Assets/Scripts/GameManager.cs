using Scripts.InventoryBase;
using UnityEngine;
using Zenject;

namespace Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Inventory _inventory;

        [Inject]
        private void Construct(Player player, Inventory inventory)
        {
            _player = player;
            _inventory = inventory;
        }

        private void Start()
        {
            _player.Enable();
        }

        public void MovePlayerOnFloor(Vector3 position)
        {
            _player.Move(position, null);
        }

        public void MoveToBuilding(Building building)
        {
            var gate = building.GetGatePosition;
            _player.Move(gate, () =>
            {
                var value = building.GetValue;
                if (value > 0)
                {
                    var type = building.GetBuildingTypeType;
                    _inventory.SetType(type, value);
                    building.DropValue();
                }
            });
        }
    }
}

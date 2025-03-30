using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Scripts.InventoryBase
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private int _maxShowViews = 4;
        [SerializeField] private RectTransform _forShow, _forHide;

        private Dictionary<BuildingType, int> _inventory = new();
        [SerializeField] private List<ViewInventory> _views = new();
        private ViewInventoryPool _viewInventoryPool;


        [Inject]
        private void Construct(ViewInventoryPool viewInventoryPool)
        {
            _viewInventoryPool = viewInventoryPool;
        }

        public void SetType(BuildingType type, int amount)
        {
            if (!_inventory.TryAdd(type, amount))
            {
                _inventory[type] += amount;
            }

            var allAmount = _inventory[type];
            var textToDisplay = $"{type}: +{amount} ({allAmount})";
            var view = _viewInventoryPool.Spawn();
            view.Init(textToDisplay, _forShow);
            _views.Add(view);
            while (_views.Count > _maxShowViews)
            {
                _views.First().FastHide();
                _views.RemoveAt(0);
            }
        }

        public void RemoveView(ViewInventory view)
        {
            _views.Remove(view);
            view.Hide();
            view.SetParent(_forHide);
            _viewInventoryPool.Despawn(view);
        }
    }
}

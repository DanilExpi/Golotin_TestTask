using System.Collections;
using UnityEngine;
using Zenject;

namespace Scripts
{
    public enum BuildingType
    {
        None,
        Wood,
        Metal,
        Meat,
        Water,
        Gold
    }

    public class Building : MonoBehaviour, IPressedDetected
    {
        [SerializeField] private Transform _gate;
        [SerializeField] private BuildingType _type;
        [SerializeField] private LogoBuilding _logo;
        [SerializeField] private float timeIncreaseValue = 1f;
        [SerializeField] private int stepIncreaseValue = 1;

        private GameManager _gameManager;

        private int Value { get; set; }

        public int GetValue => Value;

        public Vector3 GetGatePosition => _gate.position;
        public BuildingType GetBuildingTypeType => _type;

        [Inject]
        private void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void Pressed(Vector3 position)
        {
            _gameManager.MoveToBuilding(this);
        }

        private void Start()
        {
            _logo.Init(_type, Value);
            StartCoroutine(IncreaseValueCor());
        }

        private IEnumerator IncreaseValueCor()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeIncreaseValue);
                Value += stepIncreaseValue;
                _logo.SetValue(Value);
            }
        }

        public void DropValue()
        {
            Value = 0;
            _logo.SetValue(Value);
        }
    }
}

using TMPro;
using UnityEngine;

namespace Scripts
{
    public class LogoBuilding : MonoBehaviour
    {
        [SerializeField] private TextMeshPro titleText, valueText;

        public void Init(BuildingType type, int value)
        {
            titleText.text = type.ToString();
            SetValue(value);
        }

        public void SetValue(int value)
        {
            valueText.text = value.ToString();
        }
    }
}
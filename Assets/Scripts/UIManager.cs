using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Slider _sliderSound;
    
        public Slider GetSliderSound => _sliderSound;
    }
}

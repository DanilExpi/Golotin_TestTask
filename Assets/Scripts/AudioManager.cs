using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _factorVolume = 0.1f;

        private Slider _slider;

        private const string KeySound = "Sound";
        private const float DefaultVolume = 0.5f;

        private float _volume;

        private float Volume
        {
            get => _volume;
            set
            {
                PlayerPrefs.SetFloat(KeySound, value);
                SimpleSetVolume(value);
            }
        }

        [Inject]
        private void Construct(UIManager uiManager)
        {
            _slider = uiManager.GetSliderSound;
        }

        private void Start()
        {
            CheckSave();

            _slider.wholeNumbers = false;
            _slider.minValue = 0f;
            _slider.maxValue = 1f;
            _slider.value = Volume;

            _slider.onValueChanged.AddListener(ChangeVolume);
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveAllListeners();
        }

        private void ChangeVolume(float volume)
        {
            Volume = volume;
        }

        private void SimpleSetVolume(float volume)
        {
            _volume = volume;
            _audioSource.volume = volume * _factorVolume;
        }

        private void CheckSave()
        {
            if (PlayerPrefs.HasKey(KeySound))
            {
                var volume = PlayerPrefs.GetFloat(KeySound);
                SimpleSetVolume(volume);
            }
            else
            {
                Volume = DefaultVolume;
            }
        }
    }
}

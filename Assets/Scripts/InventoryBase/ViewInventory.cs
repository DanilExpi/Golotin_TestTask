using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace Scripts.InventoryBase
{
    public class ViewInventory : MonoBehaviour
    {
        [SerializeField] private float _speedAlpha = 2f;
        [SerializeField] private float _timeShow = 2f;
        [SerializeField] private CanvasGroup _group;
        [SerializeField] private TextMeshProUGUI _text;

        private const float MinAlpha = 0f;
        private const float MaxAlpha = 1f;

        private Coroutine _alphaCor;
        private Inventory _inventory;

        private float Alpha
        {
            get => _group.alpha;
            set => _group.alpha = value;
        }

        [Inject]
        private void Construct(Inventory inventory)
        {
            _inventory = inventory;
        }

        public void Init(string text, RectTransform parent)
        {
            gameObject.SetActive(true);
            Alpha = MinAlpha;
            _text.text = text;
            SetParent(parent);
            _alphaCor = StartCoroutine(ShowCor());
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetParent(RectTransform parent)
        {
            transform.SetParent(parent, false);
        }

        private IEnumerator ShowCor()
        {
            yield return ChangeAlphaCor(MaxAlpha);
            yield return new WaitForSeconds(_timeShow);
            yield return ChangeAlphaCor(MinAlpha);
            _alphaCor = null;
            RemoveInParent();
        }

        private IEnumerator ChangeAlphaCor(float alpha)
        {
            while (Alpha != alpha)
            {
                Alpha = Mathf.MoveTowards(Alpha, alpha, _speedAlpha * Time.deltaTime);
                yield return null;
            }
        }

        public void FastHide()
        {
            if (_alphaCor != null)
            {
                StopCoroutine(_alphaCor);
            }

            _alphaCor = StartCoroutine(FastHideCor());
        }

        private IEnumerator FastHideCor()
        {
            yield return ChangeAlphaCor(MinAlpha);
            _alphaCor = null;
            RemoveInParent();
        }

        private void RemoveInParent()
        {
            _inventory.RemoveView(this);
        }
    }

    public class ViewInventoryPool : MemoryPool<ViewInventory>
    {
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Scripts
{
    public interface IPressedDetected
    {
        void Pressed(Vector3 position);
    }

    public class PressedDetected : MonoBehaviour, IPointerClickHandler
    {
        private CameraManager _cameraManager;

        [Inject]
        private void Construct(CameraManager cameraManager)
        {
            _cameraManager = cameraManager;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.LogError("OnPointerClick PressedDetected");
            var position = eventData.position;

            var mainCamera = _cameraManager.GetMainCamera;

            var ray = mainCamera.ScreenPointToRay(position);
            if (Physics.Raycast(ray.origin, ray.direction, out var hit, Mathf.Infinity))
            {
                var iPressed = hit.collider.GetComponent<IPressedDetected>();
                if (iPressed != null)
                {
                    iPressed.Pressed(hit.point);
                }
            }
        }
    }
}

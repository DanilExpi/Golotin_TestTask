using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.InputSystem.Layouts;
using Zenject;

namespace Scripts
{
    public class TouchJoystick : OnScreenControl, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform controlRect;
        [SerializeField] private float joystickRadius;
        [SerializeField] private RectTransform joystick, joystickBounds;
        
        [InputControl(layout = "Vector2")]
        [SerializeField]
        private string m_ControlPath;
        
        
        private CameraManager _cameraManager;
        private bool beingTouched;
        private Vector2 initialTouchPosition;
        
        protected override string controlPathInternal
        {
            get => m_ControlPath;
            set => m_ControlPath = value;
        }


        [Inject]
        private void Construct(CameraManager cameraManager)
        {
            _cameraManager = cameraManager;
        }
        
        private void Update()
        {
            if (beingTouched)
            {
                if (Time.timeScale > 0)
                {
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(controlRect, Input.mousePosition, null, out var touchPosition);
                    UpdateTouch(touchPosition);
                }
                else
                {
                    EndTouch();
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            StartTouch(joystick.localPosition);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            EndTouch();
        }

        private void StartTouch(Vector2 touchPosition)
        {
            if (Time.timeScale > 0)
            {
                beingTouched = true;
                initialTouchPosition = touchPosition;
                joystick.localPosition = initialTouchPosition;
                joystickBounds.localPosition = initialTouchPosition;
                joystickBounds.sizeDelta = Vector2.one * joystickRadius * 2;
            }
        }

        private void UpdateTouch(Vector2 touchPosition)
        {
            Vector2 joystickDelta = (touchPosition - initialTouchPosition);
            Vector2 moveDirection = joystickDelta.normalized;
            joystick.localPosition = joystickDelta.magnitude > joystickRadius ? initialTouchPosition + moveDirection * joystickRadius : touchPosition;
            _cameraManager.MoveCamera(moveDirection);
        }

        private void EndTouch()
        {
            joystick.localPosition = joystickBounds.localPosition;
            beingTouched = false;
        }
    }
}

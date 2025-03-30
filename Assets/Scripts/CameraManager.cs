using UnityEngine;

namespace Scripts
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float _factorMove = 0.1f;

        private Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Camera GetMainCamera => mainCamera;

        public void MoveCamera(Vector2 offset)
        {
            offset *= _factorMove;
            var position = Position;
            position.x -= offset.y;
            position.z += offset.x;
            Position = position;
        }
    }
}

using UnityEngine;

namespace Scripts
{
    public class Floor : MonoBehaviour, IPressedDetected
    {
        [SerializeField] private GameManager _gameManager;

        public void Pressed(Vector3 position)
        {
            _gameManager.MovePlayerOnFloor(position);
        }
    }
}

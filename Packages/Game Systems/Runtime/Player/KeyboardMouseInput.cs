using UnityEngine;

namespace VSLikeGame.Player
{
    public sealed class KeyboardMouseInput : MonoBehaviour, IInputProvider
    {
        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }
        public bool DashPressed { get; private set; }
        public bool SkillPressed { get; private set; }

        [SerializeField] float lookSensitivity = 2f;

        void Update()
        {
            Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Look = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * lookSensitivity;

            DashPressed = Input.GetKeyDown(KeyCode.LeftShift);
            SkillPressed = Input.GetMouseButtonDown(0);
        }
    }
}

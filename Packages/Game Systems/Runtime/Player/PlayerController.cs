using UnityEngine;
using VSLikeGame.Core;

namespace VSLikeGame.Player
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class PlayerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Transform cameraPivot;
        [SerializeField] MonoBehaviour inputProviderBehaviour;

        [Header("Move")]
        [SerializeField] float moveSpeed = 6f;
        [SerializeField] float dashSpeed = 12f;
        [SerializeField] float dashDuration = 0.12f;
        [SerializeField] float gravity = -20f;

        [Header("Look")]
        [SerializeField] float pitchMin = -75f;
        [SerializeField] float pitchMax = 75f;

        CharacterController cc;
        IInputProvider input;
        bool inputLocked;
        float pitch;
        float dashT;
        float verticalVel;

        void Awake()
        {
            cc = GetComponent<CharacterController>();
            input = inputProviderBehaviour as IInputProvider;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void OnEnable()
        {
            GameEvents.OnInputLockChanged += HandleLock;
            GameEvents.OnPausedChanged += HandlePause;
        }

        void OnDisable()
        {
            GameEvents.OnInputLockChanged -= HandleLock;
            GameEvents.OnPausedChanged -= HandlePause;
        }

        void HandleLock(bool locked) => inputLocked = locked;

        void HandlePause(bool paused)
        {
            inputLocked = paused;
            Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = paused;
        }

        void Update()
        {
            if (input == null || inputLocked) return;

            TickLook();
            TickDash();
            TickMove();
        }

        void TickLook()
        {
            var look = input.Look;
            transform.Rotate(0f, look.x, 0f);

            pitch = Mathf.Clamp(pitch - look.y, pitchMin, pitchMax);
            if (cameraPivot) cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }

        void TickDash()
        {
            if (dashT > 0f) dashT -= Time.deltaTime;
            if (input.DashPressed) dashT = dashDuration;
        }

        void TickMove()
        {
            Vector3 move = new Vector3(input.Move.x, 0f, input.Move.y);
            move = Vector3.ClampMagnitude(move, 1f);
            move = transform.TransformDirection(move);

            float speed = dashT > 0f ? dashSpeed : moveSpeed;

            if (cc.isGrounded && verticalVel < 0f) verticalVel = -2f;
            verticalVel += gravity * Time.deltaTime;

            Vector3 velocity = move * speed;
            velocity.y = verticalVel;

            cc.Move(velocity * Time.deltaTime);
        }
    }
}

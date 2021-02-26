using DefaultNamespace;
using UnityEngine;

namespace Components.Move
{
    [RequireComponent(typeof(CharacterController))]
    public class ManualMove : MonoBehaviour
    {
        Transform _tr;
        CharacterController _characterController;

        public float Speed { get; set; }
        public float Direction { get; set; }
        public bool IsStopped { get; set; }

        public void Move(float direction)
        {
            Direction = direction;
            IsStopped = false;
        }

        void Awake()
        {
            _tr = transform;
            _characterController = GetComponent<CharacterController>();
        }

        void FixedUpdate()
        {
            if (!IsStopped)
            {
                _tr.rotation = Quaternion.Euler(0, Direction, 0);
                _characterController.SimpleMove(_tr.forward * (Speed * Constants.StepUnit * Time.fixedDeltaTime));
            }
            else
            {
                _characterController.SimpleMove(Vector3.zero);
            }
        }

        void OnEnable()
        {
            _characterController.enabled = true;
        }

        void OnDisable()
        {
            _characterController.enabled = false;
        }
    }
}
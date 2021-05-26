using UnityEngine;
using UnityEngine.AI;

namespace _Game.GameModules.Entities.Scripts
{
    public class EntityMovement : MonoBehaviour
    {
        Vector3 _destination;
        Transform _transform;
        [SerializeField] bool applyInputMovement;

        [SerializeField] NavMeshAgent agent;
        [SerializeField] Animator animator;

        bool _autoMove;

        public bool AutoMove
        {
            get => _autoMove;
            private set
            {
                _autoMove = value;
                if (!_autoMove || value) return;
                agent.velocity = Vector3.zero;
                agent.speed = 0;
            }
        }

        public float AnimationSpeed { get; private set; }
        public float InputSpeed { get; set; }
        public bool ApplyAnimationRootMovement { get; set; }

        public bool ApplyInputMovement
        {
            get => applyInputMovement;
            set
            {
                applyInputMovement = value;
                AutoMove = value;
            }
        }

        public float StoppingDistance
        {
            get => agent.stoppingDistance;
            set => agent.stoppingDistance = value;
        }

        public Vector3 Destination
        {
            get => agent.destination;
            set
            {
                agent.destination = value;
                AutoMove = true;
            }
        }

        public Quaternion Rotation
        {
            get => _transform.rotation;
            set
            {
                _transform.rotation = value;
                if (!AutoMove) return;
                AutoMove = false;
            }
        }

        void Start()
        {
            _transform = agent.transform;
        }

        void Update()
        {
            float speed = 0;
            if (ApplyAnimationRootMovement) speed += AnimationSpeed;
            if (ApplyInputMovement) speed += InputSpeed;
            agent.speed = speed;
            if (!AutoMove) agent.velocity = _transform.forward * speed;
        }

        void OnAnimatorMove()
        {
            AnimationSpeed = (animator.deltaPosition / Time.deltaTime).magnitude;
        }
    }
}
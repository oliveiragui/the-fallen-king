using UnityEngine;
using UnityEngine.AI;

namespace _Game.Scripts.GameContent.Entities.Components.PhysicsSystem
{
    public class EntityMovement : MonoBehaviour
    {
        [SerializeField] NavMeshAgent agent;
        Transform _transform;
        [SerializeField] bool autoMove;
        public bool canMove;

        #region Properties

        public Vector2 Velocity => agent.velocity;

        public bool IsMoving { get; private set; }

        public Quaternion Rotation { get; set; }

        public bool AutoMove
        {
            get => autoMove;
            set => autoMove = value;
        }

        public float Speed
        {
            get => agent.speed;
            set => agent.speed = value;
        }

        public float StoppingDistance
        {
            get => agent.stoppingDistance;
            set => agent.stoppingDistance = value;
        }

        public Vector3 Destination
        {
            get => agent.destination;
            set => agent.destination = value;
        }

        #endregion

        #region Methods

        public void Move(float speed, Quaternion rotation)
        {
            Rotation = rotation;
            Speed = speed;
        }

        public void Move(Quaternion rotation)
        {
            Rotation = rotation;
        }

        public void MoveTo(Vector3 destination, float speed, float stoppingDistance)
        {
            if (!AutoMove) return;
            Speed = speed;
            StoppingDistance = stoppingDistance;
            Destination = destination;
        }

        public void MoveTo(Vector3 destination, float speed)
        {
            if (!AutoMove) return;
            Speed = speed;
            Destination = destination;
        }

        public void MoveTo(Vector3 destination)
        {
            if (!AutoMove) return;
            Destination = destination;
        }

        public void Stop()
        {
            agent.velocity = Vector3.zero;
            Speed = 0;
        }

        #endregion

        #region Loop Functions

        void Start()
        {
            _transform = agent.transform;
        }

        void Update()
        {
            if (!canMove) return;
            IsMoving = agent.velocity.sqrMagnitude > 0.01f;
            UpdatePostionAndRotation();
        }

        void UpdatePostionAndRotation()
        {
            if (AutoMove) return;
            _transform.rotation = Rotation;
            agent.velocity = _transform.forward * Speed;
        }

        #endregion
    }
}
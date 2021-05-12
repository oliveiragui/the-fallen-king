using UnityEngine;
using UnityEngine.AI;

namespace _Game.GameModules.Entities.Scripts.Components.Movement
{
    public class EntityMovement : MonoBehaviour
    {
        [SerializeField] NavMeshAgent agent;
        [SerializeField] bool _autoMove;

        Quaternion _rotation;
        Transform _transform;

        #region Properties

        public bool IsMoving { get; private set; }

        public Quaternion Rotation
        {
            get => _rotation;
            set
            {
                _autoMove = false;
                _rotation = value;
            }
        }

        public Vector2 Velocity
        {
            get => agent.velocity;
            set
            {
                _autoMove = false;
                agent.velocity = value;
            }
        }

        public bool AutoMove
        {
            get => _autoMove;
            set => _autoMove = value;
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
            set
            {
                _autoMove = true;
                agent.destination = value;
            }
        }

        public void Stop()
        {
            _autoMove = false;
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
            IsMoving = agent.velocity.sqrMagnitude > 0.01f;
            if (!AutoMove) UpdatePostionAndRotation();
        }

        void UpdatePostionAndRotation()
        {
            _transform.rotation = Rotation;
            agent.velocity = _transform.forward * Speed;
        }

        #endregion
    }
}
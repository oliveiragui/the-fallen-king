using System;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Movement
{
    public class EntityMovement : MonoBehaviour
    {
        float _speed;
        float _direction;
        Transform _tr;

        [SerializeField] bool autoMovement;
        [SerializeField] NavMeshAgent navMeshAgent;
        [SerializeField] CharacterController characterController;

        public bool IsMoving => Speed > 0.1f || navMeshAgent.velocity.magnitude > 0.1f;

        public float Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                navMeshAgent.speed = _speed;
            }
        }

        public float Direction
        {
            get => _direction;
            set => _direction = value;
        }

        public float StoppingDistance
        {
            get => navMeshAgent.stoppingDistance;
            set => navMeshAgent.stoppingDistance = value;
        }

        public bool AutoMovement
        {
            get => autoMovement;
            set
            {
                autoMovement = value;
                navMeshAgent.enabled = value;
                if (value) navMeshAgent.isStopped = false;
                characterController.enabled = !value;
            }
        }

        public void Move()
        {
            if (AutoMovement) AutoMovement = false;
            _tr.rotation = Quaternion.Euler(0, Direction, 0);
            characterController.SimpleMove(_tr.forward * (Speed * Constants.StepUnit * Time.fixedDeltaTime));
        }

        public void Move(float speed, float direction)
        {
            Speed = speed;
            Direction = direction;
            Move();
        }

        public void MoveTo(Vector3 endPoint, float speed, float stoppingDistance)
        {
            Speed = speed;
            StoppingDistance = stoppingDistance;
            MoveTo(endPoint);
        }

        public void MoveTo(Vector3 endPoint)
        {
            if (!AutoMovement) AutoMovement = true;
            navMeshAgent.destination = endPoint;
        }

        public void Stop()
        {
            navMeshAgent.velocity = Vector3.zero;
            Speed = 0;
            if (AutoMovement) AutoMovement = false;
        }

        void Start()
        {
            _tr = characterController.transform;
            AutoMovement = autoMovement;
        }
        
    }
}
using UnityEngine;
using UnityEngine.AI;

namespace _Game.GameModules.Entities.Scripts
{
    public class EntityMovement : MonoBehaviour
    {
        [SerializeField] NavMeshAgent agent;
        [SerializeField] Animator animator;
        [SerializeField] bool autoMove;

        Transform _transform;

        public bool AutoMove
        {
            get => autoMove;
            set => autoMove = value;
        }

        [field: SerializeField] public bool ApplyAnimationRootMovement { get; set; }
        public bool Walking { get; set; }

        public float AnimationSpeed { get; private set; }
        public float TotalSpeed { get; private set; }
        public float Speed { get; set; }
        public float StoppingDistance { get; set; }
        public Vector3 Destination { get; set; }
        public Quaternion Rotation { get; set; }

        public void Walk(Quaternion direction)
         {
             Walking = true;
             Rotation = direction;
         }

        public void WalkTo(Vector3 destination)
        {
            Walking = true;
            Destination = destination;
        }

        public void StopWalking()
        {
            Walking = false;
            agent.velocity = Vector3.zero;
            Speed = 0;
        }

        void Start()
        {
            _transform = agent.transform;
        }

        void Update()
        {
            //if (receivingForce) UpdateForce();
            TotalSpeed = 0;
            agent.stoppingDistance = StoppingDistance;
            if (ApplyAnimationRootMovement) TotalSpeed += AnimationSpeed;
            if (Walking)
            {
                TotalSpeed += Speed;
                if (AutoMove) agent.destination = Destination;
                else _transform.rotation = Rotation;
            }

  
            agent.speed = TotalSpeed;

            if (Walking && !AutoMove || AnimationIsMoving) agent.velocity = _transform.forward * TotalSpeed;
        }

        bool AnimationIsMoving => AnimationSpeed > 0.1;

        void OnAnimatorMove()
        {
            AnimationSpeed = (animator.deltaPosition / Time.deltaTime).magnitude;
        }
    }
}
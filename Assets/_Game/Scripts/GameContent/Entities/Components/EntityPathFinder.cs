using UnityEngine;
using UnityEngine.AI;

namespace _Game.Scripts.Entities.Components
{
    public class EntityPathFinder : MonoBehaviour
    {
        [SerializeField] bool autoMovement;
        [SerializeField] NavMeshAgent navMeshAgent;
        
        public Vector3 Position { get; private set; }
        public Quaternion Rotation { get; private set; }

        void Awake()
        {
            navMeshAgent.updatePosition = false;
            navMeshAgent.updateRotation = false;
        }
        
        

        void Start()
        {
            AutoMovement = autoMovement;
        }

        void FixedUpdate()
        {
            if (!navMeshAgent.hasPath) return;
            Rotation = Quaternion.Euler(navMeshAgent.nextPosition - Position);
            navMeshAgent.nextPosition = Position;
        }

        public bool IsMoving => navMeshAgent.speed > 0.1f || navMeshAgent.velocity.magnitude > 0.1f;

        public bool AutoMovement
        {
            get => autoMovement;
            set
            {
                autoMovement = value;
                navMeshAgent.enabled = value;
                if (value) navMeshAgent.isStopped = false;
            }
        }

        public void MoveTo(Vector3 endPoint, float speed, float stoppingDistance)
        {
            navMeshAgent.speed = speed;
            navMeshAgent.stoppingDistance = stoppingDistance;
            if (!AutoMovement) AutoMovement = true;
            navMeshAgent.destination = endPoint;
        }

        public void Stop()
        {
            navMeshAgent.velocity = Vector3.zero;
            navMeshAgent.speed = 0;
            if (AutoMovement) AutoMovement = false;
        }
    }
}
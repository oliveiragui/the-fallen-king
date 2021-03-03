using UnityEngine;
using UnityEngine.AI;

namespace Components.Move
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AutoMove : MonoBehaviour
    {
        bool _isStopped;
        NavMeshAgent navMashAgent;

        public float Speed
        {
            get => navMashAgent.speed;
            set => navMashAgent.speed = value;
        }

        public float StoppingDistance
        {
            get => navMashAgent.stoppingDistance;
            set => navMashAgent.stoppingDistance = value;
        }

        public bool IsStopped
        {
            get => _isStopped;
            set
            {
                if (value)
                {
                    navMashAgent.velocity = Vector3.zero;
                    navMashAgent.isStopped = true;
                    _isStopped = true;
                }
                else
                {
                    navMashAgent.isStopped = false;
                    _isStopped = false;
                }
            }
        }

        void Awake()
        {
            navMashAgent = GetComponent<NavMeshAgent>();
        }

        void OnEnable()
        {
            navMashAgent.enabled = true;
        }

        void OnDisable()
        {
            navMashAgent.enabled = false;
        }

        public void MoveTo(Vector3 endPoint)
        {
            navMashAgent.isStopped = false;
            navMashAgent.destination = endPoint;
        }

        public void MoveTo(Vector3 endPoint, float speed)
        {
            Speed = speed;
            MoveTo(endPoint);
        }

        public void MoveTo(Vector3 endPoint, float speed, float stoppingDistance)
        {
            StoppingDistance = stoppingDistance;
            MoveTo(endPoint, speed);
        }
    }
}
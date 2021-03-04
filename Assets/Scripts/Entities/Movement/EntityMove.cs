using Components.Move;
using UnityEngine;

namespace Entities.Movement
{
    public class EntityMove : MonoBehaviour
    {
        [SerializeField] AutoMove autoMove;
        [SerializeField] ManualMove manualMove;

        float _speed;

        bool auto;

        public float Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                autoMove.Speed = _speed;
                manualMove.Speed = _speed;
            }
        }

        public float StoppingDistance
        {
            get => autoMove.StoppingDistance;
            set => autoMove.StoppingDistance = value;
        }

        void Awake()
        {
            AutoMovement(auto);
        }

        void AutoMovement(bool value)
        {
            if (value)
            {
                SetAuto(true);
                autoMove.IsStopped = true;
            }
            else
            {
                SetAuto(false);
                manualMove.IsStopped = true;
            }
        }

        void SetAuto(bool value)
        {
            autoMove.enabled = value;
            manualMove.enabled = !value;
        }

        public void Move(float direction)
        {
            if (auto)
            {
                AutoMovement(false);
                auto = false;
                manualMove.IsStopped = false;
            }

            manualMove.Move(direction);
        }

        public void MoveTo(Vector3 endPoint)
        {
            if (!auto)
            {
                AutoMovement(true);
                auto = true;
            }

            autoMove.MoveTo(endPoint);
        }

        public void Stop()
        {
            if (auto) autoMove.IsStopped = true;
            else manualMove.IsStopped = true;
        }
    }
}
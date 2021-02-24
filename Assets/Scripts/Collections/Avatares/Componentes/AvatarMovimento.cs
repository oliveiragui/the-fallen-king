using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Collections.Avatares.Componentes
{
    public class AvatarMovimento : MonoBehaviour
    {
        [SerializeField] NavMeshAgent navMashAgent;
        [SerializeField] CharacterController charController;
        Coroutine _coroutine;
        Vector3 _pontoFinal;
        Transform _tr;
        public bool Parado { get; private set; }

        bool UsaMesh
        {
            get => navMashAgent.enabled;
            set
            {
                navMashAgent.enabled = value;
                charController.enabled = !value;
            }
        }

        void Start()
        {
            _tr = transform;
            navMashAgent.isStopped = true;
            navMashAgent.updatePosition = false;
            navMashAgent.updateRotation = false;
        }

        public void MovimentoSimples(float direcao, float velocidade)
        {
            if (UsaMesh)
            {
                ParaMovimento();
                UsaMesh = false;
            }

            float veloc = velocidade;
            _tr.rotation = Quaternion.Euler(0, direcao, 0);
            charController.SimpleMove(veloc * _tr.forward);
        }

        #region NavMeshAgent

        public void MoveAte(Vector3 pontoFinal, float velocidade, float distanciaParada)
        {
            if (!UsaMesh) UsaMesh = true;
            else ParaMovimento();

            StartCoroutine(DeslocaAtePonto(pontoFinal, velocidade, distanciaParada));
        }

        public void MudaPontoFinal(Vector3 ponto)
        {
            _pontoFinal = ponto;
        }

        public void ParaMovimento()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                ParaMash();
            }
        }

        IEnumerator DeslocaAtePonto(Vector3 pontoFinal, float speed, float distanciaParada)
        {
            _pontoFinal = pontoFinal;
            navMashAgent.speed = speed;
            navMashAgent.stoppingDistance = distanciaParada;
            navMashAgent.isStopped = false;

            while (navMashAgent.remainingDistance < navMashAgent.stoppingDistance)
            {
                navMashAgent.SetDestination(_pontoFinal);
                transform.LookAt(navMashAgent.nextPosition);
                transform.position = navMashAgent.nextPosition;
                yield return new WaitForFixedUpdate();
            }

            ParaMash();
        }

        void ParaMash()
        {
            navMashAgent.velocity = Vector3.zero;
            navMashAgent.isStopped = true;
        }

        #endregion
    }
}
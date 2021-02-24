using Collections.Avatares.Componentes;
using Collections.Entidades;
using Collections.Habilidades;
using Components.Move;
using UnityEngine;

namespace Collections.Avatares
{
    public class AvatarController : MonoBehaviour
    {
        //[SerializeField] SmartMove movement;
        [SerializeField] public Entidade entidade;

        AvatarSateMachine _sateMachine;

        public HabilidadeController HabilidadeAtual;

        public AvatarComando Comando { get; private set; }
        public AvatarParams Parametros { get; private set; }
        public AvatarAnimacao Animacao { get; private set; }
        public AvatarMesh Mesh { get; private set; }

        [field: SerializeField] public AvatarModel Modelo { get; private set; }
        [field: SerializeField] public GameObject Actions { get; private set; }
        [field: SerializeField] public AvatarAudio Audio { get; private set; }
        [field: SerializeField] public ParticulasAvatar Particulas { get; private set; }
        [field: SerializeField] public AvatarMovimento Movimentacao { get; private set; }
        [field: SerializeField] public GameObject Colisores { get; private set; }

        void Start()
        {
            Parametros = new AvatarParams(this);
            Comando = new AvatarComando(this);
            Movimentacao = GetComponent<AvatarMovimento>();

            test();
            var animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = Mesh.animationController;
            animator.avatar = Mesh.avatar;
            Animacao = new AvatarAnimacao(animator);

            _sateMachine = gameObject.AddComponent<AvatarSateMachine>();

            Configura();
        }

        public void Configura()
        {
            entidade.Bainha.AoMudarArma.AddListener(item => TrocaArma(entidade));
            TrocaArma(entidade);
        }

        public void test()
        {
            Mesh = GetComponentInChildren<AvatarMesh>();
            if (Mesh != null) return;
            Mesh = Modelo.InstantiateAvatarMesh(transform).GetComponent<AvatarMesh>();
        }

        public void TrocaArma(Entidade entidade)
        {
            if (entidade is null || entidade.Bainha.EmUso == null) return;
            Animacao.TrocaArma(entidade.Bainha.EmUso.Modelo.AnimationID);
            Mesh.TrocaArma(entidade.Bainha.EmUso);
            _sateMachine.Configura(this);
        }
    }
}
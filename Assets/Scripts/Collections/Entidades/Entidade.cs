using Collections.Armas;
using Collections.Avatares;
using Collections.Entidades.Utils;
using Collections.Equipes;
using Collections.Habilidades;
using ToRefactor.Armazenamento;
using UnityEngine;

namespace Collections.Entidades
{
    public class Entidade : MonoBehaviour
    {
        public StatusEntidade status;
        [field: SerializeField] public EntidadeModel Modelo { get; private set; }
        [field: SerializeField] public AvatarController Avatar { get; private set; }

        public Equipe Equipe { get; private set; }
        public Bainha Bainha { get; private set; }

        public Inventario<IArmazenavel> Inventario { get; private set; }

        //public EntidadeStatus statis;
        public HabilidadeController[] Habilidades { get; private set; }

        void Awake()
        {
            status = new StatusEntidade(10, 2, 300);
            //statis = gameObject.AddComponent<EntidadeStatus>().Configura(Modelo.AtributosBase);
            Equipe = new Equipe(Modelo.Equipe);
            Inventario = new Inventario<IArmazenavel>(15);
            Bainha = new Bainha(15, 2);

            Bainha.AoMudarArma.AddListener(item => InicializaHabilidades(item));
            DEV_ITEM();
        }

        public void DEV_ITEM()
        {
            var arma = new ArmaController(Modelo.ArmaInicial);
            Bainha.AdicionaArma(arma);
            Bainha.Equipa(arma, 0);
            Bainha.UsaArma(0);
        }

        public void InicializaHabilidades(ArmaController arma)
        {
            if (arma == null) return;

            Habilidades = new HabilidadeController[4]
            {
                new HabilidadeController(arma.Modelo.Ataque1),
                new HabilidadeController(arma.Modelo.Ataque2),
                new HabilidadeController(arma.Modelo.Ataque3),
                new HabilidadeController(arma.Modelo.Esquiva)
            };
        }
    }
}
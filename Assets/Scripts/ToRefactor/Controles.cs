using Collections.Controles;
using UnityEngine;
using Utils;

namespace ToRefactor
{
    public class Controles : Singleton<Controles>
    {
        [SerializeField] ControleModelo[] modelos;

        public ControleModelo[] Modelos => modelos;
    }
}
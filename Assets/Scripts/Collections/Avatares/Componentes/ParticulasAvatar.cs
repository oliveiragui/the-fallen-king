using UnityEngine;

namespace Collections.Avatares.Componentes
{
    public class ParticulasAvatar : MonoBehaviour
    {
        public ParticleSystem ImpactoNoChao;
        public ParticleSystem HitDeSoco;
        public ParticleSystem HitDeEspada;
        public ParticleSystem Sangue;

        public void TocaParticulasDeImpacto()
        {
            if (!ImpactoNoChao.isPlaying) ImpactoNoChao.Play();
        }

        public void TocaParticulasDeHitDeSoco()
        {
            if (!HitDeSoco.isPlaying) HitDeSoco.Play();
        }

        public void TocaParticulasDeHitDeEspada()
        {
            if (!HitDeEspada.isPlaying) HitDeEspada.Play();
        }

        public void TocaParticulasDeSangue()
        {
            if (!Sangue.isPlaying) Sangue.Play();
        }
    }
}
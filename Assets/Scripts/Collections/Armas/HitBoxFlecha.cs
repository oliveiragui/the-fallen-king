using System.Collections;
using Collections.Avatares.Componentes;
using ToRefactor.Interagiveis;
using UnityEngine;

namespace Collections.Armas
{
    public class HitBoxFlecha : MonoBehaviour
    {
        public bool aliado;

        void Start()
        {
            StartCoroutine(DestroiAposAlgumTempo(4));
        }

        void OnTriggerEnter(Collider other)
        {
            if (!other.tag.Equals("Atingivel")) return;
            var avatar = other.GetComponent<Atingivel>().atingivel;

            if (avatar.entidade.Equipe.aliado ^ !aliado) return;

            avatar.Comando.Dano(2);
            avatar.Audio.TocaSom(SlotSom.HitDeFlecha);
            avatar.Particulas.TocaParticulasDeSangue();
            avatar.Particulas.TocaParticulasDeHitDeSoco();
            Destroy(gameObject);
            //Instantiate(Municoes.Instance.Flecha, transform.position, transform.rotation, other.transform);
        }

        IEnumerator DestroiAposAlgumTempo(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            Destroy(gameObject);
        }
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    public class Cronometro
    {
        readonly MonoBehaviour _monoBehaviour;
        Coroutine _corrotina;
        float _tempoCorrido;

        public Cronometro(MonoBehaviour monoBehaviour)
        {
            _monoBehaviour = monoBehaviour;
            AoZerarTempo = new UnityEvent();
            AoAtualizaCronometro = new AtualizaCronometroEvent();
        }

        public float TempoCorrido
        {
            get => _tempoCorrido;
            private set => _tempoCorrido = value < Duracao ? value : Duracao;
        }

        public float TempoRestante => Duracao - TempoCorrido;

        public AtualizaCronometroEvent AoAtualizaCronometro { get; }
        public UnityEvent AoZerarTempo { get; }

        public float Duracao { get; private set; }

        public bool Pausado { get; private set; }

        public void Inicia(float duracao)
        {
            Duracao = duracao;
            ZeraTempo();
            ParaCorrotina();
            _corrotina = _monoBehaviour.StartCoroutine(Temporizador());
        }

        public void Pausa()
        {
            if (Pausado) return;
            ParaCorrotina();
            Pausado = true;
        }

        public void Resume()
        {
            if (Pausado == false) return;

            if (_corrotina != null) ParaCorrotina();

            _corrotina = _monoBehaviour.StartCoroutine(Temporizador());
            Pausado = false;
        }

        public void Finaliza()
        {
            ParaCorrotina();
            ZeraTempo();
            AoZerarTempo.Invoke();
        }

        public void Reseta()
        {
            ParaCorrotina();
            ZeraTempo();
        }

        IEnumerator Temporizador()
        {
            yield return new WaitUntil(() =>
            {
                TempoCorrido += Time.deltaTime;
                AoAtualizaCronometro.Invoke(TempoCorrido);
                return TempoCorrido >= Duracao;
            });
            ZeraTempo();
            AoZerarTempo.Invoke();
        }

        void ZeraTempo()
        {
            TempoCorrido = 0;
            Pausado = false;
        }

        void ParaCorrotina()
        {
            if (_corrotina != null) _monoBehaviour.StopCoroutine(_corrotina);
        }
    }

    public class AtualizaCronometroEvent : UnityEvent<float> { }
}
using System.Collections;
using UnityEngine;

namespace Collections.Armas
{
    public class Flecha : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(DestroiAposAlgumTempo(4));
        }

        IEnumerator DestroiAposAlgumTempo(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            Destroy(gameObject);
        }
    }
}
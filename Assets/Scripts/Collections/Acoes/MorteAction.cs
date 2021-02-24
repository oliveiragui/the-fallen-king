using System.Collections;
using UnityEngine;

namespace Collections.Acoes
{
    public class MorteAction : AvatarAction
    {
        public override void OnStateEntered()
        {
            base.OnStateEntered();
            Avatar.Animacao.Morre();
            Avatar.StartCoroutine(AcaoMorre());
        }

        IEnumerator AcaoMorre()
        {
            yield return new WaitForSeconds(2.3f);
            Destroy(Avatar.transform.parent.parent.gameObject);
        }
    }
}
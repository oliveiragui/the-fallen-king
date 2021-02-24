using Collections.Controles.Sprites;
using Collections.Controles.Utils;
using UnityEngine;

namespace Collections.Controles
{
    [CreateAssetMenu(fileName = "Esquema SpriteText", menuName = "GameContent/Controles", order = 1)]
    public class ControleModelo : ScriptableObject
    {
        [field: SerializeField] public string Nome { get; private set; }
        [field: SerializeField] public TipoDeControle Tipo { get; private set; }
        [field: SerializeField] public SpriteBotao PerfilRichText { get; private set; }
    }
}
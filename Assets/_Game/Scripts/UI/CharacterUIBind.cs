using _Game.Scripts.Characters;
using _Game.Scripts.UI.StatusBar;
using UnityEngine;

namespace _Game.Scripts.UI
{
    public class CharacterUIBind : MonoBehaviour
    {
        [SerializeField] Character character;

        [SerializeField] Lifebar lifebar;
        [SerializeField] Vector3 statusBarPosOffset;

        // Start is called before the first frame update
        void Start()
        {
            character.Status.onAnyStatChanged.AddListener(status =>
            {
                lifebar.Total = status.Life.Total;
                lifebar.Current = status.Life.Current;
            });
        }

        // Update is called once per frame
        void Update() { }

        void FixedUpdate()
        {
            lifebar.transform.position = character.Entity.transform.position + statusBarPosOffset;
        }
    }
}
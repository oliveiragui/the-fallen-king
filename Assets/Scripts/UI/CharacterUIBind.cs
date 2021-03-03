using Characters;
using UI.StatusBar;
using UnityEngine;

namespace UI
{
    public class CharacterUIBind : MonoBehaviour
    {
        [SerializeField] Character character;

        [SerializeField] StatusBarManager statusBarManager;
        [SerializeField] Vector3 statusBarPosOffset;

        // Start is called before the first frame update
        void Start()
        {
            character.Status.onAnyStatChanged.AddListener(status =>
            {
                statusBarManager.Total = status.Life.Total;
                statusBarManager.Current = status.Life.Current;
            });
        }

        // Update is called once per frame
        void Update() { }

        void FixedUpdate()
        {
            statusBarManager.transform.position = character.Entity.transform.position + statusBarPosOffset;
        }
    }
}
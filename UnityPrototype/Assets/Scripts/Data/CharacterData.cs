using UnityEngine;

namespace StorageWars.Data
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "StorageWars/Character")]
    public class CharacterData : ScriptableObject
    {
        public string characterName;
        [TextArea] public string passiveDescription;
        [TextArea] public string activeDescription;
        public Sprite portrait;
    }
}

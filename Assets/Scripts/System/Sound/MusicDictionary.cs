using UnityEngine;

namespace FarmJam2026
{
    [CreateAssetMenu(fileName = "MusicDico", menuName = "Data/Sound Music Dico")]
    public class MusicDictionary : SoundDictionary<ESoundMusic>
    {
        private void OnValidate()
        {
            Validate();
        }
    }
}

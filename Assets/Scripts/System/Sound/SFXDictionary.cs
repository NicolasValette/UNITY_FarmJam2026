using UnityEngine;

namespace FarmJam2026
{
    [CreateAssetMenu(fileName = "SFXDico", menuName = "Data/Sound SFX Dico")]
    public class SFXDictionary : SoundDictionary<ESoundSFX>
    {
        private void OnValidate()
        {
            Validate();
        }
    }
}

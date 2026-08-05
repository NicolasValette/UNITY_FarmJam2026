using UnityEngine;

namespace FarmJam2026
{
    //[CreateAssetMenu(fileName = "NonoChampiName", menuName = "Data/Nono Champi Name")]
    public class NonoChampiName : ScriptableObject
    {
        [ChampiName]
        public string NonoString = string.Empty;
    }
}

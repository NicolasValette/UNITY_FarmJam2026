using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Events;

namespace FarmJam2026
{
    [System.Serializable]
    public class HarvestEvent : UnityEvent<List<Spore>>
    {

    }
}

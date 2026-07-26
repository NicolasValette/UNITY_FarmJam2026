using System;
using System.Collections.Generic;
using UnityEngine;

namespace FarmJam2026
{
    [Serializable]
    [CreateAssetMenu(fileName = "genome", menuName = "Data/Genome")]
    public class Genome : ScriptableObject
    {
        /// <summary>
        /// ADN is composed of several genes. (Genes may be composed of several genes).
        /// </summary>
        [SerializeReference]
        public List<IGene> Genes = new List<IGene>();
    }
}

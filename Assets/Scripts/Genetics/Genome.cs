using System;
using System.Collections.Generic;

namespace FarmJam2026
{
    [Serializable]
    public class Genome
    {
        /// <summary>
        /// ADN is composed of several genes. (Genes may be composed of several genes).
        /// </summary>
        public List<IGene> Genes;
    }
}

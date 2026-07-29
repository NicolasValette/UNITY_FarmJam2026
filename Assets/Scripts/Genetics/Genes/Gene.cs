using System;
using System.Collections.Generic;

namespace FarmJam2026
{
    public interface IGene
    {
        /// <summary>
        /// Apply the gene on the mushroom.
        /// </summary>
        /// <param name="mushroom"></param>
        public virtual void ExpressOn(Mushroom mushroom)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// aka "run the blender on this gene".
        /// </summary>
        /// <param name="genomes"></param>
        public virtual void PerformHybridization(List<GenomeData> genomes)
        {
            throw new NotImplementedException();
        }
    }
}

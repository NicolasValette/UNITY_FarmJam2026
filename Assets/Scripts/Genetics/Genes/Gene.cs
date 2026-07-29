using System.Collections.Generic;

namespace FarmJam2026
{
    public interface IGene
    {
        /// <summary>
        /// Apply the gene on the mushroom.
        /// </summary>
        /// <param name="mushroom"></param>
        void ExpressOn(Mushroom mushroom);

        /// <summary>
        /// aka "run the blender on this gene".
        /// </summary>
        /// <param name="genomes"></param>
        void PerformHybridization(List<Genome> genomes);
    }
}

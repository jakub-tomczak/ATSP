
using System.Collections;
using System.Collections.Generic;
using System;

namespace ATSP.Metrics
{
    public class PairsComparator: ISolutionComparator
    {
        public double CompareSolutions(uint[] solution1, uint[] solution2)
        {
            if(solution1.Length != solution2.Length)
            {
                throw new ArgumentException("Lengths of the arrays are not the same!");
            }

            var length = solution1.Length;
            var pairs1 = new HashSet<Tuple<uint, uint>>();
            var pairs2 = new HashSet<Tuple<uint, uint>>();
            for(var i  = 0; i < length;i++)
            {
                pairs1.Add(Tuple.Create(solution1[i], solution1[(i+1)%length]));
                pairs2.Add(Tuple.Create(solution2[i], solution2[(i+1)%length]));
            }
            pairs1.IntersectWith(pairs2);

            return (double)pairs1.Count / length;
        }
    }

}
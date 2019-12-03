using System.Collections;
using System.Collections.Generic;
using System;

namespace ATSP.Metrics
{
    public class NeighboursComparator: ISolutionComparator
    {
        public double CompareSolutions(uint[] solution1, uint[] solution2)
        {
            var matches = 0;
            var checks = 0;

            if(solution1.Length != solution2.Length)
            {
                throw new ArgumentException("Lengths of the arrays are not the same!");
            }
            var length = solution1.Length;

            for(var i = 0; i < length;i++)
            {
                for(var j = 0;j < length;j++)
                {
                    if(solution1[i] == solution2[j])
                    {
                        checks += 2;
                        if(solution1[(i+length - 1)%length] == solution2[(j + length - 1)%length])
                            matches++;
                        if(solution1[(i+1)%length] == solution2[(j+1)%length])
                            matches++;
                    }
                }
            }

            return (double)matches / checks;
        }
    }

}
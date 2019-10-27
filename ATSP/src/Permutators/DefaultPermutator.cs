namespace ATSP.Permutators
{
    public class DefaultPermutator : Permutator
    {
        public override void Permutate(uint [] array)
        {
            for(int i=array.Length-1;i>0;i--)
            {
                var swapIndex = randomizer.Next(i);
                swapper.Swap(array, ref swapIndex, ref i);
            }
        }
    }
}
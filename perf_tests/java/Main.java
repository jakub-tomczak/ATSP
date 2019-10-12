import java.util.Random;

public class Main
{
    private static void swap(int []array, int firstIndex, int secondIndex)
    {
        int temp = array[firstIndex];
        array[firstIndex] = array[secondIndex];
        array[secondIndex] = temp;
    }

    public static void main(String[] args)
    {
        int arraySize = 10000000;
        int []array = new int[arraySize];
        int minIterations = 10;
        long timeout = 1;
        long totalTime = 0;
        Random generator = new Random();

        for(int i=0;i<arraySize;i++)
        {
            array[i] = i;
        }

        long iterations = 0;
        do {
            long startTime = System.currentTimeMillis();
            for(int i=arraySize-1;i>0;i--)
            {
                int swapIndex = generator.nextInt(i);
                swap(array, i, swapIndex);
            }
            long endTime = System.currentTimeMillis();
            totalTime += endTime - startTime;
            iterations++;
        } while (totalTime < timeout * 1000 || iterations < minIterations);

        System.out.println("Total time is " + totalTime + " iterations " + iterations);
    }
}
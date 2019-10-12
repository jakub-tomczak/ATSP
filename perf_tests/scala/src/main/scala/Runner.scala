import scala.util.Random

class Runner {
    def run(): Unit = {
        indices.indices.foreach(x => indices(x) = x)
        var iterations = 0
        do {
            val start = System.nanoTime()
            for(i <- indices.length - 1 to 1 by -1)
            {
                val swapIndex = randomizer.nextInt(i)
                swap(indices, i, swapIndex)
            }
//            indices.indices.take(indices.length-1).foreach(x => {
//                var swapIndex = randomizer.nextInt(indices.length - x - 1)
//                swapIndex += x+1
//                swap(indices, swapIndex, x)
//            })
            val end = System.nanoTime()
            iterations += 1
            totalTime += (end - start) / 1e6
        } while (totalTime < Runner.timeout || iterations < Runner.minIterations)
        println(s"total mean time is ${totalTime / iterations}, iterations $iterations")
    }

    def swap(array: Array[Int], first: Int, second: Int): Unit = {
        val tmp = array(first)
        array(first) = array(second)
        array(second) = tmp
    }

    val randomizer: Random = new Random()
    val indices: Array[Int] = new Array[Int](Runner.arraySize)
    var totalTime: Double = 0.0
}

object Runner {
    private final val timeout = 1000; // ms
    private final val minIterations = 10;
    private final val arraySize = 10000000;
}
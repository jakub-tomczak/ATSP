function swap(arr, x, y)
    tmp=arr[x]
    arr[x]=arr[y]
    arr[y]=tmp
end

function permutate()
    arrSize = 10000000
    timeout = 1
    minIterations = 10
    indices = Vector{UInt}(undef, arrSize)

    for i=1:arrSize
        indices[i]=i
    end

    totalTime = 0.0
    iterations = 0
    while totalTime < timeout * 1000 || iterations < minIterations
        startTime = time_ns()
        i = arrSize
        swaps = broadcast(+, broadcast(%, rand(UInt, arrSize-1), arrSize), 1)
        for swapIndex in swaps
            swap(indices, swapIndex, i)
            i-=1
        end
        endTime = time_ns()
        iterations+=1
        time = (endTime-startTime) / 1e6
        totalTime += time
    end

    print("mean elapsed time is $(totalTime/iterations), number of iterations $(iterations)")
end


@time permutate()
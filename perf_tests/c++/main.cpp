#include <iostream>
#include <cstdlib>
#include <time.h>

using namespace std;

double inline ticks_to_millis(long ticks)
{
    return (double)ticks / CLOCKS_PER_SEC * 1000;
}

void inline swap(uint *array, int &first_index, int &second_index)
{
    auto temp = array[first_index];
    array[first_index] = array[second_index];
    array[second_index] = temp;
}

void inline print_array(uint *array, int array_size)
{
    cout << endl;
    for(int i=0;i<array_size;i++)
    {
        cout << array[i] << ' ';
    }
    cout << endl;
}

int main()
{
    const int array_size = 10000000;
    const int timeout = 1;
    const uint min_iterations = 10;
    uint *indices = new uint[array_size];

    for(int i=0;i<array_size;i++)
    {
        indices[i] = i;
    }

    clock_t timer;
    long total_ticks = 0;
    uint iterations = 0;
    do
    {
        timer = clock();
        for(int i=array_size-1;i>0;i--)
        {
            auto swap_index = rand() % i;
            swap(indices, i, swap_index);
        }
        auto iteration_ticks = clock() - timer;
        total_ticks += iteration_ticks;
        iterations++;
        // cout << "total time " << total_ticks << " in millis " <<
    } while (ticks_to_millis(total_ticks) < timeout * 1000 || iterations < min_iterations);

    cout << "mean elapsed time in milliseconds " << ticks_to_millis(total_ticks) / iterations << ", iterations " << iterations << endl;

    delete []indices;
    return 0;
}
import csv
import os
import numpy as np
from experiment_result import ExperimentResult
from execution import Execution

def get_list_of_directories(root_directory, add_root_directory=True):
    return [os.path.join(root_directory, d) if add_root_directory else d
        for d in os.listdir(root_directory) if os.path.isdir(os.path.join(root_directory, d))]

def load_instances(instances_directories, files_extension):
    return [load_instance_results(instance, files_extension) for instance in instances_directories]

def load_instance_results(directory, files_extension):
    experiments_results = []

    for experiment_dir in get_list_of_directories(directory, False):
        full_dir = os.path.join(directory, experiment_dir)
        files = [file for file in os.listdir(full_dir) if files_extension in file]
        result = ExperimentResult()
        for filename in files:
            result.name = experiment_dir
            with open(os.path.join(full_dir, filename)) as file:
                print("Reading {}".format(filename))
                result.instance = file.readline().split(';')[1]
                result.mean_execution_time = float(file.readline().split(';')[1].rstrip("\n").replace(",","."))
                execution = Execution()
                execution.time = float(file.readline().split(';')[1].rstrip("\n").replace(",","."))
                execution.steps = int(file.readline().split(';')[1].rstrip("\n").replace(",","."))
                execution.cost = int(file.readline().split(';')[1].rstrip("\n").replace(",","."))
                execution.best_known_cost = int(file.readline().split(';')[1].rstrip("\n").replace(",","."))
                execution.number_of_improvements = int(file.readline().split(';')[1].rstrip("\n").replace(",","."))
                execution.solution = file.readline().split(';')
                for line in file:
                    row = line.split(';')
                    execution.intermediate_costs.append(int(row[0]))
                print('\t{} intermediate costs'.format(len(execution.intermediate_costs)))
                result.executions.append(execution)
        print("Read {} files\n".format(len(result)))
        experiments_results.append(result)
    print("Read {} experiments".format(len(experiments_results)))
    return experiments_results


def calculate_arrays_similarity(arr1, arr2):
    return float(get_the_longest_subsequence(arr1, arr2)) / len(arr1)


def find_starting_index(first_array, second_array):
    for key, value in enumerate(second_array):
        if first_array[0] == value:
            return key
    print("Matching value not found.")
    return -1


def get_the_longest_subsequence(arr1, arr2):
    nearest_index = find_starting_index(arr1, arr2)
    if nearest_index < 0:
        exit(0)
    arr1 = np.array(arr1)
    arr2 = np.roll(np.array(arr2), -nearest_index)

    result = np.zeros((len(arr1), len(arr2)))

    for i in range(1, len(arr1)):
        for j in range(1, len(arr2)):
            if arr1[i] == arr2[j]:
                result[i, j] = result[i-1, j-1]+1
            else:
                result[i, j] = max(result[i-1, j], result[i, j-1])
    return 0 if result[-1, -1] <= 1 else result[-1, -1]+1
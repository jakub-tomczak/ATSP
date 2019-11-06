import csv
import os
import numpy as np
from experiment_result import ExperimentResult
from execution import Execution

def find_experiments_directories(root_directory):
    return [d for d in os.listdir(root_directory) if os.path.isdir(os.path.join(root_directory, d))]

def load_results(directory, files_extension):
    experiments_results = []

    for experiment_dir in find_experiments_directories(directory):
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
                for line in file:
                    row = line.split(';')
                    execution.intermediate_costs.append(int(row[0]))
                print('\t{} intermediate costs'.format(len(execution.intermediate_costs)))
                result.executions.append(execution)
        print("Read {} files\n".format(len(result)))
        experiments_results.append(result)
    print("Read {} experiments".format(len(experiments_results)))
    return experiments_results
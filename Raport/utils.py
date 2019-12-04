import os
import pandas as pd
import numpy as np

def calculate_effectiveness(data):
    maxSteps = np.max(data.Execution_steps)
    maxImprovements = np.max(data.Number_of_improvements)
    meanExecutionTime = np.mean(data.Execution_time)

    data.loc[:, 'Effectiveness'] = (data.Number_of_improvements/maxImprovements) \
        / (data.Execution_time / meanExecutionTime * data.Execution_steps / maxSteps * (data.Quality+0.001)*10)

def get_list_of_directories(root_directory, add_root_directory=True):
    return [os.path.join(root_directory, d) if add_root_directory else d
        for d in os.listdir(root_directory) if os.path.isdir(os.path.join(root_directory, d))]

def load_data(instances_directories, files_extension, datatype_func):
    return pd.concat([datatype_func(instance, files_extension)
        for instance in instances_directories], axis=0)

def load_instance_results(directory, files_extension):
    files = [file for file in os.listdir(directory) if files_extension in file and 'cost' not in file]
    
    experiments_results = pd.concat(
        [pd.read_csv(os.path.join(directory, file), sep=';') for file in files],
        axis=0
    )

    # calculate effectiveness based on all results from one instance
    calculate_effectiveness(experiments_results)

    print("Read {} experiments, from {}".format(len(files), directory))
    print("Experiemnts {}".format(experiments_results.shape))
    return experiments_results

def load_costs_results(directory, files_extension):
    files = [file for file in os.listdir(directory) if files_extension in file and 'cost' in file]
    
    if len(files) < 1:
        return pd.DataFrame()

    costs_results = pd.concat(
        [pd.read_csv(os.path.join(directory, file), sep=';') for file in files],
        axis=0
    )

    print("Read {} costs, from {}".format(len(files), directory))
    print("Costs {}".format(costs_results.shape))
    return costs_results
import csv
import os
from experiment_result import ExperimentResult

def load_results(directory, files_extension):
    files = [file for file in os.listdir(directory) if files_extension in file]

    experiments_results = []
    for file in files:

        result = ExperimentResult()
        result.name = file.split('_')[0]
        result.instance = file.split('_')[1].split('.')[0]
        print(result.name, result.instance)

        with open(os.path.join(directory, file)) as csv_file:
            print("Reading {}".format(file))
            header = csv_file.readline()
            reader = csv.reader(csv_file, delimiter=';')
            for row in reader:
                result.steps.append(row[0])
                result.times.append(row[1])
                result.costs.append(row[2])
        print("Read {} records\n".format(len(result)))
        experiments_results.append(result)
    return experiments_results
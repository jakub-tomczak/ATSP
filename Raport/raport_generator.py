import os
import csv
from experiment_result import ExperimentResult

def load_results(directory, files_extension):
    files = [file for file in os.listdir(directory) if files_extension in file]

    experiments_results = []
    for file in files:

        result = ExperimentResult()
        result.name = file.split('_')[0]
        result.instance = file.split('_')[1]

        with open(os.path.join(directory, file)) as csv_file:
            print("Reading {}".format(file))
            reader = csv.reader(csv_file, delimiter=';')
            for row in reader:
                result.steps.append(row[0])
                result.times.append(row[1])
                result.costs.append(row[2])
        print("Read {} records\n".format(len(result)))
        experiments_results.append(result)
    return experiments_results

def main():
    if len(os.sys.argv) < 3:
        print("Add experiments' results directory as the first argument, files extensions as the second one")
        exit(1)

    experiments_directory = os.sys.argv[1]
    extension = os.sys.argv[2]

    print("Using {} directory".format(experiments_directory))
    load_results(experiments_directory, extension)

if __name__ == "__main__":
    main()
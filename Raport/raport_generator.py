import os

def load_results(directory, files_extension):
    files = [file for file in os.listdir(directory) if files_extension in file]
    for file in files:
        print('Reading file {}'.format(file))

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
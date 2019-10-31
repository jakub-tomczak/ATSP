import os

def main():
    if len(os.sys.argv) < 2:
        print("Add experiments' results directory as the first argument")
        exit(1)

    experiments_directory = os.sys.argv[1]
    print("Using {} directory".format(experiments_directory))

if __name__ == "__main__":
    main()
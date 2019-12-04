#!/usr/bin/env python
import os
from utils import load_data, get_list_of_directories, load_costs_results, load_instance_results
from draw_plots import PlotDrawer
import argparse

def parse_args():
    parser = argparse.ArgumentParser()
    parser.add_argument('experiments_directory', type=str, help='Directory with experiments\' results directories')
    parser.add_argument('extension', type=str, help='Extension of experiments\' results files')
    parser.add_argument('plots_path', type=str, help='Path for output plots')
    parser.add_argument('--display_plots', help='Should plots be displayed', action='store_true')
    parameters = parser.parse_args()
    return parameters

def main(parameters):
    print("Using {} directory".format(parameters.experiments_directory))
    instances_directories = get_list_of_directories(parameters.experiments_directory)
    data = load_data(instances_directories, parameters.extension, load_instance_results)
    costs = load_data(instances_directories, parameters.extension, load_costs_results)
    # data.to_csv('all_data.csv', sep=';')
    plot_drawer = PlotDrawer(parameters.plots_path, parameters.display_plots)
    plot_drawer.draw_plots(data, costs)

if __name__ == "__main__":
    parameters = parse_args()
    main(parameters)

#!/usr/bin/env python
import os
from utils import load_instances, get_list_of_directories
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
    instances = load_instances(instances_directories, parameters.extension, load_intermediate_costs=True)
    plot_drawer = PlotDrawer(parameters.plots_path, parameters.display_plots)
    plot_drawer.draw_plots(instances)

if __name__ == "__main__":
    parameters = parse_args()
    main(parameters)

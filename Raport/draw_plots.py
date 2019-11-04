import os
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns

class PlotDrawer():
    def __init__(self, plots_path, display_plots):
        self.save_plots = True if plots_path is not None and plots_path != "" else False
        self.show_plots = display_plots
        self.save_directory = plots_path

    def save_plot(self, name, extension='png'):
        if not self.save_plots:
            return
        if name == "":
            print("plot name cannot be empty.")
            return
        if not os.path.exists(self.save_directory):
            if not os.makedirs(self.save_directory):
                print("Failed to create directory for plots {}".format(self.save_directory))
        plt.savefig(os.path.join(self.save_directory, '{}.{}'.format(name, extension)))

    def show_plot(self):
        if self.show_plots:
            plt.show()

    def get_function(self, type):
        if type == "mean":
            return np.mean
        elif type == "max":
            return np.max
        return None

    def get_qualities(self, data):
        return np.array([[execution.quality for execution in x.executions] for x in data])

    def get_intermediate_costs(self, data):
        return np.array([[execution.intermediate_costs for execution in x.executions] for x in data])

    def draw_quality_plots(self, data):
        if len(data) < 1:
            return
        print("drawing quality plots")
        instance_name = data[0].instance
        print(data[0].executions[0].quality)
        mean_qualities = self.get_qualities(data)
        best_qualities = np.max(self.get_qualities(data), axis=1)

        sns.boxplot(data=mean_qualities)
        # self.show_plot()
        self.save_plot("mean_quality")

        print(mean_qualities)

        sns.boxplot(data=best_qualities)
        # self.show_plot()
        self.save_plot("best_qualities")

    def draw_time_plots(self, data):
        print("drawing time plots")

    def draw_plots(self, data):
        print("drawing graphs")
        self.draw_time_plots(data)
        self.draw_quality_plots(data)


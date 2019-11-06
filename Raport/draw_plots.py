import os
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns


class PlotDrawer():
    def __init__(self, plots_path, display_plots):
        self.save_plots = True if plots_path is not None and plots_path != "" else False
        self.show_plots = display_plots
        self.save_directory = plots_path

    def save_plot(self, name, extension='png',instance=''):
        if not self.save_plots:
            return
        if name == "":
            print("plot name cannot be empty.")
            return
        if not os.path.exists(self.save_directory):
            if not os.makedirs(self.save_directory):
                print("Failed to create directory for plots {}".format(self.save_directory))
        plt.title(name + ' ' + instance)
        plt.savefig(os.path.join(self.save_directory, '{}_{}.{}'.format(name,instance, extension)))

    def show_plot(self):
        if self.show_plots:
            plt.show()

    def get_function(self, type):
        if type == "mean":
            return np.mean
        elif type == "max":
            return np.max
        return None
    
    def get_instance_name(self,data):
        return data[0].instance.replace('\n','')

    def get_qualities(self, data):
        return np.array([[execution.quality for execution in x.executions] for x in data])


    def get_intermediate_costs(self, data):
        return np.array([[execution.intermediate_costs for execution in x.executions] for x in data])

    def get_alg_names(self,data):
        return [x.name for x  in data]

    def draw_quality_plots(self, data):
        if len(data) < 1:
            return
        print("drawing quality plots")
        mean_qualities = self.get_qualities(data)
        best_qualities = np.max(self.get_qualities(data), axis=1)
        names = self.get_alg_names(data)
        sns.boxplot(data=mean_qualities.tolist()).set_xticklabels(names)
        instance_name = self.get_instance_name(data)
        # self.show_plot()
        self.save_plot("mean_quality", instance=instance_name)

        plt.figure(0)
        plt.plot(names,best_qualities,'o',color='red')
        # self.show_plot()
        self.save_plot("best_qualities", instance=instance_name)

    def draw_time_plots(self, data):
        print("drawing time plots")

    def draw_plots(self, data):
        print("drawing graphs")
        self.draw_time_plots(data)
        self.draw_quality_plots(data)


import os
import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns


class PlotDrawer():
    def __init__(self, plots_path, display_plots):
        self.save_plots = True if plots_path is not None and plots_path != "" else False
        self.show_plots = display_plots
        self.save_directory = plots_path
        self.df = pd.DataFrame()

        # names of instances and algorithms in the dataset
        # filled in the self.set_data
        self.instances = []
        self.algorithms = []

        self.costs = pd.DataFrame()
        self.cost_instances = []
        self.cost_algorithms = []

    def set_data(self, dataframe):
        self.df = dataframe
        self.instances = self.df.Instance_name.unique()
        self.algorithms = self.df.Algorithm.unique()

    def set_costs(self, costs):
        self.costs = costs
        self.cost_instances = self.costs.Instance_name.unique()
        self.cost_algorithms = self.costs.Algorithm.unique()

    def save_plot(self, name, extension='png', instance='', set_title=False):
        if not self.save_plots:
            return
        if name == "":
            print("plot name cannot be empty.")
            return
        if instance == '':
            instance = 'all'
        if not os.path.exists(self.save_directory):
            if not os.makedirs(self.save_directory):
                print("Failed to create directory for plots {}".format(self.save_directory))
        if set_title:
            plt.title(name + ' ' + instance)
        plt.savefig(os.path.join(self.save_directory, '{}_{}.{}'.format(name,instance, extension)))

    def show_plot(self):
        if self.show_plots:
            plt.show()

    def draw_instance_based_plot(self, y_name, y_desc, estimator_function, plot_name, plotting_function=sns.pointplot, x_name="Instance_name", x_desc="Instancja", data=None):
        plt.clf()
        if data is None:
            data = self.df
        plotting_function(data=data, x = x_name, y = y_name, hue="Algorithm", estimator=estimator_function)
        plt.xticks(rotation=90)
        plt.xlabel(x_desc)
        plt.ylabel(y_desc)
        plt.subplots_adjust(bottom=.2)
        plt.legend(title='Algorytm')
        plt.tight_layout()
        self.save_plot(plot_name, instance='')
        self.show_plot()

    def draw_algorithm_based_plot(self, y_name, y_desc, plot_name, plotting_function=sns.violinplot, x_name="Algorithm", x_desc="Algorytm", hue_name=None, data=None):
        if data is None:
            data = self.df
        for instance_name in self.instances:
            plt.clf()
            plotting_data = data[data.Instance_name == instance_name]
            if hue_name is None:
                plotting_function(data=plotting_data, x=x_name, y=y_name)
            else:
                plotting_function(data=plotting_data, x=x_name, y=y_name, hue=hue_name, style=hue_name)
            plt.xlabel(x_desc)
            plt.ylabel(y_desc)
            plt.tight_layout()
            self.save_plot(plot_name, instance=instance_name)
            self.show_plot()

    def draw_quality_plots(self):
        self.draw_algorithm_based_plot("Quality", "Średnia jakość rozwiązania", "mean_quality_plot")

        self.draw_instance_based_plot(y_name="Quality", y_desc="Najlepsza jakość", estimator_function=min, plot_name='best_quality')

    def draw_time_plots(self):
        self.draw_instance_based_plot(y_name="Execution_time", y_desc="Średni czas działania [ms]", estimator_function=np.mean, plot_name='mean_execution_time')

        no_tabu_data = self.df[~(self.df.Algorithm=='Tabu')]
        self.draw_instance_based_plot(y_name="Execution_time", y_desc="Średni czas działania [ms], bez Tabu", estimator_function=np.mean, plot_name='mean_execution_time_no_tabu', data=no_tabu_data)
        
    def draw_effectiveness_plots(self):
        self.draw_algorithm_based_plot(y_name="Effectiveness", y_desc="Efektywność", plotting_function=sns.boxplot, plot_name="effectiveness")

    def draw_steps_plots(self):
        self.draw_instance_based_plot(y_name="Execution_steps", y_desc="Średnia liczba kroków algorytmu", estimator_function=np.mean, plot_name='mean_steps')

    def draw_number_of_improvements_plots(self):
        self.draw_instance_based_plot(y_name="Number_of_improvements", y_desc="Liczba ocenionych rozwiązań", estimator_function=np.mean, plot_name='mean_improvements')

    def draw_restart(self):
        def aggregate_costs_data(costs_data, agg_type, algo_name):
            costs_columns = costs_data.columns[3:]
            costs_ = costs_data[costs_columns]
            df = None
            if agg_type == "min":
                best_costs = [np.min(costs_[col]) for col in costs_]
                for i in range(1, len(best_costs)):
                    if best_costs[i-1] < best_costs[i]:
                        best_costs[i] = best_costs[i-1]
                df = pd.DataFrame(np.array([costs_columns, best_costs]).T)
            elif agg_type == "mean":
                df = pd.DataFrame(np.array([costs_columns, [np.mean(costs_data[col]) for col in costs_]]).T)
            
            # add algorithm name column
            df.loc[:, 'algo_name'] = algo_name
            
            df.columns=['x', 'y', 'algo_name']
            # cast x and y to numeric values, by default they are strings
            df.x = pd.to_numeric(df.x)
            df.y = pd.to_numeric(df.y)
            return df

        for instance in self.cost_instances:
            data_best = pd.concat([aggregate_costs_data(self.costs[(self.costs.Algorithm == algo) & (self.costs.Instance_name == instance)],
                                             'min',
                                             algo)
                        for algo in self.cost_algorithms])
            data_mean = pd.concat([aggregate_costs_data(self.costs[(self.costs.Algorithm == algo) & (self.costs.Instance_name == instance)],
                                             'mean',
                                             algo)
                        for algo in self.cost_algorithms])
            
            def _draw_restarts_plots(data, y_desc, name):
                num_bins = 10
                step = int(np.ceil(len(data.x)/(num_bins*len(self.cost_algorithms))))
                
                plt.clf()
                sns.pointplot(data=data,x=data.x, y=data.y, hue=data.algo_name)\
                    .set_xticklabels(data.x[::step])
                plt.locator_params(nbins=num_bins)
                plt.xticks(rotation=45)
                plt.xlabel('Restart')
                plt.ylabel(y_desc)
                plt.subplots_adjust(bottom=.2)
                plt.legend(title='Algorytm')
                plt.tight_layout()
                self.save_plot(name, instance=instance)
                self.show_plot()
                
            _draw_restarts_plots(data_best, 'Najlepszy koszt', 'restarts_best')
            _draw_restarts_plots(data_mean, 'Średni koszt', 'restarts_mean')

    def draw_initial_vs_final_solution_plots(self):
        self.draw_algorithm_based_plot(y_name="Quality",
                                      y_desc="Jakość ostatecznego rozwiązania",
                                      plot_name='initial_vs_final',
                                      plotting_function=sns.scatterplot,
                                      x_name="Initial_quality",
                                      x_desc="Jakość pierwotnego rozwiązania",
                                      hue_name="Algorithm")

    def draw_similarity_plots(self):
        self.draw_algorithm_based_plot(y_name="Similarity",
                                      y_desc="Podobieństwo",
                                      plot_name='similarities',
                                      plotting_function=sns.scatterplot,
                                      x_name="Quality",
                                      x_desc="Jakość",
                                      hue_name="Algorithm")
    
    def draw_plots(self, data, costs):
        self.set_data(data)
        self.set_costs(costs)
        self.draw_quality_plots()
        self.draw_time_plots()
        self.draw_effectiveness_plots()
        self.draw_steps_plots()
        self.draw_number_of_improvements_plots()
        self.draw_initial_vs_final_solution_plots()
        self.draw_similarity_plots()
        self.draw_restart()



import os
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns
from utils import calculate_arrays_similarity


class PlotDrawer():
    markers = ['o--', '+--', 's--', 'v--', '*--', 'p--', 'x--', '|--']

    def __init__(self, plots_path, display_plots):
        self.save_plots = True if plots_path is not None and plots_path != "" else False
        self.show_plots = display_plots
        self.save_directory = plots_path

    def save_plot(self, name, extension='png', instance='', set_title=False):
        if not self.save_plots:
            return
        if name == "":
            print("plot name cannot be empty.")
            return
        if not os.path.exists(self.save_directory):
            if not os.makedirs(self.save_directory):
                print("Failed to create directory for plots {}".format(self.save_directory))
        if set_title:
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

    def transform_instances_to_algorithms(self, data):
        algorithms = dict()
        for instance in data:
            for experiment in instance:
                if experiment.name not in algorithms:
                    algorithms[experiment.name] = []
                algorithms[experiment.name].append(experiment)
        return algorithms

    def get_algorithm_times(self, data, kind="max"):
        names = []
        times = []
        for key, value in data.items():
            names.append(key)
            if kind == "min":
                times.append([min(x.executions, key=lambda x: x.time).time for x in value])
            elif kind == "max":
                times.append([max(x.executions, key=lambda x: x.time).time for x in value])
            elif kind == "mean_all":
                times.append([[execution.time for execution in x.executions] for x in value])
            elif kind == "mean":
                times.append([x.mean_execution_time for x in value])

        return names, times

    def get_instance_name(self,data):
        return data[0].instance.replace('\n','')

    def get_times(self, data):
        return np.array([[execution.time for execution in x.executions] for x in data])

    def get_effectiveness(self, data, worst_cost):
        return np.array([[execution.get_effectiveness2(worst_cost) for execution in x.executions] for x in data])

    def get_improvements(self, data):
        return np.array([[execution.number_of_improvements for execution in x.executions] for x in data])

    def get_qualities(self, data):
        return np.array([[execution.quality for execution in x.executions] for x in data])

    def get_costs(self, data):
        return np.array([[execution.cost for execution in x.executions] for x in data])

    def compare_instances(self, data):
        algorithms_for_comparison = ["greedy", "steepest"]
        best_executions = [
            (experiment.name, min(experiment.executions, key=lambda x: x.cost))
            for experiment in data
            if experiment.name in algorithms_for_comparison
            ]

        worst_executions = [
            (experiment.name, max(experiment.executions, key=lambda x: x.cost))
            for experiment in data
            if experiment.name in algorithms_for_comparison
            ]


        def calculate_comparison_matrix(executions):
            comparison_result = np.ones((len(executions), len(executions)))
            for i, execution in enumerate(executions):
                for j, other in enumerate(executions):
                    if execution[0] != other[0]:
                        comparison_result[i, j] = calculate_arrays_similarity(execution[1].solution, other[1].solution)

            return comparison_result

        return calculate_comparison_matrix(best_executions), calculate_comparison_matrix(worst_executions)

    def get_first_last_qualities(self,data):
        qualities = self.get_qualities(data)
        firsts = []
        lasts = []
        for i in range(len(qualities)):
            firsts.append(qualities[i,0])
            lasts.append(qualities[i,-1])
        return firsts,lasts

    def get_intermediate_costs(self, data):
        return np.array([[execution.intermediate_costs for execution in x.executions] for x in data])

    def get_alg_names(self,data):
        return [x.name for x  in data]

    def draw_quality_plots(self, data):
        if len(data) < 1:
            return
        print("drawing quality plots")
        plt.clf()
        qualities = self.get_qualities(data)
        mean_qualities = qualities
        best_qualities = np.max(qualities, axis=1)
        names = self.get_alg_names(data)
        sns.violinplot(data=mean_qualities.tolist()).set_xticklabels(names)
        instance_name = self.get_instance_name(data)
        plt.xlabel('Algorytmy')
        plt.ylabel('srednia jakosc rozwiazania')
        self.save_plot("mean_quality", instance=instance_name)

        plt.clf()
        for i, marker in zip(range(len(names)), PlotDrawer.markers[:len(names)]):
            plt.plot(names[i],best_qualities[i], marker)
        plt.xlabel('Algorytmy')
        plt.ylabel('najlepsza jakosc rozwiazania')
        self.show_plot()
        self.save_plot("best_qualities", instance=instance_name)

    def draw_first_plot_best_plot(self, data):
        print("drawing first last ")
        plt.clf()
        names = self.get_alg_names(data)
        plt.figure(2)
        firsts, lasts = self.get_first_last_qualities(data)
        for i, marker in zip(range(len(names)), PlotDrawer.markers[:len(names)]):
            plt.plot(firsts[i],lasts[i], marker,label=names[i])
        plt.legend()

        self.save_plot("first_last",instance=instance_name)

    def draw_time_plots(self, data):
        print("drawing time plots")
        plt.clf()
        times = self.get_times(data)
        names = self.get_alg_names(data)
        sns.violinplot(data=times.tolist()).set_xticklabels(names)
        instance_name = self.get_instance_name(data)
        plt.xlabel('Algorytm')
        plt.ylabel('Czas dzialania')
        self.save_plot("times", instance=instance_name)
        self.show_plot()


    def draw_time_plots_algo(self, data):
        print("drawing time plots")
        def draw_time_plot_for_algorithm(algorithm_data, algos_names, instances_names, kind):
            plt.clf()
            markers = PlotDrawer.markers[:len(algos_names)]
            fig, ax = plt.subplots(1, 1)
            for alg_name, plot_data, marker in zip(algos_names, algorithm_data, markers):
                plt.plot(plot_data, marker, label='{}'.format(alg_name, kind))

            fig.canvas.draw()
            ax.set_xticks(np.arange(0, len(instances_names)))
            ax.set_xticklabels(instances_names)
            plt.xticks(rotation=90)
            plt.xlabel('instancja')
            plt.ylabel('{} czas dzialania [ms]'.format(kind))
            plt.legend()
            plt.subplots_adjust(bottom=.2)
            self.save_plot('algos_times_{}'.format(kind), instance='all')
            self.show_plot()

        for kind in ['min', 'max', 'mean']:
            names, algos_times = self.get_algorithm_times(data, kind=kind)
            instances_names = [x.instance for x in data[names[0]]]
            draw_time_plot_for_algorithm(algos_times, names, instances_names, kind)


    def draw_intermediate_costs_plots(self, data):
        print("drawing intermediate costs plots")
        plt.clf()
        for experiment_result, marker in zip(data, PlotDrawer.markers[:len(data)]):
            best_value_experiment = min(experiment_result.executions, key=lambda x: x.cost)
            plt.plot(best_value_experiment.intermediate_costs, label=experiment_result.name)

        if len(data) > 0 and len(data[0].executions) > 0:
            axes = plt.gca()
            axes.axhline(y=data[0].executions[0].best_known_cost, linestyle='--', linewidth=.5, color='magenta', label='best known cost')
            plt.legend()
            plt.xlabel('Liczba iteracji')
            plt.ylabel('Koszt rozwiazania')
            self.save_plot("intermediate_costs".format(experiment_result.name), instance=self.get_instance_name(data))
            self.show_plot()


    def draw_effectiveness_plots(self, data):
        print("drawing effectiveness plots")
        plt.clf()
        worst_cost = np.max(self.get_costs(data))
        names = self.get_alg_names(data)
        fig, ax = plt.subplots(figsize=(8, 5))
        points = np.array(self.get_effectiveness(data, worst_cost))
        axes = sns.boxplot(data=points.tolist()).set_xticklabels(names)
        ax.set_ylabel('Effectiveness %')
        ax.set_xticks(range(len(names)))
        ax.set_xticklabels(names)
        ax.set_title("")

        self.save_plot('effectiveness', instance=data[0].instance, set_title=False)
        self.show_plot()


    def draw_first_last_plots(self,data):
        print("drawing first last plots")
        plt.clf()
        instance_name = self.get_instance_name(data)
        firsts, lasts = self.get_first_last_qualities(data)
        names = self.get_alg_names(data)
        for i, marker in zip(range(len(names)), PlotDrawer.markers[:len(names)]):
            plt.plot(firsts[i],lasts[i], marker,label=names[i])
        plt.legend()
        plt.xlabel('Jakosc pierwotnego rozwiazania')
        plt.ylabel('Jakosc ostatecznego rozwiazania')
        self.save_plot("first_last",instance=instance_name)
        self.show_plot()

    def draw_steps_quanted_results(self,data):
        print("drawing quanted plots")
        plt.clf()
        instance_name = self.get_instance_name(data)
        firsts, lasts = self.get_first_last_qualities(data)
        names = self.get_alg_names(data)
        steps =  np.array([[execution.steps for execution in x.executions] for x in data])
        calculated = np.array([[len(execution.intermediate_costs) for execution in x.executions] for x in data])
        for i, marker in zip(range(len(names)), PlotDrawer.markers[:len(names)]):
            plt.plot(steps[i],calculated[i], marker,label=names[i])
        plt.xlabel('')
        plt.ylabel('')
        plt.legend()
        self.save_plot("steps_results",instance=instance_name)
        self.show_plot()


    def draw_plots(self, data):
        if len(data) > 0:
            algos_data = self.transform_instances_to_algorithms(data)
            self.draw_time_plots_algo(algos_data)

            for instance_data in data:
                print("\n{}\ndrawing graphs for intance {}".format('*'*20, instance_data[0].instance))
                self.draw_time_plots(instance_data)
                self.draw_quality_plots(instance_data)
                self.draw_effectiveness_plots(instance_data)
                # self.draw_improvements_plots(instance_data)
                self.draw_first_last_plots(instance_data)
                self.draw_steps_quanted_results(instance_data)
                best_executions_comparison, worst_executions_comparison = self.compare_instances(instance_data)



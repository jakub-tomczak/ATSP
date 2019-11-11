import os
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns



class PlotDrawer():
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

    def get_instance_name(self,data):
        return data[0].instance.replace('\n','')

    def get_qualities(self, data):
        return np.array([[execution.quality for execution in x.executions] for x in data])

    def get_costs(self, data):
        return np.array([[execution.cost for execution in x.executions] for x in data])

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
        mean_qualities = self.get_qualities(data)
        best_qualities = np.max(self.get_qualities(data), axis=1)
        names = self.get_alg_names(data)
        sns.boxplot(data=mean_qualities.tolist()).set_xticklabels(names)
        instance_name = self.get_instance_name(data)
        plt.xlabel('Algorytmy')
        plt.ylabel('srednia jakosc rozwiazania')
        # self.show_plot()
        self.save_plot("mean_quality", instance=instance_name)
        plt.clf()
        for i in range(len(names)):
            plt.plot(names[i],best_qualities[i],'o')
        plt.legend()
        plt.xlabel('Algorytmy')
        plt.ylabel('najlepsza jakosc rozwiazania')
        # self.show_plot()
        self.save_plot("best_qualities", instance=instance_name)

        print("drawing first last ")
        plt.figure(2)
        firsts, lasts = self.get_first_last_qualities(data)
        for i in range(len(names)):
            plt.plot(firsts[i],lasts[i],'o',label=names[i])
        plt.legend()

        self.save_plot("first_last",instance=instance_name)

    def draw_time_plots(self, data):
        print("drawing time plots")
        for experiment_result in data:
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
        worst_cost = np.max(self.get_costs(data))
        names = self.get_alg_names(data)
        fig, ax = plt.subplots(figsize=(10, 5))
        ax.set_ylabel('Effectiveness %')
        ax.set_xticks(range(len(names)))
        ax.set_xticklabels(names)
        ax.set_title("")
        width = 1
        bar_width = (float)(width)/len(data)
        offsets = np.linspace(-width/len(data), width/len(data), len(data))
        points = [np.mean([execution.get_effectiveness(worst_cost) for execution in experiment_result.executions]) for experiment_result in data]

        rects = [ax.bar(i, point, label = name) for (i, point), name in zip(enumerate(points), names)]
        def autolabel(rects):
            for rect, name in zip(rects, names):
                height = rect[0].get_height()
                ax.annotate('{}%'.format(100*round(height, 4)),
                            xy=(rect[0].get_x() + rect[0].get_width() / 2, height),
                            xytext=(0, 3),  # 3 points vertical offset
                            textcoords="offset points",
                            ha='center', va='bottom')
        autolabel(rects)

        if len(data) > 0:
            # plt.legend()
            self.save_plot('effectiveness', instance=data[0].instance, set_title=False)

            self.show_plot()

    def draw_first_last_plots(self,data):
        instance_name = self.get_instance_name(data)
        firsts, lasts = self.get_first_last_qualities(data)
        names = self.get_alg_names(data)
        for i in range(len(names)):
            plt.plot(firsts[i],lasts[i],'o',label=names[i])
        plt.legend()
        plt.xlabel('Jakosc pierwotnego rozwiazania')
        plt.ylabel('Jakosc ostatecznego rozwiazania')
        self.save_plot("first_last",instance=instance_name)

    def draw_steps_quanted_results(self,data):
        instance_name = self.get_instance_name(data)
        firsts, lasts = self.get_first_last_qualities(data)
        names = self.get_alg_names(data)
        steps =  np.array([[execution.steps for execution in x.executions] for x in data])
        calculated = np.array([[len(execution.intermediate_costs) for execution in x.executions] for x in data])
        for i in range(len(names)):
            plt.plot(steps[i],calculated[i],'o',label=names[i])
        plt.xlabel('')
        plt.ylabel('')
        plt.legend()
        self.save_plot("steps_results",instance=instance_name)


    def draw_plots(self, data):
        if len(data) > 0:
            print("\n{}\ndrawing graphs for intance {}".format('*'*20, data[0].instance))
            self.draw_time_plots(data)
            plt.clf()
            self.draw_quality_plots(data)
            plt.clf()
            self.draw_effectiveness_plots(data)
            plt.clf()
            self.draw_first_last_plots(data)
            plt.clf()
            self.draw_steps_quanted_results(data)



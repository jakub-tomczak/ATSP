import numpy as np

class Execution:
    def __init__(self):
        self.time = 0.0
        self.steps = 0
        self.cost = 0
        self.intermediate_costs = []
        self.best_known_cost = 0
        self.initial_cost = 0
        self.similarity = 0.0
        self.number_of_improvements = 0
        self.solution = []

    @property
    def initial_to_final_execution_improvement(self):
        if self.cost == 0:
            return 0
        return (float)(self.initial_cost) / self.cost

    @property
    def quality(self):
        return (float)(self.cost - self.best_known_cost) / self.best_known_cost

    @property
    def simple_quality(self):
        return (self.cost - self.best_known_cost)

    def get_effectiveness(self, worst_result):
        distance = worst_result - self.best_known_cost
        return 1 - float(self.cost - self.best_known_cost) / distance

    def get_effectiveness2(self, worst_result):
        return self.get_effectiveness(worst_result) * (1/self.time)

    def __len__(self):
        return len(self.intermediate_costs)

    def first_best_quality(self):
        if len(self.intermediate_costs)>0:
            return (self.intermediate_costs[0]- self.best_known_cost) / self.best_known_cost, self.quality
        return 0,0

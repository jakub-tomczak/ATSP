import numpy as np

class Execution:
    def __init__(self):
        self.time = 0.0
        self.steps = 0
        self.cost = 0
        self.intermediate_costs = []
        self.best_known_cost = 0
        self.number_of_improvements = 0
        self.solution = []

    @property
    def quality(self):
        return (self.cost - self.best_known_cost) / self.best_known_cost

    def get_effectiveness(self, worst_result):
        distance = worst_result - self.best_known_cost
        return 1 - float(self.cost - self.best_known_cost) / distance

    def get_effectiveness2(self, worst_result):
        return self.get_effectiveness(worst_result) * (self.number_of_improvements / self.steps)

    def __len__(self):
        return len(self.intermediate_costs)
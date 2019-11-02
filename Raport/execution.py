class Execution:
    def __init__(self):
        self.time = 0.0
        self.steps = 0
        self.cost = 0
        self.intermediate_costs = []
        self.best_known_cost = 0

    @property
    def quality(self):
        return (self.cost - self.best_known_cost) / self.best_known_cost

    def __len__(self):
        return len(self.intermediate_costs)
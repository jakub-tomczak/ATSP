class Execution:
    def __init__(self):
        self.time = 0.0
        self.steps = 0
        self.cost = 0
        self.intermediate_costs = []

    def __len__(self):
        return len(self.intermediate_costs)
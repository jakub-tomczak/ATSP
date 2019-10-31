class ExperimentResult:
    def __init__(self):
        self.name = ""
        self.instance = ""
        self.times = []
        self.steps = []
        self.costs = []

    def __len__(self):
        return len(self.times)
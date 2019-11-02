class ExperimentResult:
    def __init__(self):
        self.name = ""
        self.instance = ""
        self.mean_execution_time = 0.0
        self.executions = []

    def __len__(self):
        return len(self.executions)
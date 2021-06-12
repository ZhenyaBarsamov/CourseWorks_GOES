# Модуль, содержащий класс записи статистики

class Statistics:
    '''
    Класс статистики решения задачи, содержищий информацию о студенте, 
    информацию о решённой задаче, и статистику решения
    '''
    def __init__(self, record_id, student_name, student_group, problem_name, example_name, example_description, statistics_body, total_errors_count, necessary_errors_count, mark, record_time):
        self.record_id = record_id
        self.student_name = student_name
        self.student_group = student_group
        self.problem_name = problem_name
        self.example_name = example_name
        self.example_description = example_description
        self.statistics_body = statistics_body
        self.total_errors_count = total_errors_count
        self.necessary_errors_count = necessary_errors_count
        self.mark = mark
        self.record_time = record_time

    def __str__(self):
        return f'Stat: id={self.record_id}, student={self.student_name} ({self.student_group}), problem={self.problem_name} ({self.example_name}), mark={self.mark}.'
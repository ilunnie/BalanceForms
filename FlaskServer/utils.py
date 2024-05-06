from datetime import datetime
from styles import default, correct, wrong

def get_today_info():
    current_date = datetime.now()
    midday = current_date.replace(hour=12, minute=0, second=0, microsecond=0)
    date = current_date.strftime('%d-%m-%Y')
    period = 'Manhã' if current_date < midday else 'Tarde'
    return {'date': date, 'period': period}

def time_formatter(time):
    hours = int(time) // 3600
    minutes = (int(time) % 3600) // 60
    seconds = int(time) % 60
    time = f"{hours:02d}:{minutes:02d}:{seconds:02d}"
    return time

def verify_answer(answer):
    return '✔' if answer == 2 else '✖' if answer == 1 else '➖'

def define_formatting(answer):
    return correct if answer == 2 else wrong if answer == 1 else default

def define_formatting_by_answer(answer, correct_answer):
    return correct if answer == correct_answer else default if answer == 0 else wrong
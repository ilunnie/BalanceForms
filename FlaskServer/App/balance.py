from flask import Flask, Blueprint, render_template, request, redirect
from xlsxapi import save_user_data
import time

app = Flask(__name__)
bp = Blueprint('balance', __name__, url_prefix='/', static_folder='static')

test_started = 0
default_values = [1000, 750, 500, 200, 100, 
                      675, 600, 500, 50, 25]

@bp.route('/', methods=['GET', 'POST'])
def index():
    global test_started
    if request.method == 'GET':
        test_started = 0
        return render_template(
            'index.html'
        )
    test_started = 2
    time.sleep(1)
    test_started = 0
    return render_template(
            'final.html'
        )

@bp.route('/change_values', methods=['GET', 'POST'])
def change_values():
    global test_started
    if request.method == 'GET':
        test_started = 0
        return render_template(
            'changevalues.html',
            default_values = default_values
        )
    test_started = 0
    values = list(list(request.form.values()))
    with open("values.txt", "w") as file:
        for i in range(len(default_values)):
            file.write(f"{values[i] if values[i] != '' else default_values[i]}")
            if i != 4 and i != 9:
                file.write(',')
            if i == 4:
                file.write('\n')
    test_started = 0
    return render_template(
        'index.html',
    )

@bp.route('/values', methods=['GET'])
def values():
    global test_started
    test_started = 0
    values = {'prova1': [], 'prova2': []}
    with open("values.txt", "r") as file:
        tests_values = file.read().splitlines()
        values['prova1'] = tests_values[0].split(', ')
        values['prova2'] = tests_values[1].split(', ')
    return values, 200

@bp.route('/timer', methods=['GET', 'POST'])
def timer():
    if request.method == 'GET':
        return redirect('/')
    global test_started
    minutes = request.form['minutes']
    if minutes == '':
        minutes = '30'
    test_started = 1
    return render_template(
        'timer.html',
        minutes = minutes
    )

@bp.route('/test', methods=['GET', 'POST'])
def test_status():
    if request.method == 'GET':
        return {'response': test_started}, 200
    if test_started != 0:
        user_data = request.json
        save_user_data(user_data) 
        return 'Respostas enviadas com sucesso.', 201
    return 'A prova ainda não foi iniciada ou já foi finalizada.', 403
    
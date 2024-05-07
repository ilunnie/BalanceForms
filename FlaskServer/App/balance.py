from flask import Flask, Blueprint, render_template, request, redirect
from xlsxapi import save_user_data
import time

app = Flask(__name__)
bp = Blueprint('balance', __name__, url_prefix='/', static_folder='static')

test_started = 0

@bp.route('/', methods=['GET', 'POST'])
def index():
    global test_started
    if request.method == 'GET':
        test_started = 0
        return render_template(
            'index.html',
        )
    test_started = 2
    time.sleep(1)
    test_started = 0
    return render_template(
            'final.html',
        )

@bp.route('/change_values', methods=['GET', 'POST'])
def change_values():
    global test_started
    if request.method == 'GET':
        test_started = 0
        return render_template(
            'changevalues.html',
        )
    values = request.form
    with open("values.txt", "w") as file:
        file.write(f"{values['value1']}, {values['value2']}, {values['value3']}, {values['value4']}, {values['value5']}\n")
        file.write(f"{values['value6']}, {values['value7']}, {values['value8']}, {values['value9']}, {values['value10']}")
    test_started = 0
    return render_template(
        'index.html',
    )

@bp.route('/values', methods=['GET'])
def values():
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
    
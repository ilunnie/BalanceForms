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
    
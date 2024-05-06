from openpyxl.workbook import Workbook
import openpyxl as xl
from utils import get_today_info, time_formatter, verify_answer, define_formatting, define_formatting_by_answer
from styles import header, default, correct, wrong
import os

def create_workbook():
    today = get_today_info()
    date = today['date']
    period = today['period']

    if os.path.exists(f'../Provas/{date}/{period}.xlsx'):
        return
    if not os.path.exists('../Provas'): 
        os.mkdir('../Provas')
    if not os.path.exists(f'../Provas/{date}'):
        os.mkdir(f'../Provas/{date}')

    workbook = Workbook()

    worksheet1 = workbook.active
    worksheet1.title = 'Prova 1'
    workbook.create_sheet('Prova 2')

    first_line = ['Nome', 'Data de nascimento', 'Triângulo', 'Quadrado', 'Círculo', 'Estrela', 'Hexágono', 'Respostas', 'Tempo de prova', 'Quantidade de peças utilizadas', 'Tentativas', '% de acertos']

    for worksheet in workbook.worksheets:
        for i in range(len(first_line)):
            current_column = i + 1
            cell = worksheet.cell(row = 1, column = current_column, value = first_line[i])
            cell.style = header

            worksheet.column_dimensions[cell.column_letter].width = len(first_line[i]) + 2
            if current_column == 1:
                worksheet.column_dimensions[cell.column_letter].width = 25

            worksheet.merge_cells(start_row = 1, start_column = current_column, end_row = 2, end_column = current_column)

    workbook.save(f'../Provas/{date}/{period}.xlsx')

def save_user_data(user_data):
    tests = [list(user_data['prova1'].values()), list(user_data['prova2'].values())]
    tests[0][7] = time_formatter(tests[0][7])
    tests[1][7] = time_formatter(tests[1][7])

    today = get_today_info()
    date = today['date']
    period = today['period']

    if not os.path.exists(f'../Provas/{date}/{period}.xlsx'):
        create_workbook()
    workbook = xl.load_workbook(f'../Provas/{date}/{period}.xlsx')

    current_index = 0
    for index, worksheet in enumerate(workbook.worksheets):
        answer_row = worksheet.max_row + 1
        worksheet.cell(row = answer_row, column = 1, value = user_data['nome']).style = default
        worksheet.cell(row = answer_row, column = 2, value = user_data['nascimento']).style = default
        worksheet.merge_cells(start_row = answer_row, start_column = 1, end_row = answer_row + 1, end_column = 1)
        worksheet.merge_cells(start_row = answer_row, start_column = 2, end_row = answer_row + 1, end_column = 2)
        for i in (len(tests[index][2]) - 1):
            current_index = i + 3
            if current_index <= 7:
                worksheet.cell(
                    row = answer_row,
                    column = current_index,
                    value = verify_answer(tests[index][i])
                ).style = define_formatting(tests[index][i])
                worksheet.merge_cells(start_row = answer_row, start_column = current_index, end_row = answer_row + 1, end_column = current_index)
                continue
            if current_index <= 12:
                for j in len(tests[index][i]):
                    worksheet.cell(
                        row = answer_row,
                        column = current_index,
                        value = tests[index][i][j]
                    ).style = default
                    worksheet.cell(
                        row = answer_row,
                        column = current_index,
                        value = tests[index][i + 1][j]
                    ).style = define_formatting_by_answer(tests[index][i + 1][j])
                    current_index += 1
                continue
            worksheet.cell(
                row = answer_row,
                column = current_index,
                value = tests[index][i]
            ).style = default
            worksheet.merge_cells(start_row = answer_row, start_column = current_index, end_row = answer_row + 1, end_column = current_index)
        percentage = worksheet.cell(row = answer_row, column = current_index, value = tests[index][len(tests[index] - 1)]).style = default
        percentage.number_format = '0%'


    # for index, worksheet in enumerate(worksheets):
    #     worksheet.write(answer_row, 0, user_data['nome'], user_format)
    #     worksheet.write(answer_row, 1, user_data['nascimento'], user_format)
    #     worksheet.write(answer_row, 7, tests[index][5], user_format)
    #     worksheet.write(answer_row, 8, tests[index][6], user_format)
    #     worksheet.write(answer_row, 9, tests[index][7], user_format)
    #     worksheet.write(answer_row, 10, tests[index][8], percentage_format)
    #     for i in range(2, 7):
    #         worksheet.write(answer_row, i, verify_answer(tests[index][crr]), define_formatting(tests[index][crr]))
    #         crr += 1
    #     crr = 0
    # answer_row += 1

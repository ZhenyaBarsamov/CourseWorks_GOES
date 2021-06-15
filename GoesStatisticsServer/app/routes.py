# Модуль с обработчиками маршрутов

from app.statistics import Statistics
from flask import render_template, request, redirect, url_for, session, abort, jsonify, Response
from app import app
from app import storage

@app.route('/')
@app.route('/stats')
def showStats():
    # Если пользователь не аутентифицирован, вход на страницу запрещён
    if 'sessionFlag' not in session:
        return redirect(url_for('login'))
    # Получаем все записи
    stats, error = storage.getAllStatistics()
    if error:
        abort(503)  # сервис недоступен
    return render_template('statlist.html', title='Все записи', stats=stats, authorized=True)

def checkKeyword(keyword):
    '''
    Функция проверки ключевого слова на соответствие требуемому формату.
    Возврат: errors.
    errors - список ошибок в виде строк.
    '''
    errors = []
    if not keyword or len(keyword) < 4 or len(keyword) > 32:
        errors.append('Ключевое слово не может быть пустым, не может быть короче 4 символов и не может быть длиннее 32 символов.')
    return errors

@app.route('/login', methods=['GET', 'POST'])
def login():
    # Если пользователь уже аутентифицирован, отправляем его на страницу с записями
    if 'sessionFlag' in session:
        return redirect(url_for('showStats'))
    # Запрос на отображение формы
    if request.method == 'GET':
        return render_template('login.html', title='Вход', authorized=False)
    # Запрос на обработку формы
    elif request.method == 'POST':
        # Проверяем правильность введённых данных
        keyword = request.form['keyword']
        errors = checkKeyword(keyword)
        if errors:
            return render_template('login.html', title='Вход', errors=errors, authorized=False)
        # Получаем правильное ключевое слово и сравниваем с введённым
        correctKeyword, error = storage.getKeyword()
        if error:
            abort(503)  # сервис недоступен
        if keyword != correctKeyword:
            errors.append('Неправильное ключевое слово')
        # Если теперь были ошибки - выводим их
        if errors:
            return render_template('login.html', title='Вход', errors=errors, authorized=False)
        session['sessionFlag'] = True
        #session.permanent = True  # Делаем её перманентной (время действия такой сессии задаётся в конфиге)
        return redirect(url_for('showStats'))

@app.route('/keywordupdate', methods=['GET', 'POST'])
def keywordUpdate():
    # Проверяем, аутентифицирован ли пользователь
    isAuthorized = 'sessionFlag' in session
    # Запрос на отображение формы
    if request.method == 'GET':
        return render_template('keywordupdate.html', title='Изменить ключевое слово', authorized=isAuthorized)
    # Запрос на обработку формы
    elif request.method == 'POST':
        # Проверяем правильность введённых данных
        oldKeyword = request.form['oldkeyword']
        newKeyword = request.form['newkeyword']
        repeatedNewKeyword = request.form['repeatednewkeyword']
        errors = checkKeyword(newKeyword)
        if newKeyword != repeatedNewKeyword:
            errors.append('Введённое повторно новое ключевое слово не совпадает с первым')
        # Если к этому моменту были ошибки - выводим их
        if errors:
            return render_template('keywordupdate.html', title='Изменить ключевое слово', errors=errors, authorized=isAuthorized)
        # Получаем текущее правильное ключевое слово, сравниваем его с введённым
        correctKeyword, error = storage.getKeyword()
        if error:
            abort(503)  # сервис недоступен
        if oldKeyword != correctKeyword:
            errors.append('Вы ввели неправильное ключевое слово')
        # Если к этому моменту были ошибки - выводим их
        if errors:
            return render_template('keywordupdate.html', title='Изменить ключевое слово', errors=errors, authorized=isAuthorized)
        # Меняем ключевое слово
        error = storage.changeKeyword(newKeyword)
        if error:
            abort(503)  # сервис недоступен
        # Если пользователь не был аутентифицирован - теперь да (он ввёл правильное текущее ключевое слово)
        # Создаём пользовательскую сессию, добавляя в куки сессии флаг
        if not isAuthorized:
            session['sessionFlag'] = True
        #session.permanent = True  # Делаем её перманентной (время действия такой сессии задаётся в конфиге)
        # Перенаправляем пользователя на страницу с записями
        return redirect(url_for('showStats'))

@app.route('/logout')
def logout():
    # Если пользователь не аутентифицирован, перенаправляем его на страницу входа
    if 'sessionFlag' not in session:
        return redirect(url_for('login'))
    session.pop('sessionFlag')
    return redirect(url_for('login'))

@app.route('/api/stats', methods=['GET'])
def getStats():
    # Если пользователь не аутентифицирован, требуем этого
    if 'sessionFlag' not in session:
        abort(401)  # требуется аутентификация
    # Получаем все записи 
    stats, error = storage.getAllStatistics()
    if error:
        abort(503)  # сервис недоступен
    # Отдаём их в формате JSON
    return jsonify([stat.__dict__ for stat in stats])

@app.route('/api/stats', methods=['POST'])
def createStat():
    # Проверяем правильность запроса - контент должен быть json
    if not request.is_json:
        abort(400)  # ошибка в запросе - нужен json
    # Формируем добавляемый объект Statistics
    creatingStat = Statistics(None, request.json['student_name'], request.json['student_group'],
        request.json['problem_name'], request.json['example_name'], request.json['example_description'],
        request.json['statistics_body'], request.json['total_errors_count'], request.json['necessary_errors_count'], 
        request.json['mark'], None)
    stat, error = storage.addStatistics(creatingStat)
    if error:
        abort(503)  # сервис недоступен
    # Отдаём результат - добавленную запись - в формате JSON
    return jsonify(stat.__dict__)

@app.route('/api/stats', methods=['DELETE'])
def deleteAllStats():
    # Если пользователь не аутентифицирован, требуем этого
    if 'sessionFlag' not in session:
        abort(401)  # требуется аутентификация
    # Удаляем все записи
    error = storage.deleteAllStatistics()
    if error:
        abort(503)  # сервис недоступен
    return Response(status=200)

@app.route('/api/stats/<int:recordId>/', methods=['GET'])
def getStat(recordId):
    # Если пользователь не аутентифицирован, требуем этого
    if 'sessionFlag' not in session:
        abort(401)  # требуется аутентификация
    # Получаем запись с требуемым id
    stat, error = storage.getStatisticsForId(recordId)
    if error:
        abort(503)  # сервис недоступен
    if not stat:
        abort(404)  # такой записи нет
    # Отдаём её в формате JSON
    return jsonify(stat.__dict__)


@app.route('/api/stats/<int:recordId>/', methods=['DELETE'])
def deleteStat(recordId):
    # Если пользователь не аутентифицирован, требуем этого
    if 'sessionFlag' not in session:
        abort(401)  # требуется аутентификация
    # Удаляем запись
    error = storage.deleteStatistics(recordId)
    if error:
        abort(503)  # сервис недоступен
    return Response(status=200)

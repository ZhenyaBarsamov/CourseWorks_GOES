# Модуль, предоставляющий средства для работы с хранилищем приложения - БД SQLite

import sqlite3
import datetime
from app.statistics import Statistics

g_databasePath = 'database\\database.db'  # путь до файла БД

def __selectQuery(sql, params):
    '''
    Выполнить SELECT-запрос с заданными параметрами к БД.
    Возврат: result, errorText.
    result - список кортежей.
    errorText означивается в случае ошибки.
    '''
    global g_databasePath
    res = None
    error = None
    try:
        conn = sqlite3.connect(g_databasePath, check_same_thread=False)
        cursor = conn.cursor()
        cursor.execute(sql, params)
        res = cursor.fetchall()
    except Exception as e:
        error = str(e)
    finally:
        if conn:
            conn.close()
    return res, error

def __createUpdateDeleteQuery(sql, params=None):
    '''
    Выполнить CREATE-, UPDATE- или DELETE-запрос с заданными параметрами к БД (одну строку).
    Возврат: lastInsertedRowId, errorText.
    lastInsertedRowId - id вставленной записи.
    errorText означивается в случае ошибки.
    '''
    global g_databasePath
    lastInsertedRowId = None
    error = None
    conn = None
    try:
        conn = sqlite3.connect(g_databasePath, check_same_thread=False)
        cursor = conn.cursor()
        if params:
            cursor.execute(sql, params)
        else:
            cursor.execute(sql)
        conn.commit()
        lastInsertedRowId = cursor.lastrowid
    except Exception as e:
        error = str(e)
    finally:
        if conn:
            conn.close()
    return lastInsertedRowId, error

def getAllStatistics():
    '''
    Получить все записи из БД, отсортированные по времени в порядке от новых к старым.
    Возврат: список statistics, errorText.
    errorText означивается в случае ошибки.
    '''
    stats = None
    error = None
    sql = '\
        SELECT\
            record_id, student_name, student_group,\
            problem_name, example_name, example_description,\
            statistics_body, total_errors_count, necessary_errors_count,\
            mark, record_time\
        FROM "statistics"\
        ORDER BY record_time DESC'
    queryRes, error = __selectQuery(sql, tuple())
    # Если есть хотя бы что-то, даже пустой список = is not None
    # А если просто проверять if queryRes, то пустой список даст False
    if queryRes is not None:
        stats = []
        for row in queryRes:
            stats.append(Statistics(int(row[0]), row[1], row[2], row[3], row[4], 
            row[5], row[6], int(row[7]), int(row[8]), int(row[9]),
            datetime.datetime.strptime(row[10], '%Y-%m-%d %H:%M:%S.%f')))
    return stats, error

def getStatisticsForId(recordId):
    '''
    Получить статистику из БД по её id.
    Возврат: statistics, errorText.
    errorText означивается в случае ошибки.
    '''
    statistics = None
    error = None
    sql = '\
        SELECT\
            record_id, student_name, student_group,\
            problem_name, example_name, example_description,\
            statistics_body, total_errors_count, necessary_errors_count,\
            mark, record_time\
        FROM "statistics" WHERE record_id = ?'
    params =(recordId,)
    queryRes, error = __selectQuery(sql, params)
    if queryRes:
        tmp = queryRes[0]  # берём из списка первый кортеж
        statistics = Statistics(int(tmp[0]), tmp[1], tmp[2], tmp[3], tmp[4], 
            tmp[5], tmp[6], int(tmp[7]), int(tmp[8]), 
            int(tmp[9]), datetime.datetime.strptime(tmp[10], '%Y-%m-%d %H:%M:%S.%f'))
    
    return statistics, error

def addStatistics(statistics):
    '''
    Добавить статистику в БД.
    Возврат: statistics, errorText.
    statistics - переданный параметром объект статистики с означенным record_id, если она была успешно добавлена.
    errorText означивается в случае ошибки.
    '''
    statisticsRes = None
    error = None
    sql = '\
        INSERT INTO "statistics" (\
                student_name, student_group,\
                problem_name, example_name, example_description,\
                statistics_body, total_errors_count, necessary_errors_count,\
                mark, record_time)\
        VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)'
    params = (statistics.student_name, statistics.student_group, 
            statistics.problem_name, statistics.example_name, statistics.example_description, 
            statistics.statistics_body, statistics.total_errors_count, statistics.necessary_errors_count, 
            statistics.mark, datetime.datetime.now())
    record_id, error = __createUpdateDeleteQuery(sql, params)
    if not error:
        statistics.record_id = record_id
        statisticsRes = statistics
    return statisticsRes, error

def deleteAllStatistics():
    '''
    Удалить все статистики из БД.
    Возврат: errorText.
    errorText означивается в случае ошибки.
    '''
    error = None
    sql = 'DELETE FROM "statistics"'
    _, error = __createUpdateDeleteQuery(sql, tuple())
    return error

def deleteStatistics(recordId):
    '''
    Удалить статистику из БД по её id.
    Возврат: errorText.
    errorText означивается в случае ошибки.
    '''
    error = None
    sql = 'DELETE FROM "statistics" WHERE record_id = ?'
    params = (recordId,)
    _, error = __createUpdateDeleteQuery(sql, params)
    return error

def changeKeyword(keyword):
    '''
    Изменить ключевое слово для входа на новое.
    Возврат: errorText.
    errorText означивается в случае ошибки.
    '''
    error = None
    try:
        with open('keyword.txt', 'w', encoding='utf-8') as f:
            f.write(keyword)
    except:
        error = ''
    return error

def getKeyword():
    '''
    Получить ключевое слово для входа.
    Возврат: errorText.
    errorText означивается в случае ошибки.
    '''
    keyword = None
    error = None
    try:
        with open('keyword.txt', 'r', encoding='utf-8') as f:
            keyword = f.read().strip()
    except:
        error = ''
    return keyword, error
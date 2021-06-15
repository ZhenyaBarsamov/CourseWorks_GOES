# Скрипт для создания SQLite базы данных для приложения

import sqlite3
from pathlib import Path

databaseFile = 'database.db' # название файла БД
schemaFile = 'dbschema.sql' # название файла скрипта создания БД

appPath = Path(__file__).parent # путь до данного исполняемого файла

# БД и скрипт её создания располагаются в папке database в директории с данным файлом
databasePath = appPath / 'database' / databaseFile
schemaPath = appPath / 'database' / schemaFile

# Если БД уже существует, удаляем её
if databasePath.exists():
    databasePath.unlink()

# Считываем файл со скриптом создания БД, и исполяем его
conn = sqlite3.connect(str(databasePath))
with open(schemaPath, 'r', encoding='utf8') as schemaScript:
    conn.executescript(schemaScript.read())
conn.close()

print('БД создана!')
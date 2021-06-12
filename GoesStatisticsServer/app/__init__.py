from flask import Flask
from config import Config

# Создаём экземпляр приложения
app = Flask(__name__)
# Задаём конфигурацию сервера из нашего config-объекта
app.config.from_object(Config())

# Импортируем маршруты - внизу, потому что модулю routes понадобится сам app
from app import routes
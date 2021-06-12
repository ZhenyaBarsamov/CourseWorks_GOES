# Модуль, предоставляющий конфигурационный класс для Flask

import datetime

class Config:
    def __init__(self):
        # Секретный ключ приложения, использующийся для шифрования,
        # подписывания и прочего. Необходим для хранения куки сессии
        self.SECRET_KEY = 'MY_SECRET-SECRET_KEY'
        # Время действия перманентной сессии (для которой permanent = True)
        self.PERMANENT_SESSION_LIFETIME = datetime.timedelta(days=1)
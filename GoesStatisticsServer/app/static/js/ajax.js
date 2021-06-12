// Сохраняем в переменные форму добавления записи, а также список записей, блок-загрузочный экран и блок, содержащий контент списка записей
const addForm = document.querySelector('#add-form');
const todosList = document.querySelector('#todos-list');
const loadingDiv = document.querySelector('#loading');
const todoListWrapper = document.querySelector('#todo-list-wrapper');
// Получаем чекбоксы фильтрации
const filterDoneAll = document.querySelector('#filter-done-all');
const filterDoneDone = document.querySelector('#filter-done-done');
const filterDoneNot = document.querySelector('#filter-done-not');

// Функция включения/отключения загрузочного экрана
function setLoading(isLoading) {
    // В зависимости от значения isLoading отображаем или загрузочный экран, или контент списка
    todoListWrapper.style.display = isLoading ? 'none' : '';
    loadingDiv.style.display = isLoading ? '' : 'none';
}

// Проверка карточки задания на выполненность
function isTodoDone(todoLi) {
    // Определеяем по цевету карточки (если карточка цвета info, то текущий статус "выполнена")
    return todoLi.classList.contains('bg-info');
}

// -----------------------AJAX--------------------------

// Переопределение обработчика события отправки формы
addForm.addEventListener('submit', async (event) => {
    // Отменяем вызов события по умолчанию
    event.preventDefault();
    // Достаём данные из формы в объект
    let newTodoObj = {};
    new FormData(addForm).forEach((value, key) => newTodoObj[key] = value);
    // Проверяем, введён ли заголовок записи. Нет заголовка - нет записи
    if (newTodoObj.title.trim() == '')
        return;
    // Выставляем загрузку
    setLoading(true);
    // Добавляем
    try {
        // Выполняем запрос и получаем в ответ объект добавленной записи
        const response = await fetch('/api/todos', {
            method: 'POST',
            credentials: 'same-origin',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(newTodoObj),
        });
        if (!response.ok) {
            alert(await response.text());
            if (response.status === 401) {
                window.location = '/login';
            }
            setLoading(false);
            return;
        }
        newTodoObj = await response.json();
        // Создаём новый элемент списка - карточку белого цвета (т.е. не задача не выполнена)
        const newListItem = document.createElement('li');
        newListItem.classList.add('card', 'bg-light', 'mb-3');
        newListItem.setAttribute('data-todo-id', newTodoObj.id);
        // Создаём заголовок карточки
        const cardHeader = document.createElement('div');
        cardHeader.classList.add('card-header');
        cardHeader.textContent = newTodoObj.title;
        // Создаём тело карточки
        const cardBody = document.createElement('div');
        cardBody.classList.add('card-body');
        // Создаём основную часть тела карточки
        const bodyText = document.createElement('p');
        bodyText.classList.add('card-text');
        bodyText.textContent = newTodoObj.body;
        cardBody.appendChild(bodyText);
        // Создаём кнопку изменения статуса записи
        const stateButton = document.createElement('button');
        stateButton.type = 'button';
        stateButton.classList.add('btn', 'btn-warning', 'mr-1'); // margin right-1 - костыль, почему-то без него кнопки слипаются
        stateButton.textContent = 'Изменить статус';
        stateButton.addEventListener('click', () => changeTodoState(newTodoObj.id));
        cardBody.appendChild(stateButton);
        // Создаём кнопку удаления записи
        const deleteButton = document.createElement('button');
        deleteButton.type = 'button';
        deleteButton.classList.add('btn', 'btn-danger');
        deleteButton.textContent = 'Удалить';
        deleteButton.addEventListener('click', () => deleteTodo(newTodoObj.id));
        cardBody.appendChild(deleteButton);
        // Создаём футер карточки
        const cardFooter = document.createElement('div');
        cardFooter.classList.add('card-footer');
        cardFooter.textContent = 'Статус: Не выполнено';
        // Формируем карточку
        newListItem.appendChild(cardHeader);
        newListItem.appendChild(cardBody);
        newListItem.appendChild(cardFooter);
        // Добавляем карточку в документ
        todosList.appendChild(newListItem);
        // Чистим форму
        document.querySelector('#form-input-title').value = '';
        document.querySelector('#form-input-body').value = '';
        // В зависимости от выбранной фильтрации показываем/скрываем её
        if (filterDoneDone.checked)
            newListItem.style.display = 'none';
    } catch (e) {
        alert(e.message);
    }
    // Убираем загрузку
    setLoading(false);
});

// Функция изменения статуса записи
async function changeTodoState(todoId) {
    // Выставляем загрузку
    //setLoading(true);
    // Изменяем статус
    try {
        const response = await fetch(`/api/todos/${todoId}/change_state`, {
            method: 'POST',
            credentials: 'same-origin',
        });
        if (!response.ok) {
            alert(await response.text());
            if (response.status === 401) {
                window.location = '/login';
            }
            //setLoading(false);
            return;
        }
        let todoLi = document.querySelector(`li[data-todo-id="${todoId}"]`);
        // Меняем статус карточки
        let statusText = isTodoDone(todoLi) ? 'Статус: Не выполнено' : 'Статус: Выполнено';
        todoLi.getElementsByClassName('card-footer')[0].textContent = statusText;
        // Меняем цвет карточки: просто "переключаем" классы. 
        // Toggle добавляет класс, если его нет, и удаляет класс, если он есть.
        todoLi.classList.toggle('bg-info');
        todoLi.classList.toggle('text-light');
        todoLi.classList.toggle('bg-light');
        // В зависимости от выбранной фильтрации скрываем карточку
        if (isTodoDone(todoLi) && filterDoneNot.checked || !isTodoDone(todoLi) && filterDoneDone.checked)
            todoLi.style.display = 'none';
    } catch (e) {
        alert(e.message);
    }
    // Убираем загрузку
    //setLoading(false);
}

// Функция удаления записи
async function deleteTodo(todoId) {
    // Выставляем загрузку
    //setLoading(true);
    // Удаляем
    try {
        const response = await fetch(`/api/todos/${todoId}/`, {
            method: 'DELETE',
            credentials: 'same-origin',
        });
        if (!response.ok) {
            alert(await response.text());
            if (response.status === 401) {
                window.location = '/login';
            }
            //setLoading(false);
            return;
        }
        // Удаляем из списка
        document.querySelector(`li[data-todo-id="${todoId}"]`).remove();
    } catch (e) {
        alert(e.message);
    }
    // Убираем загрузку
    //setLoading(false);
}

// -----------------ФИЛЬТРАЦИЯ-----------------------

// Обработчик события на отображение всех записей
filterDoneAll.addEventListener('change', (event) => {
    if (!filterDoneAll.checked)
        return;
    // Отображаем все
    for (var i = 0; i < todosList.children.length; i++)
        todosList.children[i].style.display = '';
});

// Обработчик события на отображение выполненных записей
filterDoneDone.addEventListener('change', (event) => {
    if (!filterDoneDone.checked)
        return;
    // Скрываем все невыполненные и отображаем выполненные
    for (var i = 0; i < todosList.children.length; i++)
        if (isTodoDone(todosList.children[i]))
            todosList.children[i].style.display = '';
        else
            todosList.children[i].style.display = 'none';
});

// Обработчик события на отображение невыполненных заданий
filterDoneNot.addEventListener('change', (event) => {
    if (!filterDoneNot.checked)
        return;
    // Скрываем все выполненные
    for (var i = 0; i < todosList.children.length; i++)
        if (!isTodoDone(todosList.children[i]))
            todosList.children[i].style.display = '';
        else
            todosList.children[i].style.display = 'none';
});

// Получаем чекбоксы фильтрации, и список записей
const filterDoneAll = document.querySelector('#filter-done-all');
const filterDoneDone = document.querySelector('#filter-done-done');
const filterDoneNot = document.querySelector('#filter-done-not');
const todosListElem = document.querySelector('#todos-list'); // todosList уже есть в ajax.js => надо другое имя (а можно просто юзать тот)

// Проверка карточки задания на выполненность
function isTodoDone(todoLi) {
    // Определеяем по цевету карточки
    return todoLi.classList.contains('bg-info');
}

// Обработчик события на отображение всех записей
filterDoneAll.addEventListener('change', (event) => {
    if (!filterDoneAll.checked)
        return;
    // Отображаем все
    for (var i = 0; i < todosListElem.children.length; i++)
        todosListElem.children[i].style.display = '';
});

// Обработчик события на отображение выполненных записей
filterDoneDone.addEventListener('change', (event) => {
    if (!filterDoneDone.checked)
        return;
    // Скрываем все невыполненные и отображаем выполненные
    for (var i = 0; i < todosListElem.children.length; i++)
        if (isTodoDone(todosListElem.children[i]))
            todosListElem.children[i].style.display = '';
        else
            todosListElem.children[i].style.display = 'none';
});

// Обработчик события на отображение невыполненных заданий
filterDoneNot.addEventListener('change', (event) => {
    if (!filterDoneNot.checked)
        return;
    // Скрываем все выполненные
    for (var i = 0; i < todosListElem.children.length; i++)
        if (!isTodoDone(todosListElem.children[i]))
            todosListElem.children[i].style.display = '';
        else
            todosListElem.children[i].style.display = 'none';
});
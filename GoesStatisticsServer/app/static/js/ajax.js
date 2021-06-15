// Сохраняем в переменные список записей, блок-загрузочный экран и блок, содержащий контент списка записей, поля ввода фильтров
const statsList = document.querySelector('#stats-list');
const loadingDiv = document.querySelector('#loading');
const statsListWrapper = document.querySelector('#stats-list-wrapper');
const studentNameInput = document.querySelector('#studentname-filter-input');
const studentGroupInput = document.querySelector('#studentgroup-filter-input');
const deleteAllButton = document.querySelector('#delete-all-button');
const clearAllFiltersButton = document.querySelector('#clear-all-filters-button')


// Функция включения/отключения загрузочного экрана
function setLoading(isLoading) {
    // В зависимости от значения isLoading отображаем или загрузочный экран, или контент списка
    statsListWrapper.style.display = isLoading ? 'none' : '';
    loadingDiv.style.display = isLoading ? '' : 'none';
}

// -----------------------AJAX--------------------------

// Удалить запись с заданным идентификатором
async function deleteStat(recordId) {
    // Выставляем загрузку
    //setLoading(true);
    // Удаляем
    try {
        const response = await fetch(`/api/stats/${recordId}/`, {
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
        document.querySelector(`li[data-stat-id="${recordId}"]`).remove();
        // Добавляем надпись "Нет записей", если была удалена последняя
        if (statsList.children.length == 0)
            statsList.innerHTML = 'Нет записей';
    } catch (e) {
        alert(e.message);
    }
    // Убираем загрузку
    //setLoading(false);
}

// Удалить все записи
async function deleteAllStats() {
    // Выставляем загрузку
    //setLoading(true);
    // Удаляем
    try {
        const response = await fetch(`/api/stats`, {
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
        // Удаляем все записи из списка
        while(statsList.firstChild) 
            statsList.removeChild(statsList.firstChild);
        // Добавляем надпись "Нет записей"
        statsList.innerHTML = 'Нет записей';
    } catch (e) {
        alert(e.message);
    }
    // Убираем загрузку
    //setLoading(false);
}

// Обработчик на событие нажатия кнопки удаления всех записей
deleteAllButton.addEventListener('click', (event) => {
    deleteAllStats();
});


// -----------------ФИЛЬТРАЦИЯ-----------------------

// Отфильтровать отображаемый список записей на основании введённых в studentNameInput и studentGroupInput
// имени и группы, без учёта регистра.
function filterRecords() {
    // Получаем введённое имя, удаляем лишние пробелы, формируем шаблон для регулярного выражения для проверки имени (без учёта регистра)
    // Если имя пустое - допустимо любое имя
    let studentName = studentNameInput.value.trim();
    let nameTemplate = studentName != '' ? `Имя: .*${studentName}.*` : 'Имя: .*';
    let regexName = new RegExp(nameTemplate, "i");
    // Получаем введённую группу, удаляем лишние пробелы, формируем шаблон для регулярного выражения для проверки группы (без учёта регистра)
    // Если группа пустая - допустима любая группа
    let studentGroup = studentGroupInput.value.trim();
    let groupTemplate = studentGroup != '' ? `Группа \\(класс\\): .*${studentGroup}.*` : 'Группа \\(класс\\): .*';
    let regexGroup = new RegExp(groupTemplate, "i");
    // Скрываем в списке все записи, имя и группа в которых не удовлетворяют соответствующим регулярным выражениям
    for (var i = 0; i < statsList.children.length; i++) {
        // Получаем идентификатор записи
        let recordId = statsList.children[i].getAttribute('data-stat-id');
        // Получаем текст из параграфов с именем и группой и сравниваем с нужными шаблонами. И скрываем/показываем нужные элементы
        let curStudentName = document.querySelector(`#studentname-${recordId}`).innerHTML.trim();
        let curStudentGroup = document.querySelector(`#studentgroup-${recordId}`).innerHTML.trim();
        if (regexName.test(curStudentName) && regexGroup.test(curStudentGroup))     
            statsList.children[i].style.display = '';
        else
            statsList.children[i].style.display = 'none';
    }
}

// Обработчик события изменения имени в фильтре
studentNameInput.addEventListener('input', (event) => {
        filterRecords();
});

// Обработчик события изменения группы в фильтре
studentGroupInput.addEventListener('input', (event) => {
    filterRecords();
});

// Обработчик на событие нажатия кнопки сброса фильтров
clearAllFiltersButton.addEventListener('click', (event) => {
    studentNameInput.value = '';
    studentGroupInput.value = '';
    filterRecords();
});

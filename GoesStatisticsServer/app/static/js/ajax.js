// Сохраняем в переменные форму добавления записи, а также список записей, блок-загрузочный экран и блок, содержащий контент списка записей
const statsList = document.querySelector('#stats-list');
const loadingDiv = document.querySelector('#loading');
const statsListWrapper = document.querySelector('#stats-list-wrapper');
const studentNameInput = document.querySelector('#studentname-filter-input');
const studentGroupInput = document.querySelector('#studentgroup-filter-input');
const deleteAllButton = document.querySelector('#delete-all-button');


// Функция включения/отключения загрузочного экрана
function setLoading(isLoading) {
    // В зависимости от значения isLoading отображаем или загрузочный экран, или контент списка
    statsListWrapper.style.display = isLoading ? 'none' : '';
    loadingDiv.style.display = isLoading ? '' : 'none';
}

// -----------------------AJAX--------------------------

// Функция удаления записи
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

async function deleteAllStats() {
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
}

deleteAllButton.addEventListener('click', (event) => {
    deleteAllStats();
});


// -----------------ФИЛЬТРАЦИЯ-----------------------

function filterRecords() {
    let studentName = studentNameInput.value.trim();
    let nameTemplate = studentName != '' ? `Имя: .*${studentName}.*` : 'Имя: .*';
    let regexName = new RegExp(nameTemplate, "i");
    let studentGroup = studentGroupInput.value.trim();
    let groupTemplate = studentGroup != '' ? `Группа \\(класс\\): .*${studentGroup}.*` : 'Группа \\(класс\\): .*';
    let regexGroup = new RegExp(groupTemplate, "i");
    for (var i = 0; i < statsList.children.length; i++) {
        let recordId = statsList.children[i].getAttribute('data-stat-id');
        let curStudentName = document.querySelector(`#studentname-${recordId}`).innerHTML.trim();
        let curStudentGroup = document.querySelector(`#studentgroup-${recordId}`).innerHTML.trim();
        if (regexName.test(curStudentName) && regexGroup.test(curStudentGroup)) {            
            statsList.children[i].style.display = '';
        }
        else {
            statsList.children[i].style.display = 'none';
        }
    }
}

// Обработчик события на ввод конкретного имени
studentNameInput.addEventListener('input', (event) => {
        filterRecords();
});

// Обработчик события на ввод конкретной группы
studentGroupInput.addEventListener('input', (event) => {
    filterRecords();
});

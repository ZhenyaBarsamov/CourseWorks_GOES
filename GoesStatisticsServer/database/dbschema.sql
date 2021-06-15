--SQL-скрипт создания БД для хранения записей статистики

CREATE TABLE "statistics"
(
	record_id INTEGER PRIMARY KEY,
	student_name TEXT NOT NULL,
	student_group TEXT NOT NULL,
	problem_name TEXT NOT NULL,
	example_name TEXT NOT NULL,
	example_description TEXT NOT NULL,
	statistics_body TEXT NOT NULL,
	total_errors_count INTEGER NOT NULL,
	necessary_errors_count INTEGER NOT NULL,
	mark INTEGER NOT NULL,
	record_time	TIMESTAMP NOT NULL
);

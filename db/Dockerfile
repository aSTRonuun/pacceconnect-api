FROM mysql:5.7.22
EXPOSE 3306
COPY ./Service/Adapters/Data/Data/Migrations_SQL/ /home/database/migrations/
COPY ./Service/Adapters/Data/Data/CI/init_database.sh /docker-entrypoint-initdb.d/init_database.sh
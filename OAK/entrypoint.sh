#!/bin/bash

set -e
run_cmd="dotnet OAK.WebApi.dll"

# Wait for the database to be available
until pg_isready -h $DB_HOST -p $DB_PORT -U $DB_USER; do
  >&2 echo "Postgres is unavailable - sleeping"
  sleep 1
done

>&2 echo "Postgres is up - executing command"
exec $run_cmd

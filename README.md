# Metallum

Metal bands management suite.

## Setup

The setup is fairly simple. You will need a PostgreSQL database with the following installed extensions:

- `CREATE EXTENSION "unaccent";`
- `CREATE EXTENSION "uuid-ossp";`

Then you will need to run the scripts in the following directory to initialize the database:

`backend/src/Metallum.Infrastructure/Scripts/`

You can then set the connection to the database by right-clicking on the **WorkerService** and **Web** projects, then clicking `Manage User Secrets` and adding the following property and replacing the `{variables}`:

```json
"ConnectionStrings": {
  "MetallumDbContext": "User Id={username};Password={password};Host={host_or_ip_address};Port={port};Database={database_name};"
}
```

## Importing bands

You can import the bands from Canada by running the **Metallum.ETL.WorkerService** project.

## Browsing bands

The two following endpoints in the **Metallum.Web** project currently allow to browse bands:

- `GET /bands`: returns a sorted and paged list of bands. The bands can be filtered by the `Deleted` flag, their `Status` and searched in the following fields: `Genre`, `Location` and `Name`.
- `GET /bands/quebec/random`: returns a list (default size of 10) of randomly-selected bands from Quebec province. All selected bands won't be deleted and their status will be `Active`.

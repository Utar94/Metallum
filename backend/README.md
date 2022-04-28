# Metallum API

Metal bands management Web API.

## Migrations

This project is setup to use migrations. All the commands below must be executed in the **Metallum.Infrastructure** project directory.

### Create a migration

To create a new migration, execute the following command. Do not forget to specify a name!

`dotnet ef migrations add <YOUR_MIGRATION_NAME> --startup-project ../Metallum.ETL.WorkerService`

### Remove a migration

To remove the latest new migration, execute the following command.

`dotnet ef migrations remove --startup-project ../Metallum.ETL.WorkerService`

### Generate a script

To generate a script, execute the following command. You can optionally specify a _from_ migration name.

`dotnet ef migrations script <FROM_MIGRATION_NAME>? --startup-project ../Metallum.ETL.WorkerService`

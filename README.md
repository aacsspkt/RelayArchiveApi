# RelayArchiveApi

This is asp.net6 api built using entity framework core for archiving relay infos relayed by specialize relayer for zebec bridge contract

## Available Routes:

| Routes                                                                                                              | Methods | Descriptions                                                            |
| ------------------------------------------------------------------------------------------------------------------- | ------- | ----------------------------------------------------------------------- |
| /                                                                                                                   | GET     | For Health Check                                                        |
| /api/v1/relayinfos?chain={string}&emitterAddressHex={string}&sequence={number}&limit={number:1-100}&offset={number} | GET     | Gets all relay infos                                                    |
| /api/v1/relayinfos/{chain}/{emitterAddressHex}/{sequence}                                                           | GET     | Gets relay info based on chain, emitterAddressHex and sequence in param |
| /api/v1/relayinfos                                                                                                  | POST    | Saves relay info                                                        |
| /api/v1/relayinfos/{chain}/{emitterAddressHex}/{sequence}                                                           | DELETE  | Deletes relay info                                                      |

## Usage

### Using docker:

Clone the repository and change directory to `relayarchive` where `docker-compose.yml` file is present.

Run:

```bash
docker-compose up
```

Open http://localhost:5546 in your browser. It will open `pgadmin`. Login with username and password specified in `docker-compose.yml` file.

Add new server connection with same database server configuration specified in `docker-compose.yml` file

Run scripts in `RelayArchive.sql` file using query tool. It will create required tables in the schema.

### Without docker

Make sure you have dotnet sdk v6 and postgres installed in your machine.

Go to `RelayArchive.Api` directory and open terminal in that directory.

Run:

```bash
dotnet restore
```

Make changes in value of key `DefaultConnection` in `appsettings.json` file for database connection according to your postgres server configuration in your machine.

Install `dotnet-ef` tool by running this command:

```bash
dotnet tool install --global dotnet-ef
```

After installation, run:

```bash
dotnet ef database update
```

It will update your database using migrations files.

Then to run the api application, run:

```bash
dotnet run
```

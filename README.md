# Scripter & ScriptEngine Library
 
 This tool allows you to either use the console app to extract inserts, updates, deletes and selects from a database into a scripting file. I use this tool a lot to copy settings and other information from one database to another when I do not have access to the target database.
 The script will pull the actual values (based on the provided where clause) from the source database.
 
## Scripter tool
 
  The scripter tool supports a number of verbs and options to allow you to create these scripts right in the command line (for Windows, MAC, etc...). The following verbs and options are supported:
  
### inserts

 The inserts verb allows you to create insert statements from a set of records. the tool will also add an "if exists" check to the script for each record so that it will not cause any primary key conflicts.
 
 Available options:
 
  -x, --exists              checks if the record already exists

  -c, --connectionstring    connection string to database (you can use this or set the separate items with the info below)

  -s, --server              server name of the connection (instead of specifying connection string)

  -d, --database            database name of the connection (instead of specifying connection string)

  -i, --integrated          connection supports integrated security (instead of specifying connection string)

  -u, --user                user name for the connection (instead of specifying connection string)

  -p, --password            password for the connection (instead of specifying connection string)

  -t, --tablename           Database Table Name

  -k, --keys                List of primary keys of this table

  -w, --whereclause         where clause to filter output

  -o, --orderclause         order clause to sort output

  -l, --location            file location to write to

  --help                    Display this help screen.

  --version                 Display version information.

### updates

 The inserts verb allows you to create update statements from a set of records. the tool will also add an "if exists" check to the script for each record so that it will not cause any primary key conflicts.
 
 Available options:
  
  -x, --exists              checks if the record already exists

  -c, --connectionstring    connection string to database (you can use this or set the separate items with the info below)

  -s, --server              server name of the connection (instead of specifying connection string)

  -d, --database            database name of the connection (instead of specifying connection string)

  -i, --integrated          connection supports integrated security (instead of specifying connection string)

  -u, --user                user name for the connection (instead of specifying connection string)

  -p, --password            password for the connection (instead of specifying connection string)

  -t, --tablename           Database Table Name
  
  -f, --fields              list of fields to update (if none provided, all fields will be scripted for update)

  -k, --keys                List of primary keys of this table

  -w, --whereclause         where clause to filter output

  -o, --orderclause         order clause to sort output

  -l, --location            file location to write to

  --help                    Display this help screen.

  --version                 Display version information.
  
 ### deletes

 The inserts verb allows you to create delete statements from a set of records. the tool will also add an "if exists" check to the script for each record so that it will not cause any primary key conflicts.
 
 Available options:
 
  -x, --exists              checks if the record already exists

  -c, --connectionstring    connection string to database (you can use this or set the separate items with the info below)

  -s, --server              server name of the connection (instead of specifying connection string)

  -d, --database            database name of the connection (instead of specifying connection string)

  -i, --integrated          connection supports integrated security (instead of specifying connection string)

  -u, --user                user name for the connection (instead of specifying connection string)

  -p, --password            password for the connection (instead of specifying connection string)

  -t, --tablename           Database Table Name

  -k, --keys                List of primary keys of this table

  -w, --whereclause         where clause to filter output

  -o, --orderclause         order clause to sort output

  -l, --location            file location to write to

  --help                    Display this help screen.

  --version                 Display version information.

### selects

 The inserts verb allows you to create select statements from a set of records. the tool will also add an "if exists" check to the script for each record so that it will not cause any primary key conflicts. By default, the engine hard codes the values like <code>select 'xxx' as ColA ...</code> but you can also create a simple select statement by setting the --hardcode flag to false.
 
 Available options:
 
  -h, --hardcode            hard codes the select values (Default is true)

  -c, --connectionstring    connection string to database (you can use this or set the separate items with the info below)

  -s, --server              server name of the connection (instead of specifying connection string)

  -d, --database            database name of the connection (instead of specifying connection string)

  -i, --integrated          connection supports integrated security (instead of specifying connection string)

  -u, --user                user name for the connection (instead of specifying connection string)

  -p, --password            password for the connection (instead of specifying connection string)

  -t, --tablename           Database Table Name

  -k, --keys                List of primary keys of this table

  -w, --whereclause         where clause to filter output

  -o, --orderclause         order clause to sort output

  -l, --location            file location to write to

  --help                    Display this help screen.

  --version                 Display version information.
  
 ## Script Engine library
 
  You can use the script library to generate these statements in code if needed.

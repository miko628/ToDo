# ToDo
a simple WPF application that allows you to make a to do list and synchronise it with google calendar.

# Technology
- C#
- WPF
- MVVM pattern
- MySql

## NuGet Packages
- Google.Apis.Calendar.v3
- MahApps.Metro
- MySql.Data
- MySqlConnector
- System.Data.SqlClient

# Setup

Installation:
``` 
git clone https://github.com/miko628/ToDo.git
```
## To use this application you need MySql database with imported data from "dbcreate.txt"
in App.config set up configuration information for connecting with MySql database

## To use google calendar application you need to create Google Cloud project with Google Calendar Api
then put credentials.json in "Utility" folder

using System;
using System.Collections.Generic;
using CommandLine;

namespace Scripter
{
	public class BaseOptions
	{
		[Option('c', "connectionstring", HelpText = "connection string to database", SetName = "manual")]
		public string ConnectionString { get; set; } = "";

		[Option('s', "server", HelpText = "server name of the connection", SetName = "builder")]
		public string Server { get; set; } = "";
		[Option('d', "database", HelpText = "database name of the connection", SetName = "builder")]
		public string Database { get; set; } = "";
		[Option('i', "integrated", HelpText = "connection supports integrated security", SetName = "builder")]
		public bool IntegratedSecurity { get; set; } = false;
		[Option('u', "user", HelpText = "user name for the connection", SetName = "builder")]
		public string Username { get; set; } = "";
		[Option('p', "password", HelpText = "password for the connection", SetName = "builder")]
		public string Password { get; set; } = "";


		[Option('t', "tablename", HelpText = "Database Table Name")]
		public string TableName { get; set; } = "";

		[Option('k', "keys", HelpText = "List of primary keys of this table")]
		public IEnumerable<string> Keys { get; set; } = new List<string>();

		[Option('w', "whereclause", HelpText = "where clause to filter output")]
		public string WhereClause { get; set; } = "";

		[Option('o', "orderclause", HelpText = "order clause to sort output")]
		public string OrderClause { get; set; } = "";

		[Option('l', "location", HelpText = "file location to write to")]
		public string FileName { get; set; } = "";

		
	}

	[Verb("selects", HelpText="creates select statements")]
	public class SelectsOptions : BaseOptions
	{
		[Option('h', "hardcode", HelpText = "hard code values")]
		public bool HardCodeValues { get; set; } = true;

		[Option('v', "replace", HelpText = "a key value pair list of fieldnames and their replacements")]
		public IEnumerable<string> ValueReplacements { get; set; } = new List<string>();

	}

	[Verb("inserts", HelpText="creates insert statements")]
	public class InsertsOptions : BaseOptions
	{
		[Option('x', "exists", HelpText = "checks if exists")]
		public bool CheckIfExists { get; set; } = false;

		[Option('v', "replace", HelpText = "a key value pair list of fieldnames and their replacements")]
		public IEnumerable<string> ValueReplacements { get; set; } = new List<string>();

	}

	[Verb("updates", HelpText="creates update statements")]
	public class UpdatesOptions : BaseOptions
	{
		[Option('f', "fields", HelpText = "list of fields to update")]
		public IEnumerable<string> UpdateFields { get; set; } = new List<string>();

		[Option('x', "exists", HelpText = "checks if exists")]
		public bool CheckIfExists { get; set; } = true;

		[Option('v', "replace", HelpText = "a key value pair list of fieldnames and their replacements")]
		public IEnumerable<string> ValueReplacements { get; set; } = new List<string>();

	}

	[Verb("deletes", HelpText="creates delete statements")]
	public class DeletesOptions : BaseOptions
	{
		[Option('x', "exists", HelpText = "checks if exists")]
		public bool CheckIfExists { get; set; } = true;
	}
}


using CommandLine;
using steepvalley.ScriptEngineLibrary;

namespace Scripter
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Console.WriteLine("Scripter V1.0 - (c) 2022 - Steepvalley, Inc.");
            Console.WriteLine();

            int result = 0;

            
            Parser.Default.ParseArguments<SelectsOptions, InsertsOptions, UpdatesOptions, DeletesOptions>(args)
                .MapResult(
                    (SelectsOptions options) => RunCreateSelectStatements(options),
                    (InsertsOptions options) => RunCreateInsertStatements(options),
                    (UpdatesOptions options) => RunCreateUpdateStatements(options),
                    (DeletesOptions options) => RunCreateDeletesStatements(options),
                    errors => 1);


            return result;
        }

        private static int RunCreateSelectStatements(SelectsOptions options)
        {
            if (CheckConnection(options) == 1) { return 1; }

            var replacements = ListToDict(options.ValueReplacements);
            var output = ScriptEngine.CreateSelectStatement(
                            options.TableName,
                            options.Keys.ToArray(),
                            options.WhereClause,
                            options.OrderClause,
                            GetConnection(options),
                            options.HardCodeValues,
                            replacements);

            return WriteToFile(options.FileName, output);
        }

        private static int RunCreateInsertStatements(InsertsOptions options)
        {
            if (CheckConnection(options) == 1) { return 1; }
            var replacements = ListToDict(options.ValueReplacements);
            var output = ScriptEngine.CreateInsertStatement(
                            options.TableName,
                            options.Keys.ToArray(),
                            options.WhereClause,
                            options.OrderClause,
                            GetConnection(options),
                            options.CheckIfExists,
                            replacements);

            return WriteToFile(options.FileName, output);

        }

        private static int RunCreateUpdateStatements(UpdatesOptions options)
        {
            if (CheckConnection(options) == 1) { return 1; }
            var replacements = ListToDict(options.ValueReplacements);
            var output = ScriptEngine.CreateUpdateStatement(
                            options.TableName,
                            options.Keys.ToArray(),
                            options.UpdateFields.ToArray(),
                            options.WhereClause,
                            options.OrderClause,
                            GetConnection(options),
                            options.CheckIfExists,
                            replacements);
            return WriteToFile(options.FileName, output);
        }

        private static int RunCreateDeletesStatements(DeletesOptions options)
        {
            if (CheckConnection(options) == 1) { return 1; }
            var output = ScriptEngine.CreateDeleteStatement(
                            options.TableName,
                            options.Keys.ToArray(),
                            options.WhereClause,
                            options.OrderClause,
                            GetConnection(options),
                            options.CheckIfExists);

            return WriteToFile(options.FileName, output);
        }

        private static int WriteToFile(string filename, string? output)
        {
            try
            {
                //System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filename));
                System.IO.File.WriteAllText(filename, output);
                Console.WriteLine($"Script File has been created at {filename}");
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
            
        }

        private static int CheckConnection(BaseOptions options)
        {
            try
            {
                if (ScriptEngine.CanConnect(GetConnection(options)))
                {
                    Console.WriteLine("Connection Successful");
                    return 0;
                }
                else
                {
                    Console.WriteLine($"Cannot connect to specified connection {GetConnection(options)}");
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 1;
            }
        }

        public static string GetConnection(BaseOptions options)
        {
            var retval = "";
            if (string.IsNullOrEmpty(options.ConnectionString))
            {
                retval = ScriptEngine.BuildConnectionString(
                              options.Server,
                              options.Database,
                              options.IntegratedSecurity,
                              options.Username,
                              options.Password);
            }
            else
            {
                retval = options.ConnectionString;
            }
            return retval;
        }

        private static Dictionary<string, string> ListToDict(IEnumerable<string> list)
        {
            var dict = new Dictionary<string, string>();
            if (list?.Count() == 0) { return dict; }

            if (list?.Count() % 2 == 0)
            {
                for (int i = 0; i < list.Count(); i += 2)
                {
                    dict.Add(list.ElementAt(i), list.ElementAt(i + 1));
                }
            }

            return dict;
        }
    }
}
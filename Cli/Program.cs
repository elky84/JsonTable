// See https://aka.ms/new-console-template for more information

using JsonTable;

try
{
    MasterTable.Load("Cli.Table");
    Console.WriteLine("Load Completed");
}
catch (Exception exception)
{
    Console.WriteLine($"Unhandled exception. <Reason:{exception.Message}> <StackTrace:{exception.StackTrace}>");
}

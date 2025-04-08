// See https://aka.ms/new-console-template for more information
using DatabaseConnLibrary;
using MySql.Data.MySqlClient;

var db = new DatabaseConnection("sem3", "root", "root");

var table = db.GetTableData("test");

Console.WriteLine(table.Rows.Count);
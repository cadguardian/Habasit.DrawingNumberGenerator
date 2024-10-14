// See https://aka.ms/new-console-template for more information
using EmailParserTool;

Console.WriteLine("Paste drawing request email to generate drawing number:");

EmailParser emailParser = EmailParser.CreateEmailParser();

string emailBody = Console.ReadLine();

string drawingNumber = emailParser.ParseEmail(emailBody)?.DrawingNumber;

Console.WriteLine(drawingNumber);
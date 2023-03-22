using ConceptsPOO;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

try
{
    Console.WriteLine(new Date(2023, 3, 21));
    Console.WriteLine(new Date(1974, 9, 23));
    Console.WriteLine(new Date(2024, 2, 29));
}
catch (Exception error)
{
    Console.WriteLine(error.Message);
}





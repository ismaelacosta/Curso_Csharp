using ConceptsPOO;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Employee e1 = new SalaryEmployee()
{
    Id = 1010,
    FirstName = "Sandra",
    LastName = "Morales",
    BirthDate = new Date(1990,5, 23),
    HiringDate = new Date(2022, 1,15),
    IsActive = true,
    Salary = 1815453.45M,
};

Employee e2 = new CommissionEmployee()
{
    Id = 2020,
    FirstName = "Patricia",
    LastName = "Gutierrez",
    BirthDate = new Date(1995,5, 23),
    HiringDate = new Date(2022, 2,5),
    IsActive = true,
    CommissionPercentaje = 0.03F,
    Sales = 32000000M
};

Console.WriteLine(e1);
Console.WriteLine(e2);



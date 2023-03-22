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

Employee e3 = new HourlyEmployee(){
    Id = 3030,
    FirstName = "Gonzalo",
    LastName = "Cardona",
    BirthDate = new Date(1995,5, 23),
    HiringDate = new Date(2022, 2,5),
    IsActive = true,
    HourValue = 12356.56M,
    Hours = 123.5F,
};

Employee e4 = new BaseCommissionEmployee(){
    Id = 4040,
    FirstName = "Jazmin",
    LastName = "Salazar",
    BirthDate = new Date(1995,5, 23),
    HiringDate = new Date(2022, 2,5),
    IsActive = true,
    CommissionPercentaje = 0.015F,
    Sales = 58000000M,
    Base = 860678.45M,
};

/* Console.WriteLine(e1);
Console.WriteLine(e2);
Console.WriteLine(e3);
Console.WriteLine(e4); */

ICollection<Employee> employees = new List<Employee>(){
    e1, e2, e3, e4,
};

decimal payroll = 0 ;

foreach (Employee employee in employees)
{
    Console.WriteLine(employee);
    payroll += employee.GetValueToPay();
}

Console.WriteLine("             ==================");
Console.WriteLine($"TOTAL: {$"{payroll:C2}",15}");

Invoice invoice = new Invoice(){
    Description = "IPhone 13",
    Id = 1,
    Price = 5300000M,
    Quantity = 6,
};

Invoice invoice2 = new Invoice(){
    Description = "Posta Premium",
    Id = 2,
    Price = 32000M,
    Quantity = 17.5F,
};

Console.WriteLine(invoice);
Console.WriteLine(invoice2);






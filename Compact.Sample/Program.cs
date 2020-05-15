using Compact.Core;
using System;

namespace Compact.Sample
{
    [DatabaseTable(TableName = "Cars")]
    class Car : DatabaseObject
    {
        [DatabaseColumn(ColumnName = "Make", DataType = "VARCHAR(32)")]
        public string Make { get; set; }

        [DatabaseColumn(ColumnName = "Year", DataType = "INTEGER")]
        public int Year { get; set; }
    }

    class Program
    {
        static void Poop()
        {
            CompactConfiguration.RegisterDatabase("Server=localhost;Initial Catalog=TestDb;Integrated Security=true;", true);
            var carRepository = new DatabaseObjectRepository<Car>();
            var car = new Car
            {
                Make = "Honda",
                Year = 1997
            };
            carRepository.InsertOrUpdate(car);
        }

        static void Main(string[] args)
        {
            Poop();
            Console.ReadLine();
        }
    }
}

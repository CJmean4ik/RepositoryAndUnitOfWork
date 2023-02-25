namespace DataBase.Entitys
{
    internal class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public string? Office { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public User()
        {

        }

        /// <summary>
        /// Используется для вставки нового пользователя
        /// </summary>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <param name="office"></param>
        /// <param name="age"></param>
        /// <param name="salary"></param>
        public User(string? name, string? position = "", string? office = "", int age = 0, int salary = 0) 
        {
            Name = name;
            Position = position;
            Office = office;
            Age = age;
            Salary = salary;
        }

        /// <summary>
        /// Используется для обновления существуещего пользователя по Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <param name="office"></param>
        /// <param name="age"></param>
        /// <param name="salary"></param>
        public User(int id, string? name, string? position = "", string? office = "", int age = 0, int salary = 0)
        : this(name,position,office,age,salary)
        {
            Id = id;
        }
    }
}

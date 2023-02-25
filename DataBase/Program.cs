using DataBase.Entitys;
using DataBase.Repository.ImpelementRepository;

namespace DataBase
{
    internal class Program
    {

        static async Task Main(string[] args)
        {

            string connString = ConfigureConnection.GetConnectionStringFromJSON();
            using (UnitOfWork unitOfWork = new UnitOfWork(connString))
            {
                UserRepository userRepository = new UserRepository(unitOfWork);
                await userRepository.DeleteAsync(13);

                unitOfWork.SaveDbChanges();

                foreach (var item in await userRepository.GetAllEntitiesAsync())
                {
                    Console.WriteLine(item.Id + " " + item.Name);
                }
            }
            Console.WriteLine();
        }

        public static void PrintExeptions(object obj, EventArgsExcep ea)
        {
            Console.WriteLine(ea.Warning);
            if (ea.exception != null)
                Console.WriteLine(ea.exception);


        }

    }
}
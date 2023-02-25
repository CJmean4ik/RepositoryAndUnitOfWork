using DataBase.Entitys;
using System.Data;
using System.Data.SqlClient;

namespace DataBase.Repository.ImpelementRepository
{
    internal class UserRepository : IUserRepository
    {

        #region Fields

        private List<User> Users;
        private bool _dbChanged = false;
        private UnitOfWork _unitOfWork;
        #endregion
        public UserRepository(UnitOfWork unitOfWork)
        {
            Users = new List<User>();
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateAsync(User entity)
        {
            if (entity == null) throw new NullReferenceException();

            using (SqlCommand sqlCommand = _unitOfWork.CreateCommand())
            {
                sqlCommand.CommandText = "InsertNewEmployee";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter
                {
                    Direction = ParameterDirection.Input,
                    DbType = DbType.String,
                    ParameterName = "@Name",
                    Value = entity.Name
                });
                sqlCommand.Parameters.Add(new SqlParameter
                {
                    Direction = ParameterDirection.Input,
                    DbType = DbType.String,
                    ParameterName = "@Position",
                    Value = entity.Position
                });
                sqlCommand.Parameters.Add(new SqlParameter
                {
                    Direction = ParameterDirection.Input,
                    DbType = DbType.String,
                    ParameterName = "@Office",
                    Value = entity.Office,
                });
                sqlCommand.Parameters.Add(new SqlParameter
                {
                    Direction = ParameterDirection.Input,
                    DbType = DbType.Int32,
                    ParameterName = "@Age",
                    Value = entity.Age,
                });
                sqlCommand.Parameters.Add(new SqlParameter
                {
                    Direction = ParameterDirection.Input,
                    DbType = DbType.Int32,
                    ParameterName = "@Salary",
                    Value = entity.Salary,
                });
                sqlCommand.Parameters.Add(new SqlParameter
                {
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.ReturnValue,
                    ParameterName = "@@Identity"
                });


                await sqlCommand.ExecuteNonQueryAsync();
                _dbChanged = true;

                return true;
            }
        }
        public async Task<List<User>> GetAllEntitiesAsync()
        {
            if (Users.Count > 0 && !_dbChanged) return Users;

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandText = "GetAllEmployee";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                var reader = await sqlCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var user = new User();
                    user.Id = Convert.ToInt32(reader["Id"]);
                    user.Name = reader["Name"].ToString();
                    user.Position = reader["Position"].ToString();
                    user.Office = reader["Office"].ToString();
                    user.Age =  int.TryParse(reader["Age"].ToString(),out int age)  == true ? age : 0;
                    user.Salary = int.TryParse(reader["Salary"].ToString(), out int salary) == true ? salary : 0;
                    Users.Add(user);
                }
            }

            return Users;
        }
        public async Task<bool> UpdateAsync(User entity)
        {
            using (SqlCommand sqlCommand = _unitOfWork.CreateCommand())
            {
                sqlCommand.CommandText = "Update";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter
                {
                    Direction = ParameterDirection.Input,
                    DbType = DbType.String,
                    ParameterName = "@Name",
                    Value = entity.Name
                });
                sqlCommand.Parameters.Add(new SqlParameter
                {
                    Direction = ParameterDirection.Input,
                    DbType = DbType.String,
                    ParameterName = "@Position",
                    Value = entity.Position
                });
                sqlCommand.Parameters.Add(new SqlParameter
                {
                    Direction = ParameterDirection.Input,
                    DbType = DbType.String,
                    ParameterName = "@Office",
                    Value = entity.Office,
                });
                sqlCommand.Parameters.Add(new SqlParameter
                {
                    Direction = ParameterDirection.Input,
                    DbType = DbType.Int32,
                    ParameterName = "@Age",
                    Value = entity.Age,
                });
                sqlCommand.Parameters.Add(new SqlParameter
                {
                    Direction = ParameterDirection.Input,
                    DbType = DbType.Int32,
                    ParameterName = "@Salary",
                    Value = entity.Salary,
                });
               await sqlCommand.ExecuteNonQueryAsync();
            }

            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {

            using (SqlCommand sqlCommand = _unitOfWork.CreateCommand())
            {
                sqlCommand.CommandText = "DeleteByIdEmployee";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@Id",
                    SqlDbType = SqlDbType.Int,
                    SqlValue = id,
                });

                sqlCommand.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@Complete",
                     SqlDbType = SqlDbType.Bit,
                       Direction = ParameterDirection.ReturnValue,
                });

                await sqlCommand.ExecuteNonQueryAsync();

                bool resultProcedure = (int)sqlCommand.Parameters["@Complete"].Value == 0 ? false : true;
                _dbChanged = true;

                return resultProcedure;
            }
        }

    }
}

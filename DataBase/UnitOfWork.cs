using System.Data.SqlClient;

namespace DataBase
{
    internal class UnitOfWork : IDisposable
    {
        private SqlTransaction _sqlTransaction;
        private SqlConnection _sqlConnection;
        private bool _ownConnection;
        public UnitOfWork(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
            _sqlConnection.Open();
            _sqlTransaction = _sqlConnection.BeginTransaction();
            _ownConnection = true;
        }

        public SqlCommand CreateCommand()
        {
            var command = _sqlConnection.CreateCommand();
            command.Transaction = _sqlTransaction;
            return command;
        }

        public void SaveDbChanges()
        {

            if (_sqlTransaction != null)
            {
                _sqlTransaction.Commit();
                _sqlTransaction = null;
            }
        }
        public void Dispose()
        {
            if (_sqlTransaction != null)
            {
                _sqlTransaction.Rollback();
                _sqlTransaction = null;
            }

            if (_sqlConnection != null && _ownConnection)
            {
                _sqlConnection.Close();
                _sqlConnection = null;
            }
        }
    }
}

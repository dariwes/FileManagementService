using ExceptionLogger;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccessLayer.Repository
{
    public class PersonRepository
    {
        private readonly string connection;
        private Logger logger;
        public List<Person> People = new List<Person>();

        public PersonRepository(string connection)
        {
            this.connection = connection;
            logger = new Logger(connection);
        }

        public void GetPerson()
        {
            using (var sqlConnection = new SqlConnection(connection))
            {
                sqlConnection.Open();
                var transaction = sqlConnection.BeginTransaction();

                try
                {
                    using (var command = new SqlCommand(Procedure.GetPeople, sqlConnection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Transaction = transaction;

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var person = new Person
                                    {
                                        ID = reader.GetInt32(0),
                                        PersonType = reader.GetString(1),
                                        FirstName = reader.GetString(2),
                                        MiddleName = reader.GetValue(3) as string,
                                        LastName = reader.GetString(4),
                                        EmailPromotion = reader.GetInt32(5),
                                        EmailAddress = reader.GetString(6),
                                        PhoneNumber = reader.GetString(7),
                                        PasswordSalt = reader.GetString(8),
                                        City = reader.GetString(9),
                                    };

                                    People.Add(person);
                                    transaction.Commit();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    logger.LogError(ex.Message);
                }
            }
        }

        public Person GetPersonByID(int ID)
        {
            using (var sqlConnection = new SqlConnection(connection))
            {
                sqlConnection.Open();
                var transaction = sqlConnection.BeginTransaction();
                var person = new Person();

                try
                {
                    using (var command = new SqlCommand(Procedure.GetPersonByID, sqlConnection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Transaction = transaction;


                        command.Parameters.Add(new SqlParameter("@ID", ID));

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    person.ID = reader.GetInt32(0);
                                    person.PersonType = reader.GetString(1);
                                    person.FirstName = reader.GetString(2);
                                    person.MiddleName = reader.GetValue(3) as string;
                                    person.LastName = reader.GetString(4);
                                    person.EmailPromotion = reader.GetInt32(5);
                                    person.EmailAddress = reader.GetString(6);
                                    person.PhoneNumber = reader.GetString(7);
                                    person.PasswordSalt = reader.GetString(8);
                                    person.City = reader.GetString(9);
                                }
                            }

                            transaction.Commit();
                            return person;
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    logger.LogError(ex.Message);
                    return null;
                }
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using AbstractDAL;
using Entities;
using Utils;

namespace BookCopyDAL
{
    public class BookCopyDao : IBookCopyDao
    {
        public BookCopyDao()
        {
        }

        public IEnumerable<BookCopy> GetAll()
        {
            var listBookCopy = new List<BookCopy>();
            const string sqlExpression = "SELECT * FROM BookCopy";
            using (var connection = Dbsql.GetDbConnection())
            {
                connection.Open();
                var command = new SqlCommand(sqlExpression, connection);
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                        listBookCopy.Add(new BookCopy(
                            (int)(dataReader["BookCopyID"]), 
                            (int)(dataReader["BookID"]),
                            (int)(dataReader["StorageID"])));
                }
            }
            return listBookCopy.AsEnumerable();
        }

        public int Delete(int idBookCopy)
        {
            try
            {
                const string sqlExpression = "DELETE FROM BookCopy WHERE BookCopyID = @id";
                using (var connection = Dbsql.GetDbConnection())
                {
                    connection.Open();
                    var command = new SqlCommand(sqlExpression, connection);
                    var idParam = new SqlParameter("@id", idBookCopy);
                    command.Parameters.Add(idParam);
                    return command.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }

        public int Create(BookCopy bookCopy)
        {
            try
            {
                const string sqlExpression = "INSERT INTO BookCopy (BookCopyID, BookID, StorageID) VALUES (@BookCopyID, @BookID, @StorageID)";
                using (var connection = Dbsql.GetDbConnection())
                {
                    connection.Open();
                    var command = new SqlCommand(sqlExpression, connection);
                    var idParam = new SqlParameter("@BookCopyID", bookCopy.BookCopyID);
                    command.Parameters.Add(idParam);
                    var idBookParam = new SqlParameter("@BookID", bookCopy.BookID);
                    command.Parameters.Add(idBookParam);
                    var idStorageParam = new SqlParameter("@StorageID", bookCopy.StorageID);
                    command.Parameters.Add(idStorageParam);
                    var number = command.ExecuteNonQuery();
                    return number;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using AbstractDAL;
using Entities;
using Utils;

namespace BookDAL
{
    public class BookDao : IBookDao
    {
        public BookDao()
        {
        }

        public IEnumerable<Book> GetAll()
        {
            var listBook = new List<Book>();
            const string sqlExpression = "SELECT * FROM Book";
            using (var connection = Dbsql.GetDbConnection())
            {
                connection.Open();
                var command = new SqlCommand(sqlExpression, connection);
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                        listBook.Add(new Book(
                            (int)(dataReader["BookID"]), 
                            (string)(dataReader["Title"]),
                            (int)(dataReader["YearOfWriting"]),
                            (int)(dataReader["PublishingHouseID"]),
                            (string)(dataReader["LanguageBook"])));
                }
            }
            return listBook.AsEnumerable();
        }

        public int Delete(int idBook)
        {
            try
            {
                const string sqlExpression = "DELETE FROM Book WHERE BookID = @id";
                using (var connection = Dbsql.GetDbConnection())
                {
                    connection.Open();
                    var command = new SqlCommand(sqlExpression, connection);
                    var idParam = new SqlParameter("@id", idBook);
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

        public int Create(Book book)
        {
            try
            {
                const string sqlExpression = "INSERT INTO Book (BookID, Title, YearOfWriting, PublishingHouseID, LanguageBook) VALUES (@BookID, @Title, @YearOfWriting, @PublishingHouseID, @LanguageBook)";
                using (var connection = Dbsql.GetDbConnection())
                {
                    connection.Open();
                    var command = new SqlCommand(sqlExpression, connection);
                    var idParam = new SqlParameter("@BookCopyID", book.BookID);
                    command.Parameters.Add(idParam);
                    var titleParam = new SqlParameter("@BookID", book.BookTitle);
                    command.Parameters.Add(titleParam);
                    var yearOfWritingParam = new SqlParameter("@StorageID", book.YearOfWriting);
                    command.Parameters.Add(yearOfWritingParam);
                    var publishingHouseIdParam = new SqlParameter("@StorageID", book.PublishingHouseID);
                    command.Parameters.Add(publishingHouseIdParam);
                    var languageBookIdParam = new SqlParameter("@StorageID", book.LanguageBook);
                    command.Parameters.Add(languageBookIdParam);
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
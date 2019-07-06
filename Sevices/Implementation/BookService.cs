using OpenSpace.Models;
using OpenSpace.Request;
using OpenSpace.Sevices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenSpace.Sevices.Implementation
{
    public class BookService : IBookService
    {
        private static List<Book> Books;

        public BookService()
        {
            Books = new List<Book>
            {
                new Book()
                {
                    Id = 1,
                    Name = "1984",
                    Author = "George Orwell"
                },

                new Book()
                {
                    Id = 2,
                    Name = "Uma Breve História do Tempo",
                    Author = "Stephen Hawking"
                },

                new Book()
                {
                    Id = 3,
                    Name = "Alice",
                    Author = "Lewis Carroll"
                },

                new Book()
                {
                    Id = 4,
                    Name = "Código da Vinci",
                    Author = "Dan Brown"
                }
            };
        }

        public List<Book> GetAll()
        {
            return Books;
        }

        public Book Get(int id)
        {
            Book book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                throw new ArgumentException("Livro não existe");
            }
            return book;
        }

        public int Post(BookRequest request)
        {
            int newId = Books.Count + 1;
            Books.Add(new Book()
            {
                Id = newId,
                Author = request.Author,
                Name = request.Name
            });

            return newId;
        }

        public void Put(int bookId, BookRequest request)
        {
            Book bookEdit = Books.FirstOrDefault(b => b.Id == bookId);
            if(bookEdit == null)
            {
                throw new ArgumentException("Livro não existe");
            }
            bookEdit.Name = request.Name;
            bookEdit.Author = request.Author;

        }

        public void Delete(int id)
        {
            Book bookEdit = Books.FirstOrDefault(b => b.Id == id);
            if (bookEdit == null)
            {
                throw new ArgumentException("Livro não existe");
            }
            Books.Remove(bookEdit);
        }
    }
}

using OpenSpace.Models;
using OpenSpace.Request;
using System.Collections.Generic;

namespace OpenSpace.Sevices.Interface
{
    public interface IBookService
    {

        List<Book> GetAll();
        Book Get(int id);
        int Post(BookRequest request);
        void Put(int bookId, BookRequest request);
        void Delete(int id);
    }
}

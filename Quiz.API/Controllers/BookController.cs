//using System;
//using System.Collections.Generic;
//using Microsoft.AspNetCore.Mvc;
//using Quiz.Core;
//using Quiz.Data.Service;

//namespace Quiz.API.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class BookController : ControllerBase
//    {
//        private readonly BookService _bookService;

//        public BookController(IRepository<Book> bookService)
//        {
//            this._bookService = (BookService)bookService;
//        }

//        [HttpGet]
//        public ActionResult<Result<List<Book>>> Get()
//        {
//            try
//            {
//                List<Book> books = _bookService.Get().Data;
//                return new Result<List<Book>>(true, "", books);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//    }
//}
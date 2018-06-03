using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
//using TestingControllersSample.Controllers;
//using TestingControllersSample.Core.Interfaces;
//using TestingControllersSample.Core.Model;
//using TestingControllersSample.ViewModels;
using TodoApi.Controllers;
using TodoApi.Models;
using Xunit;

namespace TodoApi.Tests
{
    [Collection("Live tests")]
    public class TodoControllerTests 
    {

        private TodoContext _context;
        private Mock<ILogger<TodoController>> _logger;
        private TodoContext _todoContext;

        public TodoControllerTests()
        {
            InitContext();
        }

        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase();

            this._context = new TodoContext(builder.Options);
            int changed = this._context.SaveChanges();
            this._todoContext = this._context;

            this._logger = new Mock<ILogger<TodoController>>();
        }

        [Fact]
        public void Index_ReturnsAListOfTodo()
        {
            var controller = new TodoController(_context, _logger.Object);


            var result = controller.GetAll();
            Assert.False(result.ElementAt(0).IsComplete);
            Assert.Equal("Item1", result.ElementAt(0).Name);
            Assert.Equal(1, result.ElementAt(0).Id);
            Assert.Single(result);
        }
    }
}

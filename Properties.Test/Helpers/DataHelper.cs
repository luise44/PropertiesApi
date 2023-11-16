using Microsoft.EntityFrameworkCore;
using Moq;
using Properties.Data;
using Properties.Data.Entities;
using System.Linq.Expressions;

namespace Properties.Test.Helpers
{
    public static class DataHelper
    {
        public static PropertiesDbContext GetDbContext<T>(List<T> data, Expression<Func<PropertiesDbContext, DbSet<T>>> dbSetSelectionExpression) where T : BaseEntity
        {
            IQueryable<T> lstDataQueryable = data.AsQueryable();
            Mock<DbSet<T>> dbSetMock = new Mock<DbSet<T>>();
            var options = new DbContextOptionsBuilder<PropertiesDbContext>().Options;
            Mock<PropertiesDbContext> dbContext = new Mock<PropertiesDbContext>(options);

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(lstDataQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(s => s.Expression).Returns(lstDataQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(s => s.ElementType).Returns(lstDataQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(s => s.GetEnumerator()).Returns(() => lstDataQueryable.GetEnumerator());
            dbSetMock.Setup(x => x.Add(It.IsAny<T>())).Callback<T>(data.Add);
            dbSetMock.Setup(x => x.Remove(It.IsAny<T>())).Callback<T>(x=> data.Remove(x));

            dbContext.Setup(dbSetSelectionExpression).Returns(dbSetMock.Object);

            return dbContext.Object;
        }
    }
}

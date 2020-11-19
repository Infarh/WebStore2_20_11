using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebStore.Infrastructure.Middleware
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _Next;

        public TestMiddleware(RequestDelegate Next) => _Next = Next;

        public async Task Invoke(HttpContext Context)
        {
            //Действия над context до следующего элемента в конвейере
            await _Next(Context); // Вызов следующего промежуточного ПО в конвейере
            // Действия над context после следующего элемента в конвейере
        }
    }
}

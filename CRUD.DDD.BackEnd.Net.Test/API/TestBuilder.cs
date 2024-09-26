using Microsoft.AspNetCore.Mvc.Testing;

namespace CRUD.DDD.BackEnd.Net.Test.API
{
    public abstract class IntegrationTestBuilder : IDisposable
    {
        protected HttpClient TestClient;
        private bool Disposed;

        protected IntegrationTestBuilder()
        {
            BootstrapTestingSuite();
        }

        protected void BootstrapTestingSuite()
        {
            Disposed = false;
            var appFactory = new WebApplicationFactory<Program>();
            TestClient = appFactory.CreateClient();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            if (disposing)
            {
                TestClient.Dispose();
            }

            Disposed = true;
        }
    }
}

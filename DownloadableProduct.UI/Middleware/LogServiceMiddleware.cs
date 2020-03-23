using DownloadableProduct.DataAccess.Repositories;
using DownloadableProduct.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DownloadableProduct.UI.Middleware
{
    public class LogServiceMiddleware
    {
        private readonly RequestDelegate Next;
        public LogServiceMiddleware(RequestDelegate requestDelegate)
        {
            Next = requestDelegate;
        }

        public async Task Invoke(HttpContext context, LogServiceRepository logServiceRepositoy)
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            string userId = null;

            if (context.User.Identity.IsAuthenticated)
            {
                userId = context.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            }

            await Next(context);

            stopwatch.Stop();

            logServiceRepositoy.Insert(new LogService
            {
                CreateDate = DateTime.Now,
                UserId = userId,
                Method = context.Request.Method,
                RelativePath = context.Request.Path.Value,
                ContentLengthRequest = context.Request.ContentLength,
                ContentLengthResponse = context.Response.ContentLength,
                ResponseStatusCode = context.Response.StatusCode,
                Elabsed = stopwatch.Elapsed,
                IpAddress = context.Connection.RemoteIpAddress.ToString()
            });
            logServiceRepositoy.Save();
        }
    }
}

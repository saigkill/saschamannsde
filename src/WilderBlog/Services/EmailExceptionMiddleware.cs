using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace WilderBlog.Services
{
    public class EmailExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMailService _mailService;

        public EmailExceptionMiddleware(RequestDelegate next, IMailService mailService)
        {
            _next = next;
            _mailService = mailService;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await _mailService.SendMailAsync("exceptionmessage.txt", "Sascha Manns", "Sascha.Manns@outlook.de", "[MannsBlog Exception]", ex.ToString());

                // Don't swallow the exception
                throw ex;
            }

        }
    }
}

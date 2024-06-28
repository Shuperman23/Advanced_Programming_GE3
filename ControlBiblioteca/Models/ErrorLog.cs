using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace ControlBiblioteca.Models
{
    public class ErrorLog
    {
        public ErrorLog(int Id, string Controller = null, string Endpoint = null, string ErrorMessage = null, string ErrorStackTrace = null)
        {
            this.Id = Id;
            this.Controller = Controller;
            this.Endpoint = Endpoint;
            this.ErrorMessage = ErrorMessage;
            this.ErrorStackTrace = ErrorStackTrace;
            this.ErrorTimestamp = DateTime.Now;
        }

        public int Id { get; set; }

        public string Controller { get; set; }

        public string Endpoint { get; set; }

        public string ErrorMessage { get; set; }

        public string ErrorStackTrace { get; set; }

        public DateTime ErrorTimestamp { get; set; }
    }
}

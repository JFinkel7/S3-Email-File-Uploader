using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace MainActivity {
    public class Mail {
        private readonly EmailAddress To;
        private readonly EmailAddress From;
        private string Subject;
        private string Body;
        private string HtmlContent;
        public string ErrorMessage { get; private set; }

        public Mail(string senderEmailAddress, string clientEmailAddress) {
            // To The ClientEmailAddress
            To = new EmailAddress(clientEmailAddress);
            // From My Email Address
            From = new EmailAddress(senderEmailAddress);
        }


        // Sets The Email Subject For The Email 
        public void setSubject(string subject) {
            this.Subject = subject;
        }

        // Sets The Body Text For The Email 
        public void setBody(string body) {
            this.Body = body;
        }

        // Sets The Html Content For The Email 
        public void setHtmlContent(string htmlContent) {
            this.HtmlContent = htmlContent;
        }

        // Sends A Single Email 
        public async Task<Response> sendMail(string key) {
            var client = new SendGridClient(key);
            var message = MailHelper.CreateSingleEmail(
                From,
                To,
                Subject,
                Body,
                HtmlContent
            );
            var response = await client.SendEmailAsync(message);
            return response;
        }
    }


}// class ends 


using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using MainActivity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MainActivity.Services;


namespace MainActivity.Controllers {

    public class HomeController : Controller {
        private readonly ProjectCredentials projectCredentials;
        private readonly TransferUtility transferUtility;
        private readonly IAmazonS3 client;

        public HomeController() {
            this.projectCredentials = ProjectCredentials.GetInstance();
            this.client = new AmazonS3Client(projectCredentials.ReadAccessKey(), projectCredentials.ReadSecretKey(), RegionEndpoint.USWest1);
            this.transferUtility = new TransferUtility(client);

        }

        // GET - 
        [HttpGet]
        public IActionResult Index() {
            return View();
        }


        // POST -
        [HttpPost]
        public async Task<IActionResult> Index(ClientViewModel model) {
            if (ModelState.IsValid) {

                using (var memoryStream = new MemoryStream()) {
                    // Reads & Uploads The Client File 
                    model.Client.File.CopyTo(memoryStream);
                    var uploadRequest = new TransferUtilityUploadRequest {
                        InputStream = memoryStream,
                        BucketName = projectCredentials.ReadBucketName(),
                        // Reads Client Key Based On Client FileName
                        Key = model.Client.File.FileName,
                        StorageClass = S3StorageClass.StandardInfrequentAccess,
                        CannedACL = S3CannedACL.PublicRead
                    };
                    // Uploads The File 
                    await this.transferUtility.UploadAsync(uploadRequest);
                }
                // Sends The Email 
                await sendEmail(model);
                ModelState.Clear();
            }
            return View();
        }




        private string GetUrlString(ClientViewModel model) {
            return (this.client.GetPreSignedURL(new GetPreSignedUrlRequest {
                BucketName = projectCredentials.ReadBucketName(),
                Key = model.Client.File.FileName,
                Expires = DateTime.Now.AddMinutes(5)
            }));
        }

        private async Task sendEmail(ClientViewModel model) {
            Mail mail = new Mail(projectCredentials.ReadMyEmail(), model.Client.EmailAddress);

            mail.setSubject("Dear Client");

            mail.setBody(GetUrlString(model));

            mail.setHtmlContent(String.Format("<p>{0}</p>", GetUrlString(model)));

            await mail.sendMail(projectCredentials.ReadEmailApiKey());

            ViewData["Result"] = ("Email Sent");

        }


    }
}

using Microsoft.Extensions.Configuration;
using System;



namespace MainActivity.Services {
    public sealed class ProjectCredentials {
        //**
        private readonly IConfiguration configuration;
        private static ProjectCredentials Instance = null;
   
        //**
        // Constructor 
        private ProjectCredentials(IConfiguration configuration) {
            this.configuration = configuration;

        }

        // Initilize The Class
        public static void Initialize(IConfiguration configuration) {
            if (Instance == null) {
                Instance = new ProjectCredentials(configuration);
            } else {
                throw new InvalidOperationException("ProjectCredentials Is Already Initialized");
            }
        }

        // Get Class Instance 
        public static ProjectCredentials GetInstance() {
            if (Instance == null) {
                throw new InvalidOperationException("ProjectCredentials Is Not Initialized");
            }
            return Instance;
        }
    
        public string ReadMyEmail() {
            return (configuration["MY_EMAIL"]);
        }


        public string ReadAccessKey() {
            return (configuration["AccessKey"]);
        }

        public string ReadSecretKey() {
            return (configuration["SecretKey"]);
        }


        public string ReadEmailApiKey() {
            return (configuration["EmailApiKey"]);
        }

        public string ReadBucketName() {
            return (configuration["BUCKET_NAME"]);
        }


    }// Class ENDS 
}

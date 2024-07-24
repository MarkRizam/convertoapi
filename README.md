# ConvertoAPI

ConvertoAPI is a backend API that converts numerical input into words and returns the corresponding string output. For example, an input of "123.45" is converted to "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS".

## Table of Contents
1. [Introduction](#introduction)
2. [Prerequisites](#prerequisites)
3. [Installation](#installation)
4. [Building the Project](#building-the-project)
5. [Running the Project](#running-the-project)
6. [Deploying the Project](#deploying-the-project)
7. [Interacting with the API](#interacting-with-the-api)
8. [Project Structure](#project-structure)

## Introduction

ConvertoAPI is a RESTful API built with ASP.NET Core and Docker. It is designed to be deployed on AWS Elastic Beanstalk using Jenkins for CI/CD.

## Prerequisites

Before you begin, ensure you have the following installed on your system:
- .NET SDK 8.0
- Docker
- AWS CLI
- AWS Elastic Beanstalk CLI (EB CLI)
- Git
- Jenkins

## Installation

### Clone the Repository

```bash
git clone https://github.com/MarkRizam/convertoapi.git
cd convertoapi
AWS Configuration
Ensure your AWS CLI is configured with the necessary credentials and default region.

bash
Copy code
aws configure
Jenkins Configuration
Install Jenkins on your system.

Install necessary Jenkins plugins:

Amazon EC2 Plugin
AWS Elastic Beanstalk Publisher
Git Plugin
Pipeline Plugin
Set up AWS credentials in Jenkins:

Go to Jenkins Dashboard > Manage Jenkins > Manage Credentials.
Add credentials with your AWS access key and secret key.
Building the Project
Build Docker Image
To build the Docker image locally:

bash
Copy code
docker build -t convertoapi:latest .
Running the Project
Run the Docker Container
To run the Docker container locally:

bash
Copy code
docker run -d -p 8080:80 convertoapi:latest
The API will be accessible at http://localhost:8080.

Deploying the Project
Jenkins Pipeline
Set up a Jenkins pipeline with the following script:

groovy
Copy code
pipeline {
    agent any

    environment {
        ECR_REPO_URI = 'your_AWS_Id.dkr.ecr.ap-southeast-2.amazonaws.com/convertoapi'
        AWS_REGION = 'ap-southeast-2'
        AWS_ACCOUNT_ID = 'your_acc_id'
        DOCKER_IMAGE = 'convertoapi:latest'
        DOCKER_TAG = 'latest'
    }

    stages {
        stage('Clone Repository') {
            steps {
                git url: 'https://github.com/MarkRizam/convertoapi.git', branch: 'main'
            }
        }
        stage('Build Docker Image') {
            steps {
                script {
                    bat 'docker build -t %DOCKER_IMAGE% .'
                }
            }
        }
        stage('Login to AWS ECR') {
            steps {
                script {
                    bat 'aws ecr get-login-password --region %AWS_REGION% | docker login --username AWS --password-stdin %ECR_REPO_URI%'
                }
            }
        }
        stage('Push Docker Image to ECR') {
            steps {
                script {
                    bat 'docker tag %DOCKER_IMAGE% %ECR_REPO_URI%:%DOCKER_TAG%'
                    bat 'docker push %ECR_REPO_URI%:%DOCKER_TAG%'
                }
            }
        }
        stage('Deploy to Elastic Beanstalk') {
            steps {
                script {
                    bat '''
                    set AWS_ACCESS_KEY_ID=%AWS_ACCESS_KEY_ID%
                    set AWS_SECRET_ACCESS_KEY=%AWS_SECRET_ACCESS_KEY%
                    eb init -p docker converto --region %AWS_REGION%
                    eb create converto-env
                    eb deploy
                    '''
                }
            }
        }
    }

    post {
        always {
            cleanWs()
        }
    }
} 


### How to Add This to Your Git Repository

1. Save the `deploy.sh` script in the root of your project repository.
2. Save the `Jenkinsfile` in the root of your project repository.
3. Push the changes to your Git repository:
   ```sh
   git add deploy.sh Jenkinsfile README.md
   git commit -m "Added deployment script and Jenkins pipeline configuration"
   git push origin main


Interacting with the API
API Endpoints
Convert Number to Words
Endpoint: /api/convert
Method: POST
Request Body: { "number": "123.45" }
Response Body: { "words": "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS" }

Example Request
Using curl:

bash
Copy code
curl -X POST http://localhost:8080/api/convert -H "Content-Type: application/json" -d '{ "number": "123.45" }'
Project Structure
Copy code
ConvertoApi/
├── ConvertoApi.csproj
├── Controllers/
│   └── ConvertController.cs
├── Services/
│   └── NumberToWordsService.cs
├── Dockerfile
└── README.md

References:
front end git : https://github.com/MarkRizam/converto.git
front end URL : https://converto.s3.ap-southeast-2.amazonaws.com/index.html
back end API URL : https://convertod-env.eba-xyiimhck.ap-southeast-2.elasticbeanstalk.com

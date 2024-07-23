pipeline {
    agent any

    environment {
        ECR_REPO_URI = '637423371804.dkr.ecr.ap-southeast-2.amazonaws.com/convertoapi'
        AWS_REGION = 'ap-southeast-2'
        AWS_ACCOUNT_ID = '637423371804'
        DOCKER_IMAGE = 'convertoapi:latest'
        DOCKER_TAG = 'latest'
        APP_NAME = 'converto'
        ENV_NAME = 'converto-env'
    }

    stages {
        stage('Clone Repository') {
            steps {
                git url: 'https://github.com/MarkRizam/convertoapi.git', branch: 'main'
            }
        }
        stage('Run Deployment Script') {
            steps {
                script {
                    bat 'bash deploy.sh'
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

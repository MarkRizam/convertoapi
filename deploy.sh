#!/bin/bash

set -e

# Variables
ECR_REPO_URI="637423371804.dkr.ecr.ap-southeast-2.amazonaws.com/convertoapi"
AWS_REGION="ap-southeast-2"
DOCKER_IMAGE="convertoapi:latest"
DOCKER_TAG="latest"
APP_NAME="converto"
ENV_NAME="converto-env"

# Authenticate with AWS
echo "Authenticating with AWS..."
aws ecr get-login-password --region $AWS_REGION | docker login --username AWS --password-stdin $ECR_REPO_URI

# Build the Docker image
echo "Building Docker image..."
docker build -t $DOCKER_IMAGE .

# Tag the Docker image
echo "Tagging Docker image..."
docker tag $DOCKER_IMAGE $ECR_REPO_URI:$DOCKER_TAG

# Push the Docker image to ECR
echo "Pushing Docker image to ECR..."
docker push $ECR_REPO_URI:$DOCKER_TAG

# Initialize Elastic Beanstalk
echo "Initializing Elastic Beanstalk application..."
eb init -p docker $APP_NAME --region $AWS_REGION

# Check if the environment exists
if eb status $ENV_NAME >/dev/null 2>&1; then
    echo "Environment $ENV_NAME already exists. Skipping creation."
else
    echo "Creating Elastic Beanstalk environment..."
    eb create $ENV_NAME
fi

# Deploy to Elastic Beanstalk
echo "Deploying to Elastic Beanstalk..."
eb deploy $ENV_NAME

echo "Deployment completed successfully!"

#!/bin/bash

# Function to send a POST request to the API
test_api() {
    local number=$1
    local url="https://d29cj64b482wgp.cloudfront.net/api/Convert"
    
    # Send the POST request with JSON body
    response=$(curl -s -X POST $url \
        -H "Content-Type: application/json" \
        -d "{\"number\": $number}")

    # Print the response
    echo "Response: $response"
}

# Loop to allow interactive input
while true; do
    echo -n "Enter a number to convert (or type 'exit' to quit): "
    read number
    if [[ "$number" == "exit" ]]; then
        break
    fi
    test_api $number
done

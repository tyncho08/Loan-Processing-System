#!/bin/bash

echo "=== Loan Processing System Startup Script ==="
echo

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Function to check if a port is in use
check_port() {
    if lsof -Pi :$1 -sTCP:LISTEN -t >/dev/null ; then
        return 0
    else
        return 1
    fi
}

# Function to kill process on port
kill_port() {
    if check_port $1; then
        echo -e "${YELLOW}Killing process on port $1${NC}"
        lsof -ti:$1 | xargs kill -9
        sleep 2
    fi
}

echo -e "${GREEN}Starting both Angular frontend and .NET Web API...${NC}"

# Kill any existing processes on the ports
kill_port 3000  # Angular port
kill_port 44365 # Web API port

# Check if Node.js is installed
if ! command -v node &> /dev/null; then
    echo -e "${RED}Error: Node.js is not installed. Please install Node.js first.${NC}"
    echo "Visit: https://nodejs.org/"
    exit 1
fi

# Get the current directory to navigate properly
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"

# Start Angular first
echo "Starting Angular frontend on port 3000..."
cd "UI Angular/LPSystemAngular"

# Install dependencies if node_modules doesn't exist
if [ ! -d "node_modules" ]; then
    echo "Installing Angular dependencies..."
    npm install
fi

# Fix Node.js compatibility issue with legacy OpenSSL
echo "Starting Angular with Node.js compatibility fix..."
export NODE_OPTIONS="--openssl-legacy-provider"
npm start &
ANGULAR_PID=$!

# Go back to script directory and start Web API
cd "$SCRIPT_DIR"
echo "Starting .NET Core Web API on port 44365..."
cd "WebAPICore"

dotnet run --launch-profile https &
WEBAPI_PID=$!

echo "Waiting for API to start..."
sleep 3

echo
echo -e "${GREEN}=== Applications Started ===${NC}"
echo "Angular Frontend: http://localhost:3000"
echo "Web API Backend: http://localhost:44365"
echo
echo "Press Ctrl+C to stop both applications"

# Wait for Ctrl+C
trap 'echo -e "\n${YELLOW}Stopping applications...${NC}"; kill $ANGULAR_PID 2>/dev/null; kill $WEBAPI_PID 2>/dev/null; exit 0' INT
wait
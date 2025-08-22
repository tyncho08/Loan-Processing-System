#!/bin/bash

echo "=== Loan Processing System Startup Script ==="
echo

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
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
kill_port 4200  # Angular port (default Angular port)
kill_port 5275  # Web API port (updated to match our configuration)

# Check if Node.js is installed
if ! command -v node &> /dev/null; then
    echo -e "${RED}Error: Node.js is not installed. Please install Node.js first.${NC}"
    echo "Visit: https://nodejs.org/"
    exit 1
fi

# Check if .NET is installed
if ! command -v dotnet &> /dev/null; then
    echo -e "${RED}Error: .NET Core is not installed. Please install .NET Core SDK 8.0 or later.${NC}"
    echo "Visit: https://dotnet.microsoft.com/download"
    exit 1
fi

# Get the current directory to navigate properly
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"

echo -e "${BLUE}Starting .NET Core Web API on port 5275...${NC}"
cd "WebAPICore"

# Build the .NET project first
echo "Building .NET project..."
dotnet build
if [ $? -ne 0 ]; then
    echo -e "${RED}Error: .NET build failed${NC}"
    exit 1
fi

# Start Web API in background
dotnet run --urls="http://localhost:5275" &
WEBAPI_PID=$!

# Go back to script directory and start Angular
cd "$SCRIPT_DIR"
echo -e "${BLUE}Starting Angular frontend on port 4200...${NC}"
cd "UI Angular/LPSystemAngular"

# Install dependencies if node_modules doesn't exist
if [ ! -d "node_modules" ]; then
    echo "Installing Angular dependencies..."
    npm install
fi

# Fix Node.js compatibility issue with legacy OpenSSL
echo "Starting Angular with Node.js compatibility fix..."
export NODE_OPTIONS="--openssl-legacy-provider"

# Start Angular in background
ng serve --port 4200 --host 0.0.0.0 &
ANGULAR_PID=$!

echo "Waiting for services to start..."
sleep 5

echo
echo -e "${GREEN}=== Applications Started Successfully ===${NC}"
echo -e "${GREEN}🌐 Angular Frontend: ${NC}http://localhost:4200"
echo -e "${GREEN}🔌 Web API Backend:  ${NC}http://localhost:5275"
echo -e "${GREEN}📋 API Documentation: ${NC}http://localhost:5275/swagger"
echo
echo -e "${YELLOW}💡 Tips:${NC}"
echo "  - The Angular app will automatically open in your browser"
echo "  - Use the API documentation at /swagger to test endpoints"
echo "  - Check the terminal for any error messages"
echo
echo -e "${YELLOW}Press Ctrl+C to stop both applications${NC}"

# Function to cleanup on exit
cleanup() {
    echo -e "\n${YELLOW}Stopping applications...${NC}"
    if [ ! -z "$ANGULAR_PID" ]; then
        kill $ANGULAR_PID 2>/dev/null
        echo "✅ Angular frontend stopped"
    fi
    if [ ! -z "$WEBAPI_PID" ]; then
        kill $WEBAPI_PID 2>/dev/null
        echo "✅ Web API backend stopped"
    fi
    echo -e "${GREEN}👋 All applications stopped successfully${NC}"
    exit 0
}

# Set trap for cleanup
trap cleanup INT

# Wait for both processes
wait
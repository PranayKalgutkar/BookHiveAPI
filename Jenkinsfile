pipeline {
    agent any

    environment {
        USER = "pranaykalgutkar"
        API_SRC = "/Users/$USER/repos/nextgen/api/api.core"
        DEPLOY_DIR = "/var/www/bookhiveapi"
        LOG_FILE = "output.log"
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'Stage', url: 'https://github.com/PranayKalgutkar/BookHiveAPI.git'
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build --configuration Release'
            }
        }

        stage('Test') {
            steps {
                sh 'dotnet test --configuration Release'
            }
        }

        stage('Deploy') {
            when {
                branch 'Stage'  // Only run on Stage branch
            }
            steps {
                script {
                    sh '''
                    #!/bin/bash
                    set -e

                    # Run commands as the specified user
                    sudo -u $USER bash <<EOF

                    # Navigate to API source code
                    cd $API_SRC

                    # Restore, build, and publish
                    dotnet restore
                    dotnet build --configuration Release
                    dotnet publish -c Release -o $DEPLOY_DIR

                    # Navigate to deployment folder
                    cd $DEPLOY_DIR

                    # Set production environment
                    export ASPNETCORE_ENVIRONMENT=Production

                    # Stop any existing running API process
                    pkill -f "dotnet API.Core.dll" || true

                    # Start new instance in background
                    nohup dotnet API.Core.dll > $LOG_FILE 2>&1 &

                    EOF
                    '''
                }
            }
        }
    }
}